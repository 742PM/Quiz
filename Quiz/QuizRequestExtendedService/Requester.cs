using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using QuizRequestExtendedService.DTO;
using RestSharp;

namespace QuizRequestExtendedService
{
    public class Requester : IQuizServiceExtended
    {
        private readonly string serverUri;
        private const int MaxRetries = 5;

        public Requester(string serverUri)
        {
            this.serverUri = serverUri;
        }

        public IEnumerable<TopicDTO> GetTopics()
        {
            var client = new RestClient(serverUri + "/api/topics");
            var content = SendGetRequest(client, Method.GET);
            var topics = JsonConvert.DeserializeObject<List<TopicDTO>>(content.Content);
            return topics;
        }

        public IEnumerable<LevelDTO> GetLevels(Guid topicId)
        {
            var client = new RestClient(serverUri + $"/api/{topicId}/levels");
            var content = SendGetRequest(client, Method.GET);
            var levels = JsonConvert.DeserializeObject<List<LevelDTO>>(content.Content);
            return levels;
        }
        
        public IEnumerable<AdminTemplateGeneratorDTO> GetTemplateGenerators(Guid topicId, Guid levelId)
        {
            var client = new RestClient(serverUri + $"/service/{topicId}/{levelId}/templateGenerators");
            var content = SendGetRequest(client, Method.GET);
            var levels = JsonConvert.DeserializeObject<List<AdminTemplateGeneratorDTO>>(content.Content);
            return levels;
        }

        private IRestResponse SendGetRequest(IRestClient client, Method method, Parameter parameter = null)
        {
            var request = new RestRequest(method);
            if (parameter != null)
                request.AddParameter(parameter);
            for (var i = 0; i < MaxRetries; i++)
            {
                var response = client.Execute(request);
                if (response.IsSuccessful) 
                    return response;
            }
            return null;
        }
    }
}
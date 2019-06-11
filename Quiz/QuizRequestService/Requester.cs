using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Result;
using Newtonsoft.Json;
using Polly;
using QuizRequestService.DTO;
using RestSharp;

namespace QuizRequestService
{
    public class Requester : IQuizService
    {
        private readonly string serverUri;

        public Requester(string serverUri)
        {
            this.serverUri = serverUri;
        }

        public async Task<Maybe<IEnumerable<TopicDTO>>> GetTopics()
        {
            var client = new RestClient(serverUri + "/api/topics");
            var request = await SendRequest(client, Method.GET);
            return request.HasValue 
                ? JsonConvert.DeserializeObject<List<TopicDTO>>(request.Value.Content) 
                : Maybe<IEnumerable<TopicDTO>>.None;
        }

        public async Task<Maybe<IEnumerable<LevelDTO>>> GetLevels(Guid topicId)
        {
            var client = new RestClient(serverUri + $"/api/{topicId}/levels");
            var request = await SendRequest(client, Method.GET);
            return request.HasValue
                ? JsonConvert.DeserializeObject<List<LevelDTO>>(request.Value.Content)
                : Maybe<IEnumerable<LevelDTO>>.None;
        }

        public async Task<Maybe<IEnumerable<LevelDTO>>> GetAvailableLevels(Guid userId, Guid topicId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/availableLevels");
            var request = await SendRequest(client, Method.GET);
            return request.HasValue && request.Value.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<List<LevelDTO>>(request.Value.Content)
                : Maybe<IEnumerable<LevelDTO>>.None;
        }

        public async Task<Maybe<ProgressDTO>> GetProgress(Guid userId, Guid topicId, Guid levelId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/{levelId}/progress");
            var request = await SendRequest(client, Method.GET);
            return (request.HasValue && request.Value.StatusCode == HttpStatusCode.OK)
                ? JsonConvert.DeserializeObject<ProgressDTO>(request.Value.Content)
                : Maybe<ProgressDTO>.None;
        }

        public async Task<Maybe<TaskDTO>> GetTaskInfo(Guid userId, Guid topicId, Guid levelId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/{levelId}/task");
            var request = await SendRequest(client, Method.GET);
            return request.HasValue && request.Value.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<TaskDTO>(request.Value.Content)
                : Maybe<TaskDTO>.None;
        }

        public async Task<Maybe<TaskDTO>> GetNextTaskInfo(Guid userId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/nextTask");
            var request = await SendRequest(client, Method.GET);
            return request.HasValue && request.Value.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<TaskDTO>(request.Value.Content)
                : Maybe<TaskDTO>.None;
        }

        public async Task<Maybe<HintDTO>> GetHint(Guid userId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/hint");
            var request = await SendRequest(client, Method.GET);
            return request.HasValue && request.Value.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<HintDTO>(request.Value.Content)
                : Maybe<HintDTO>.None;
        }

        public async Task<Maybe<bool>> SendAnswer(Guid userId, string answer)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/sendAnswer");
            var parameter = new Parameter("application/json", $"\"{answer}\"", ParameterType.RequestBody);
            var request = await SendRequest(client, Method.POST, parameter);
            if (request.HasValue && request.Value.StatusCode == HttpStatusCode.OK)
                switch (request.Value.Content)
                {
                    case "true":
                        return true;
                    case "false":
                        return false;
                    default:
                        return Maybe<bool>.None;
                }
            return Maybe<bool>.None;
        }

        private async Task<Maybe<IRestResponse>> SendRequest(IRestClient client, Method method, Parameter parameter = null)
        {
            var request = new RestRequest(method);
            if (parameter != null)
                request.AddParameter(parameter);
            var response = await Policy
                .HandleResult<IRestResponse>(m => !m.IsSuccessful)
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2))
                .ExecuteAsync(() => client.ExecuteTaskAsync(request));
            return response.IsSuccessful ? response.Sure() : Maybe<IRestResponse>.None;
        }
    }
}
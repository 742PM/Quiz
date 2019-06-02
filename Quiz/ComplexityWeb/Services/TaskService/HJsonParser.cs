using Hjson;
using Infrastructure.Result;

namespace QuizWebApp.Services.TaskService
{
    public static class HJsonParser
    {
        public static Maybe<string> ConvertToJson(this string hjson) => hjson.Try(hj => HjsonValue.Parse(hj).ToString());

    }
}
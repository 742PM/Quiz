using Hjson;
using Infrastructure.Result;

namespace Infrastructure
{
    public static class HJsonParser
    {
        public static Maybe<string> ConvertToJson(this string hjson) => hjson.Try(hj => HjsonValue.Parse(hj).ToString());

    }
}
using System;
using System.Threading.Tasks;
using DataBase;
using Infrastructure.Extensions;
using Infrastructure.Result;
using MongoDB.Driver;
using static Infrastructure.Result.None;

namespace QuizRequestExtendedService.Database
{
    public class UserRepository<TToken, TAuth> : IUserRepository<TToken, TAuth>
    {
        private const string CollectionName = "EditorAuth";
        private readonly IMongoCollection<Container> authDataCollection;
        private readonly Func<TToken> tokenCreator;

        static UserRepository()
        {
            MongoHelpers.AutoRegisterClassMap<Container>();
            MongoHelpers.AutoRegisterClassMap<TAuth>();
            MongoHelpers.AutoRegisterClassMap<TToken>();
        }

        public UserRepository(IMongoDatabase database, Func<TToken> tokenCreator)
        {
            this.tokenCreator = tokenCreator;
            authDataCollection = database.GetCollection<Container>(CollectionName);
        }

        public async Task RegisterUserAsync(TAuth data)
        {
            var token = tokenCreator();
            await authDataCollection.InsertOneAsync(new Container(token, data));
        }

        public async Task<Maybe<TToken>> AuthenticateUserAsync(TAuth data)
        {
            var result = await authDataCollection.FindAsync(ta => ta.Data.Equals(data));

            if (!await result.AnyAsync())
                return Maybe<TToken>.None;
            return (await result.FirstAsync()).Token.Sure();
        }

        public None RegisterUser(TAuth data)
        {
            var token = tokenCreator();
            authDataCollection.InsertOne(new Container(token, data));
            return Nothing;
        }

        public Maybe<TToken> AuthenticateUser(TAuth data)
        {
            var result = authDataCollection.Find(ta => ta.Data.Equals(data));

            return result.Any() ? result.First().Token.Sure() : Maybe<TToken>.None;
        }

        private class Container
        {
            public Container(TToken token, TAuth data)
            {
                Token = token;
                Data = data;
            }

            public TAuth Data { get; }
            public TToken Token { get; }
        }
    }
}
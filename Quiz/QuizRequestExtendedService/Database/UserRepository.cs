using System;
using System.Threading.Tasks;
using DataBase;
using Infrastructure.Extensions;
using Infrastructure.Result;
using MongoDB.Driver;

namespace QuizRequestExtendedService.Database
{ 
    public class UserRepository<TToken, TAuth> : IUserRepository<TToken, TAuth>
    {
        private readonly Func<TToken> tokenCreator;
        private const string CollectionName = "EditorAuth";
        private readonly IMongoCollection<Container> authDataCollection;

        static UserRepository()
        {
            MongoHelpers.AutoRegisterClassMap<Container>();
            MongoHelpers.AutoRegisterClassMap<TAuth>();
            MongoHelpers.AutoRegisterClassMap<TToken>();
        }

        public UserRepository(IMongoDatabase database, Func<TToken> tokenCreator )
        {
            this.tokenCreator = tokenCreator;
            authDataCollection = database.GetCollection<Container>(CollectionName);
        }

        public async Task RegisterUser(TAuth data)
        {
            var token = tokenCreator();
            await authDataCollection.InsertOneAsync(new Container(token, data));
        }

        public async Task<Maybe<TToken>> AuthenticateUser(TAuth data)
        {
            var result = await authDataCollection.FindAsync(ta => ta.Data.Equals(data));

            if (!await result.AnyAsync())
                return Maybe<TToken>.None;
            return (await result.FirstAsync()).Token.Sure();
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
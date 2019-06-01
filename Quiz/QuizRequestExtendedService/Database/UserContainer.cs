using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Result;
using MongoDB.Driver;
using DataBase.;

namespace QuizRequestExtendedService.Database
{
    public class UserContainer<TToken, TAuth>
    {
        private const string CollectionName = "EditorAuth";
        private readonly IMongoCollection<Container> authDataCollection;

        static UserContainer()
        {
            MongoHelpers.
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

        public UserContainer(IMongoDatabase database)
        {
            authDataCollection = database.GetCollection<Container>(CollectionName);
        }

        public async Task<Maybe<TToken>>  AuthenticateUser(TAuth data)
        {
             var result = await authDataCollection.FindAsync(ta => ta.Data.Equals(data));
             
             if (!await result.AnyAsync())
                 return Maybe<TToken>.None;
             return (await result.FirstAsync()).Token.Sure();
        }
    }
}

using System;
using MongoDB.Driver;

namespace DataBase
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> userCollection;
        
        public const string CollectionName = "users";

        public MongoUserRepository(IMongoDatabase database)
        {
            userCollection = database.GetCollection<UserEntity>(CollectionName);
        }

        public UserEntity Insert(UserEntity user)
        {
            userCollection.InsertOne(user);
            return user;
        }

        public UserEntity FindById(Guid id)
        {
            return userCollection.Find(u => u.Id == id).SingleOrDefault();
        }

        public void Update(UserEntity user)
        {
            userCollection.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void Delete(Guid id)
        {
            userCollection.DeleteOne(u => u.Id == id);
        }

        public UserEntity UpdateOrInsert(UserEntity user)
        {
            userCollection.ReplaceOne(
                u => u.Id == user.Id,
                user,
                new UpdateOptions
                {
                    IsUpsert = true,
                });
            return user;
        }
    }
}
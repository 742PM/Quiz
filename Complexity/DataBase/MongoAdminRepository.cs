using Application.Repositories;
using Application.Repositories.Entities;
using MongoDB.Driver;

namespace DataBase
{
    public class MongoAdminRepository : IAdminRepository
    {
        private const string CollectionName = "Admins";
        
        private readonly IMongoCollection<AdminEntity> adminCollection;

        public MongoAdminRepository(IMongoDatabase database)
        {
            adminCollection = database.GetCollection<AdminEntity>(CollectionName);
        }

        public AdminEntity Insert(AdminEntity adminEntity)
        {
            adminCollection.InsertOne(adminEntity);
            return adminEntity;
        }

        public AdminEntity FindByHash(string hash)
        {
            return adminCollection
                .Find(a => a.PasswordHash == hash)
                .SingleOrDefault();
        }

        public void Delete(string hash)
        {
            adminCollection
                .DeleteOne(a => a.PasswordHash == hash);
        }

        public void Update(AdminEntity adminEntity)
        {
            adminCollection
                .ReplaceOne(a => a.PasswordHash == adminEntity.PasswordHash, adminEntity);
        }
    }
}
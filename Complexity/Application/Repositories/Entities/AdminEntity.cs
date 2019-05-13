using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class AdminEntity : Entity
    {
        public AdminEntity(Guid id) : base(id)
        {
        }

        public AdminEntity(Guid id, string passwordHash, UserRightsEntity userRightsEntity) : base(id)
        {
            PasswordHash = passwordHash;
            UserRightsEntity = userRightsEntity;
        }

        public string PasswordHash { get; }

        public UserRightsEntity UserRightsEntity { get; }

        public AdminEntity With(string passwordHash, UserRightsEntity userRightsEntity)
            => new AdminEntity(Id, passwordHash, userRightsEntity);
    }
}
using System;
using Domain;
using Infrastructure;
using Infrastructure.DDD;

namespace DataBase.Entities
{
    public class UserEntity : Entity<Guid>
    {
        public UserEntity(Guid id) : base(id)
        {
        }

        public UserEntity With(UserProgressEntity userProgress) => new UserEntity(Id,userProgress);

        public UserEntity(Guid id, UserProgressEntity userProgressEntity) : base(id)
        {
            UserProgressEntity = userProgressEntity;
        }

        public UserProgressEntity UserProgressEntity { get; }
    }
}

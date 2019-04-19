using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class UserEntity : Entity
    {
        public UserEntity(Guid id) : base(id)
        {
        }

        public UserEntity(Guid id, UserProgressEntity userProgressEntity) : base(id)
        {
            UserProgressEntity = userProgressEntity;
        }

        public UserProgressEntity UserProgressEntity { get; private set; }

        public UserEntity With(UserProgressEntity userProgress) => new UserEntity(Id, userProgress);
    }
}

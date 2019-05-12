using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class UserEntity : Entity
    {
        public UserEntity(Guid id) : base(id)
        {
        }

        public UserEntity(Guid id, UserProgressEntity userProgressEntity, UserRightsEntity userRightsEntity) : base(id)
        {
            UserProgressEntity = userProgressEntity;
            UserRightsEntity = userRightsEntity;
        }

        public UserProgressEntity UserProgressEntity { get; }

        public UserRightsEntity UserRightsEntity { get; }

        /// <inheritdoc />
        public override string ToString() => $"{base.ToString()}, {nameof(UserProgressEntity)}: {UserProgressEntity}";

        public UserEntity With(UserProgressEntity userProgress, UserRightsEntity userRightsEntity)
            => new UserEntity(Id, userProgress, userRightsEntity);
    }
}
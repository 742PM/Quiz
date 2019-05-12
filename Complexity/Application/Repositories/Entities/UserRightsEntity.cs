using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class UserRightsEntity: Entity
    {
        private UserRightsEntity(Guid id,
            bool canPlayOnService, 
            bool canGetGeneratorsWithLevels, 
            bool canEditLevels, 
            bool canEditTopics, 
            bool canEditGenerators, 
            bool canRenderTasks) : base(id)
        {
            CanPlayOnService = canPlayOnService;
            CanGetGeneratorsWithLevels = canGetGeneratorsWithLevels;
            CanEditLevels = canEditLevels;
            CanEditTopics = canEditTopics;
            CanEditGenerators = canEditGenerators;
            CanRenderTasks = canRenderTasks;
        }

        public bool CanPlayOnService { get; }
        public bool CanGetGeneratorsWithLevels { get; }
        public bool CanEditLevels { get; }
        public bool CanEditTopics { get; }
        public bool CanEditGenerators { get; }
        public bool CanRenderTasks { get; }

        public static UserRightsEntity CreatePlayerRights()
        {
            return new UserRightsEntity(Guid.NewGuid(),
                true, 
                false,
                false, 
                false, 
                false,
                false);
        }
        
        public static UserRightsEntity CreateAdminRights()
        {
            return new UserRightsEntity(Guid.NewGuid(),
                true, 
                true,
                true, 
                true, 
                true,
                true);
        }
    }
}
using System;
using System.Collections.Generic;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class UserRightsEntity : Entity
    {
        private UserRightsEntity(Guid id,
            bool canPlayInService,
            bool canGetGeneratorsWithLevels,
            bool canEditLevels,
            bool canEditTopics,
            bool canEditGenerators,
            bool canRenderTasks) : base(id)
        {
            CanPlayInService = canPlayInService;
            CanGetGeneratorsWithLevels = canGetGeneratorsWithLevels;
            CanEditLevels = canEditLevels;
            CanEditTopics = canEditTopics;
            CanEditGenerators = canEditGenerators;
            CanRenderTasks = canRenderTasks;
        }

        public List<string> GetRights()
        {
            var rights = new List<string>();
            
            if (CanPlayInService) rights.Add("canPlayInService");
            if (CanEditTopics) rights.Add("canEditTopics");
            if (CanEditLevels) rights.Add("canEditLevels");
            if (CanEditGenerators) rights.Add("canEditGenerators");
            if (CanRenderTasks) rights.Add("canRenderTasks");
            if (CanRenderTasks) rights.Add("canRenderTasks");

            return rights;
        }

        public bool CanPlayInService { get; }
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
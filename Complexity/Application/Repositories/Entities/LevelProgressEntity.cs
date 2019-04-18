using System;
using System.Collections.Generic;

namespace Application.Repositories.Entities
{
    public class LevelProgressEntity
    {
        public LevelProgressEntity(Guid levelId, Dictionary<Guid, int> currentLevelStreaks)
        {
            LevelId = levelId;
            CurrentLevelStreaks = currentLevelStreaks;
        }

        public Guid LevelId { get; }

        /// <summary>
        ///     Maps Generator Id to current streak in it
        /// </summary>
        public Dictionary<Guid, int> CurrentLevelStreaks { get;  }
    }
}

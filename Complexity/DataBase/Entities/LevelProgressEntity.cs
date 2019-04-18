using System;
using System.Collections.Generic;

namespace DataBase.Entities
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

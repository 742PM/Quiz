using System;
using System.Collections.Generic;

namespace DataBase.Entities
{
    public class LevelProgressEntity
    {
        public LevelProgressEntity(Dictionary<Guid, int> currentLevelStreaks, Guid levelId)
        {
            CurrentLevelStreaks = currentLevelStreaks;
            LevelId = levelId;
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public Guid LevelId { get; private set; }

        /// <summary>
        ///     Maps Generator Id to current streak in it
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public Dictionary<Guid, int> CurrentLevelStreaks { get; private set; } 
    }
}

using System;
using System.Collections.Generic;

namespace DataBase.Entities
{
    public class LevelProgressEntity
    {
        public Guid LevelId { get; set; }

        /// <summary>
        ///     Maps Generator Id to current streak in it
        /// </summary>
        public Dictionary<Guid, int> CurrentLevelStreaks { get; set; }
    }
}

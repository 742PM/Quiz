using System;
using System.Collections.Generic;
using DataBase.DatabaseEntities.GeneratorEntities;

namespace DataBase.DatabaseEntities
{
    public class LevelProgressEntity
    {
        public Guid LevelId { get; set; }

        /// <summary>
        /// Maps Generator Id to current streak in it
        /// </summary>
        public Dictionary<Guid,int> CurrentLevelStreaks { get; set; }
    }
}

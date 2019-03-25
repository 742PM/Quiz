using System;
using DataBase.DatabaseEntities.GeneratorEntities;

namespace DataBase.DatabaseEntities
{
    public class LevelProgressEntity
    {
        public Guid LevelId { get; set; }

        public (GeneratorEntity, int)[] CurrentLevelStreaks { get; set; }
    }
}

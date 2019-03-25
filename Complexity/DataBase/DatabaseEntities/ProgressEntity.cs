using System;

namespace DataBase.DatabaseEntities
{
    public class ProgressEntity
    {
        public Guid CurrentTopicId { get; set; }
        public Guid CurrentLevelId { get; set; }
        public Guid[] TopicsId { get; set; }
        public LevelProgressEntity[] LevelsProgress { get; set; }

        public TaskInfoEntity CurrentTask { get; set; }
    }
}

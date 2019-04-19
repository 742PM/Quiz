﻿using System;
using System.Collections.Generic;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class LevelProgressEntity  : Entity
    {
        public LevelProgressEntity(Guid levelId, Dictionary<Guid, int> currentLevelStreaks, Guid id) : base(id)
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

﻿using System;

namespace Domain
{
    /// <summary>
    /// Subtopic for some topic
    /// </summary>
    [MustBeSaved]
    [Entity] //Should it be Entity?
    public class Level : Entity<Guid>
    {
        public Level(Guid id, string description, (TaskGenerator,int)[] generators) : base(id)
        {
            Description = description;
            Generators = generators;
        }

        public string Description { get; }

        public (TaskGenerator generator, int streak)[] Generators { get; }

    }
}
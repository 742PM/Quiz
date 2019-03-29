using System;
using System.Collections.Generic;

namespace Infrastructure.DDD
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class EntityAttribute : Attribute
    {
    }

    /// <summary>
    ///     Базовый класс всех DDD сущностей.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    public abstract class Entity<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        [MustBeSaved]
        public TId Id
        {
            get; // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local For MongoDB
            private set;
        }

        protected bool Equals(Entity<TId> other) => EqualityComparer<TId>.Default.Equals(Id, other.Id);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity<TId>) obj);
        }

        public override int GetHashCode() => EqualityComparer<TId>.Default.GetHashCode(Id);

        public override string ToString() => $"{GetType().Name}({nameof(Id)}: {Id})";
    }
}

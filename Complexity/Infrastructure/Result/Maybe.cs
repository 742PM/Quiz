using System;

namespace Infrastructure.Result
{
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly MaybeValueWrapper value;
        public T Value => HasNoValue ? throw new InvalidOperationException() : value.Value;

        public static Maybe<T> None => new Maybe<T>();

        public bool HasValue => value != null;
        public bool HasNoValue => !HasValue;

        private Maybe(T value)
        {
            this.value = value == null? null : new MaybeValueWrapper(value);
        }

        public static implicit operator Maybe<T>(T value)
        {
            if (value?.GetType() == typeof(Maybe<T>)) return (Maybe<T>) (object) value;

            return new Maybe<T>(value);
        }

        public static Maybe<T> From(T obj) => new Maybe<T>(obj);

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            if (value is Maybe<T>)
                return maybe.Equals(value);

            return !maybe.HasNoValue && maybe.Value.Equals(value);
        }

        public static bool operator !=(Maybe<T> maybe, T value) => !(maybe == value);

        public static bool operator ==(Maybe<T> first, Maybe<T> second) => first.Equals(second);

        public static bool operator !=(Maybe<T> first, Maybe<T> second) => !(first == second);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(Maybe<T>))
            {
                if (obj is T obj1 ) obj = new Maybe<T>(obj1);

                if (!(obj is Maybe<T>))
                    return false;
            }

            var other = (Maybe<T>) obj;
            return Equals(other);
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
                return true;

            if (HasNoValue || other.HasNoValue)
                return false;

            return value.Value.Equals(other.value.Value);
        }

        public override int GetHashCode()
        {
            return HasNoValue ? 0 : value.Value.GetHashCode();
        }

        public override string ToString()
        {
            return HasNoValue ? "No value" : Value.ToString();
        }

        private class MaybeValueWrapper
        {
            internal readonly T Value;

            public MaybeValueWrapper(T value)
            {
                Value = value;
            }
        }
    }
}

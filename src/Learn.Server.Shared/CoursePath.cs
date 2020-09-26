using Orleans.Concurrency;
using System;

namespace Learn.Server.Shared
{
    [Immutable]
    public sealed class CoursePath : IEquatable<CoursePath>
    {
        public CoursePath(Guid key, string name, string slug, Guid version)
        {
            Key = key;
            Name = name;
            Slug = slug;
            Version = version;
        }

        public Guid Key { get; }

        public string Name { get; }

        public string Slug { get; }

        public Guid Version { get; }

        public CoursePath WithVersion(Guid version)
        {
            return new CoursePath(Key, Name, Slug, version);
        }

        #region IEquatable

        public bool Equals(CoursePath other)
        {
            if (other is null) return false;

            return other.Key == Key
                && other.Name == Name
                && other.Slug == Slug
                && other.Version == Version;
        }

        public override bool Equals(object obj)
        {
            return obj is CoursePath other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Name, Slug, Version);
        }

        #endregion IEquatable
    }
}
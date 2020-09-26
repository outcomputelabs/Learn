using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Learn.Server.Data.Exceptions
{
    [Serializable]
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "Unused")]
    public class SlugAlreadyExistsException : Exception
    {
        public SlugAlreadyExistsException(string slug) : base($"Slug '{slug}' already exists.", null)
        {
            Slug = slug;
        }

        protected SlugAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Slug = info.GetString(nameof(Slug));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Slug), Slug);
        }

        public string Slug { get; }
    }
}
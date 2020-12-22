using System;
using System.Runtime.Serialization;
using static System.String;

namespace Learn.Server.Data.Exceptions
{
    [Serializable]
    public class SlugAlreadyExistsException : Exception
    {
        public SlugAlreadyExistsException(string slug) : base($"Slug '{slug}' already exists.", null)
        {
            Slug = slug;
        }

        protected SlugAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Slug = info.GetString(nameof(Slug)) ?? Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Slug), Slug);
        }

        public string Slug { get; }
    }
}
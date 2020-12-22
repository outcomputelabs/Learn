using System;
using System.Runtime.Serialization;

namespace Learn.Server.Data.Exceptions
{
    [Serializable]
    public class KeyAlreadyExistsException : Exception
    {
        public KeyAlreadyExistsException(Guid key) : base($"Key '{key}' already exists.")
        {
            Key = key;
        }

        protected KeyAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Key = (Guid)(info.GetValue(nameof(Key), typeof(Guid)) ?? Guid.Empty);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Key), Key);
        }

        public Guid Key { get; }
    }
}
using System;
using System.Runtime.Serialization;

namespace Learn.Server.Data.Exceptions
{
    [Serializable]
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
        {
        }

        public ConcurrencyException(string message) : base(message)
        {
        }

        public ConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConcurrencyException(Guid? storedVersion, Guid? currentVersion) : base(null, null)
        {
            StoredVersion = storedVersion;
            CurrentVersion = currentVersion;
        }

        protected ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            StoredVersion = (Guid?)info.GetValue(nameof(StoredVersion), typeof(Guid?));
            CurrentVersion = (Guid?)info.GetValue(nameof(CurrentVersion), typeof(Guid?));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(StoredVersion), StoredVersion);
            info.AddValue(nameof(CurrentVersion), CurrentVersion);
        }

        public Guid? StoredVersion { get; }

        public Guid? CurrentVersion { get; }
    }
}
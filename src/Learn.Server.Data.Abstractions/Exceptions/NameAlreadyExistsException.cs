using System;
using System.Runtime.Serialization;
using static System.String;

namespace Learn.Server.Data.Exceptions
{
    [Serializable]
    public class NameAlreadyExistsException : Exception
    {
        public NameAlreadyExistsException(string name) : base($"Name '{name}' already exists.", null)
        {
            Name = name;
        }

        protected NameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = info.GetString(nameof(Name)) ?? Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Name), Name);
        }

        public string Name { get; }
    }
}
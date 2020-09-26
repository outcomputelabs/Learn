using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Learn.Server.Data.Exceptions
{
    [Serializable]
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "Unused")]
    public class NameAlreadyExistsException : Exception
    {
        public NameAlreadyExistsException(string name) : base($"Name '{name}' already exists.", null)
        {
            Name = name;
        }

        protected NameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = info.GetString(nameof(Name));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Name), Name);
        }

        public string Name { get; }
    }
}
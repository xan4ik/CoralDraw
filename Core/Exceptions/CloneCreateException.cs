using System;

namespace Core
{
    // TODO: Check this
    public class CloneCreateException : Exception 
    {
        public CloneCreateException(Type type, Type prototype ) : base($"Type: {type.ToString()} have to implement IPrototype<{prototype.ToString()}> interface")
        {
            RequiredPrototype = prototype;            
            Type = type;
        }

        public readonly Type Type;
        public readonly Type RequiredPrototype;
    }



}

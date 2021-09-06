using System;

namespace Core
{
    // TODO: Composite
    public class CloneCreateException : Exception 
    {
        public CloneCreateException(Type type, Type prototype ):
            base($"Type {type.ToString()} have to implement IPrototype<{prototype.ToString()}> interface")
        {
            Type = type;
            PrototypeType = prototype;
        }

        public readonly Type Type;
        public readonly Type PrototypeType;
    }



}

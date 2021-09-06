using System;

namespace UseCases
{
    [AttributeUsage(AttributeTargets.Class)]
    class FactoryKeyAttribute : Attribute 
    {
        public readonly string Key;

        public FactoryKeyAttribute(string key)
        {
            this.Key = key;
        }
    
    }
}

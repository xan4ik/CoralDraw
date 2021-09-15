using System;

namespace UseCases
{
    [AttributeUsage(AttributeTargets.Class)]
    class CreatorKeyAttribute : Attribute 
    {
        public readonly string Key;

        public CreatorKeyAttribute(string key)
        {
            this.Key = key;
        }
    
    }
}

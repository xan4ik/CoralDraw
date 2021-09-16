using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UseCases
{
    public class AutoStringAssemblyInit<Creator> : IFactoryInitializer<string, Creator>
    { //TODO: Handle Exception, FiX
        public void Init(IFactory<string, Creator> factory)
        {
            var factories = GetCreatorTypes().ToArray();
            foreach (var factoryClass in factories)
            {
                if (CanHandleType(factoryClass))
                {
                    var key = GetKeyOf(factoryClass);
                    var instance = CreateInstanceOf(factoryClass);
                    factory.AddCreator(key, (Creator)instance);
                }
            }
        }

        private IEnumerable<Type> GetCreatorTypes() 
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsAssignableFrom(typeof(Creator)));
        }

        private bool CanHandleType(Type type) 
        {
            return type.GetCustomAttribute<CreatorKeyAttribute>(true) != null; 
        }

        private string GetKeyOf(Type type) 
        {
            return type.GetCustomAttribute<CreatorKeyAttribute>(true).Key;
        }

        private object CreateInstanceOf(Type type)
        {
            var defaultCtor = type.GetConstructor(null);
            return defaultCtor.Invoke(null);
        }
    }
}

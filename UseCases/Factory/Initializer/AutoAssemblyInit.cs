using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UseCases
{
    public class AutoStringAssemblyInit<Factory> : IFactoryInitializer<string, Factory>
    { //TODO: Handle Exception
        public void Init(IFactory<string, Factory> factory)
        {
            var factories = GetCreatorTypes();
            foreach (var factoryClass in factories)
            {
                if (CanHandleType(factoryClass))
                {
                    var key = GetKeyOf(factoryClass);
                    var instance = CreateInstanceOf(factoryClass);
                    factory.AddFactory(key, (Factory)instance);
                }
            }
        }

        private IEnumerable<Type> GetCreatorTypes() 
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsAssignableFrom(typeof(Factory)));
        }

        private bool CanHandleType(Type type) 
        {
            return type.GetCustomAttribute<FactoryKeyAttribute>(false) != null; 
        }

        private string GetKeyOf(Type type) 
        {
            return type.GetCustomAttribute<FactoryKeyAttribute>(false).Key;
        }

        private object CreateInstanceOf(Type type)
        {
            var defaultCtor = type.GetConstructor(null);
            return defaultCtor.Invoke(null);
        }
    }
}

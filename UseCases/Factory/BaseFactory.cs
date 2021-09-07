using System;
using System.Collections.Generic;

namespace UseCases
{
    public abstract class BaseFactory<Key, Factory> : IFactory<Key, Factory>
    {
        private Dictionary<Key, Factory> creators;

        protected BaseFactory(IFactoryInitializer<Key, Factory> initializer)
        {
            creators = new Dictionary<Key, Factory>();
            initializer.Init(this);
        }

        public Factory GetCreator(Key key) 
        {
            if (IsFactoryExist(key))
            {
                return creators[key];
            }
            else throw new ArgumentException($"Not valid key - {key}");
        }

        public void AddCreator(Key key, Factory factory)
        {
            if (IsFactoryNotExist(key))
            {
                creators.Add(key, factory);
            }
            else throw new ArgumentException($"Factory with key - {key} already exist");
        }

        private bool IsFactoryNotExist(Key key)
        {
            return !creators.ContainsKey(key);
        }

        public void RemoveCreator(Key key)
        {
            if (IsFactoryExist(key))
            {
                creators.Remove(key);
            }
            else throw new ArgumentException($"Factory with key - {key} is not exist");
        }

        private bool IsFactoryExist(Key key)
        {
            return creators.ContainsKey(key);
        }

        public IEnumerable<Key> GetFactoriesKeys()
        {
            return creators.Keys;
        }
    }
}

using System;
using System.Collections.Generic;

namespace UseCases
{
    public abstract class BaseFactory<Key, Creator> : IFactory<Key, Creator>
    {
        private Dictionary<Key, Creator> creators;

        protected BaseFactory(IFactoryInitializer<Key, Creator> initializer)
        {
            creators = new Dictionary<Key, Creator>();
            initializer.Init(this);
        }

        public Creator GetCreator(Key key) 
        {
            if (IsFactoryExist(key))
            {
                return creators[key];
            }
            else throw new ArgumentException($"Not valid key - {key}");
        }

        public void AddCreator(Key key, Creator factory)
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

        public IEnumerable<Key> GetFactoryKeys()
        {
            return creators.Keys;
        }
    }
}

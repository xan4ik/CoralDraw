using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases
{
    public class EmptyInit<Key, Factory> : IFactoryInitializer<Key, Factory>
    {
        public void Init(IFactory<Key, Factory> factory)
        {
            return;
        }
    }
}

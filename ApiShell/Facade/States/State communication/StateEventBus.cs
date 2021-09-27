using System;
using System.Collections.Generic;

namespace ApiShell
{
    internal class StateEventBus
    {
        class StateEvent<T> 
        {
            public event Action<T> Publisher;
            public void Invoke(T eventArgs) 
            {
                if (!(Publisher is null)) 
                {
                    Publisher.Invoke(eventArgs);
                }
            }
        }

        private List<object> events;
        public StateEventBus()
        {
            events = new List<object>();
        }

        public void CreateEventOf<T>() 
        {
            if (ContainsEvent<T>())
            {
                throw new ArgumentException($"System already contains event for <{typeof(T)}>");
            }
            events.Add(new StateEvent<T>());
        }

        public bool ContainsEvent<T>() 
        {
            foreach (var element in events)
            {
                if (element is StateEvent<T>) 
                {
                    return true;
                }
            }
            return false;
        }

        public void Publish<T>(T eventArgs) 
        {
            var _event = GetEventFor<T>();
            _event.Invoke(eventArgs);
        }

        public void UnsubscribeFromPublisher<T>(Action<T> action) 
        {
            var _event = GetEventFor<T>();
            _event.Publisher -= action;
        }

        public void SubscribeToPublisher<T>(Action<T> action) 
        {
            var _event = GetEventFor<T>();
            _event.Publisher += action;
        }

        private StateEvent<T> GetEventFor<T>() 
        {
            foreach (var element in events)
            {
                if (element is StateEvent<T> _event)
                {
                    return _event;
                }
            }
            throw new ArgumentException($"Event for <{typeof(T)}> don't exist");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiShell
{
    internal class State 
    {
        private IStateHandler[] handlers;

        public State(string name, params IStateHandler[] handlers)
        {
            this.StateName = name;
            this.handlers = handlers;
        }

        public string StateName { get; private set; }
        
        public bool ContainsHandler<T>() where T : IStateHandler
        {
            foreach (var handler in handlers)
            {
                if (handler is T) 
                {
                    return true;
                }
            }
            return false;
        }

        public T GetHandler<T>() where T: IStateHandler
        {
            foreach (var handler in handlers)
            {
                if (handler is T required)
                {
                    return required;
                }
            }
            throw new HandlerNotFoundException<T>();
        }

        public IEnumerable<T> GetHandlers<T>() where T : IStateHandler 
        {
            return (IEnumerable<T>)handlers.Where(handler => handler is T);
        }
    }

    public class HandlerNotFoundException<T> : Exception
    {
        public HandlerNotFoundException() : base($"Required type \"{typeof(T).ToString()}\" is't exist")
        { }
    }
}

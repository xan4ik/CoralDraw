using System;
using System.Collections.Generic;
using System.Text;

namespace ApiShell
{
    public struct CompositeEventArgs
    {
        public enum EventType 
        {
            Group,
            Ungroup
        }

        public EventType Type;
    }
}

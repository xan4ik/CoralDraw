using System;
using System.Collections.Generic;
using System.Text;

namespace ApiShell
{
    public partial class Redactor
    {
        private class StateMemento
        {
            private FacadeState lastState;
            public StateMemento(FacadeState lastState)
            {
                this.lastState = lastState;
            }

            public void RestoreStateFor(Redactor facade) 
            {
                facade.state = lastState;
            }
        }
    }
}

using Core;
using System.Collections.Generic;

namespace ApiShell
{
    public partial class Redactor
    {
        internal CommandHistory History;
        internal List<IFigure> Figures;
        private State lastActiveState;
        private State currentState;

        public Redactor()
        {
            History = new CommandHistory();
            Figures = new List<IFigure>();
            currentState = new State("Creation",
                new CreateStateEventHandler(),
                new DefaultDrawEventHadler()
             );
            lastActiveState = new State("Selection",
                new SelectStateEventHandler()
            );
        }

        public string ActiveStateName() 
        {
            return currentState.StateName;
        }

        public void SwapState()
        {
            var currentState = this.currentState;
            this.currentState = lastActiveState;
            lastActiveState = currentState;
        }

        public void InvokeEventFor<T>(T args)
        {
            currentState
                .GetHandler<IStateHandler<T>>()
                .Handle(args, this);
        }
    }
}

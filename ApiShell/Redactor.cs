using Core;
using System.Collections.Generic;

namespace ApiShell
{
    public partial class Redactor
    {
        private RedactorCore core;
        private State lastActiveState;
        private State currentState;

        public Redactor()
        {
            core = new RedactorCore();
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
        public void UndoLastAction() 
        {
            core.History.UndoLastCommand();
        }
        
        public void InvokeHandlerFor<T>(T args)
        {
            currentState
                .GetHandler<IStateHandler<T>>()
                .Handle(args, core);
        }

        public void SwapState()
        {
            var activeState = currentState;
            currentState = lastActiveState;
            lastActiveState = activeState;
        }
    }

    internal class RedactorCore 
    {
        internal CommandHistory History;
        internal List<IFigure> Figures;
        internal StateEventBus EventBus;

        public RedactorCore()
        {
            History = new CommandHistory();
            Figures = new List<IFigure>();
            EventBus = new StateEventBus();
        }
    }
}

using Core;
using System;
using UseCases;

namespace ApiShell
{
    internal class CompositeSaver : IStateHandler<CompositeEventArgs> 
    {
        private IFigure lastSelectedFigure;

        public CompositeSaver()
        {
            lastSelectedFigure = DummyFigure.GetInstance();
        }

        public void OnSelectedFigureUpdate(IFigure figure) 
        {
            lastSelectedFigure = figure;
        }

        public void Handle(CompositeEventArgs args, RedactorCore core)
        {
            if (lastSelectedFigure is IComposite<IFigure> composite)
            {
                HandleCompositeEvent(composite, args, core);
            }
            else throw new InvalidCastException("You are truing to group not complex figure!");
        }

        private void HandleCompositeEvent(IComposite<IFigure> composite, CompositeEventArgs args, RedactorCore core)
        {
            switch (args.Type)
            {
                case (CompositeEventArgs.EventType.Group):
                    {
                        core.History.ExecuteCommand(
                                new GroupCommand(core.Figures, composite));
                        break;
                    }
                case (CompositeEventArgs.EventType.Ungroup):
                    {
                        core.History.ExecuteCommand(
                                new UngroupCommand(core.Figures, composite));
                        break;
                    }
            }
        }

        void IStateHandler.LateInit(RedactorCore core)
        {
            core.EventBus.SubscribeToPublisher<IFigure>(OnSelectedFigureUpdate);
        }

        void IStateHandler.Init(RedactorCore core)
        {
            return;
        }
    }
}

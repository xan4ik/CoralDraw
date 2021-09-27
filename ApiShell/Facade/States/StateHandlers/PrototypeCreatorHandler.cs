using Core;
using System;
using UseCases;

namespace ApiShell
{
    internal class PrototypeCreatorHandler : IStateHandler<string>
    {
        private IFigure lastSelectFigure;

        public PrototypeCreatorHandler()
        {
            lastSelectFigure = DummyFigure.GetInstance();
        }
        void IStateHandler.Init(RedactorCore core)
        {
            core.EventBus.CreateEventOf<CreatorEventArgs>();
        }

        void IStateHandler.LateInit(RedactorCore core)
        {
            core.EventBus.SubscribeToPublisher<IFigure>(OnSelectedFigureUpdate);
        }

        private void OnSelectedFigureUpdate(IFigure figure)
        {
            lastSelectFigure = figure;   
        }

        public void Handle(string args, RedactorCore core)
        {
            if (lastSelectFigure is IPrototype<IFigure> prototype) 
            {
                var eventArgs = new CreatorEventArgs() 
                {
                    Key = args,
                    Creator = new PrototypeCreator(prototype)
                };
                core.EventBus.Publish<CreatorEventArgs>(eventArgs);
            }
            else throw new ArgumentException("Trying create creator for not IPrototype<IFigure>");
        }
    }
}

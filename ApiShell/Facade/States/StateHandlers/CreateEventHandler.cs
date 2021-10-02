using Core;
using System;
using UseCases;

namespace ApiShell
{
    internal class CreateEventHandler :
        IStateHandler<ChangeCreatorEventArgs>,
        IStateHandler<MouseEventArgs>,
        IStateHandler<Color>
    {
        private readonly IFactory<string, IFigureCreator> figureFactory;
        private readonly IFactory<string, IDrawerCreator> drawerFactory;

        private IFigureCreator figureCreator;
        private IDrawerCreator drawerCreator;
        private Color color;
        private Point lastTouch;

        public CreateEventHandler()
        {
            drawerFactory = new DrawerVisitorFactory(new StaticInitDrawerVisitorFactory());
            figureFactory = new FigureFactory(new StaticInitFigureFactory());
        }

        public void Handle(Color args, RedactorCore core)
        {
            this.color = args;
        }

        public void Handle(MouseEventArgs args, RedactorCore core)
        {
            switch (args.Type)
            {
                case (ClickType.Down):
                    {
                        OnMouseDown(args, core);
                        break;
                    }
                case ClickType.Up:
                    {
                        OnMouseUp(args, core);
                        break;
                    }
            }
        }

        public void Handle(ChangeCreatorEventArgs args, RedactorCore core)
        {
            if (args.IsFigureFactory)
            {
                figureCreator = figureFactory.GetCreator(args.Key);
            }
            else
            {
                drawerCreator = drawerFactory.GetCreator(args.Key);
            }
        }

        private void OnMouseDown(MouseEventArgs args, RedactorCore core)
        {
            lastTouch = args.Touch;
        }

        private void OnMouseUp(MouseEventArgs args, RedactorCore core)
        {
            var figure = CreateFigure(args.Touch);
            core.History.ExecuteCommand(
                    new FigureCreateCommand(core.Figures, figure)
                );
        }

        private IFigure CreateFigure(Point touch)
        {
            var snapshot = CreateFigureSnapshot(lastTouch, touch);
            var drawer = drawerCreator.CreateDrawer(color);
            return  figureCreator.CreateFigure(drawer, snapshot);
        }

        private Snapshot CreateFigureSnapshot(Point pointDown, Point pointUp)
        {
            var location = new Point()
            {
                X = Math.Min(pointDown.X, pointUp.X),
                Y = Math.Min(pointDown.Y, pointUp.Y)
            };
            var size = new Size()
            {
                Width = Math.Abs(pointDown.X - pointUp.X),
                Height = Math.Abs(pointDown.Y - pointUp.Y)
            };
            return new Snapshot(location, size);
        }
        
        private void OnCreatorEventHandler(CreatorEventArgs args) 
        {
            args.Core.History.ExecuteCommand(
                new CreatePrototypeFactory(figureFactory, args.Key, args.Creator)
            );
        }


        void IStateHandler.LateInit(RedactorCore core)
        {
            core.EventBus.SubscribeToPublisher<CreatorEventArgs>(OnCreatorEventHandler);
        }

        void IStateHandler.Init(RedactorCore redactor)
        {
            return;
        }
    }
}

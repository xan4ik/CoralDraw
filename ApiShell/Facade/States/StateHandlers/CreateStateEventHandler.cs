using Core;
using System;
using UseCases;

namespace ApiShell
{
    internal class CreateStateEventHandler :
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

        public CreateStateEventHandler()
        {
            drawerFactory = new DrawerVisitorFactory(new StaticInitDrawerVisitorFactory());
            figureFactory = new FigureFactory(new StaticInitFigureFactory());
        }

        public void Handle(Color args, Redactor redactor)
        {
            this.color = args;
        }

        public void Handle(MouseEventArgs args, Redactor redactor)
        {
            switch (args.Type)
            {
                case (ClickType.Down):
                    {
                        OnMouseDown(args, redactor);
                        break;
                    }
                case ClickType.Up:
                    {
                        OnMouseUp(args, redactor);
                        break;
                    }
            }
        }

        public void Handle(ChangeCreatorEventArgs args, Redactor redactor)
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

        private void OnMouseDown(MouseEventArgs args, Redactor redactor)
        {
            lastTouch = args.Touch;
        }

        private void OnMouseUp(MouseEventArgs args, Redactor redactor)
        {
            var figure = CreateFigure(args.Touch);
            redactor.Figures.Add(figure); // TODO: maybe internal method
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
        
        void IStateHandler.Handle(object args, Redactor redactor)
        {
            throw new NotImplementedException();
        }

    }
}

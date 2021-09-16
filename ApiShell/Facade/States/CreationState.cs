using Core;
using System;
using UseCases;

namespace ApiShell
{

    public partial class Redactor
    {
        private class CreationState : FacadeState
        {
            private readonly IFactory<string, IFigureCreator> figureFactory;
            private readonly IFactory<string, IDrawerCreator> drawerFactory;

            private IFigureCreator figureCreator;
            private IDrawerCreator drawerCreator;
            private Point pointDown;
            private Color color;

            public CreationState(IFactory<string, IFigureCreator> figureFactory, 
                                 IFactory<string, IDrawerCreator> drawerFactory, Redactor parent) : base(parent)
            {
                this.figureFactory = figureFactory;
                this.drawerFactory = drawerFactory;
            }

            public override void Draw(IDrawerAdapter adapter)
            {
                foreach (var figure in parent.figures)
                {
                    figure.Draw(adapter);
                }
            }

            public override void MouseDown(Point point)
            {
                pointDown = point;
            }

            public override void MouseUp(Point point)
            {
                var figure = CreateFigure(point);
                var createCommand = new FigureCreateCommand(parent.figures, figure);
                parent.history.ExecuteCommand(createCommand);
            }

            private IFigure CreateFigure(Point pointUp)
            {
                var drawer = drawerCreator.CreateDrawer(color);
                var snapshot = GetFigureSnapshot(pointDown, pointUp);
                return figureCreator.CreateFigure(drawer, snapshot);
            }

            private Snapshot GetFigureSnapshot(Point pointDown, Point pointUp) 
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

            public override void UpdateDrawerCreator(string factoryKey)
            {
                drawerCreator = drawerFactory.GetCreator(factoryKey);
            }

            public override void UpdateFigureCreator(string factoryKey)
            {
                figureCreator = figureFactory.GetCreator(factoryKey);
            }

            public override void ChangeColorTo(Color color)
            {
                this.color = color;
            }

            public override void MouseMove(Point point)
            {
                return;
            }


            #region OtherStateMethods

            public override void KeyDown(Key key)
            {
                throw new NotImplementedException(NotImplementedMessage);
            }

            public override void KeyUp(Key key)
            {
                throw new NotImplementedException(NotImplementedMessage);
            }
            #endregion
        }
    }
}

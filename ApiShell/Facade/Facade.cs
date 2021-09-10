using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public partial class Facade
    {
        private List<IFigure> figures;
        private CommandHistory history;
        private FacadeState state;
    }
     
    public partial class Facade
    {
        private abstract class FacadeState 
        {
            public abstract void MouseDown(Point point, Facade facade);
            public abstract void MouseHold(Point point, Facade facade);
            public abstract void MouseUp(Point point, Facade facade);
            public abstract void KeyDown(string key);
            public abstract void KeyUp(string key);
            public abstract void Draw(IDrawerAdapter adapter, Facade facade);
            public abstract void UpdateFigureCreator(string factoryKey);
            public abstract void UpdateDrawerCreator(string factoryKey);
        }
        private class CreationState : FacadeState
        {
            private readonly IFactory<string, IFigureCreator> figureFactory;
            private readonly IFactory<string, IDrawerCreator> drawerFactory;

            private IFigureCreator figureCreator;
            private IDrawerCreator drawerCreator;
            private Point pointDown;

            public CreationState(IFactory<string, IFigureCreator> figureFactory, 
                                 IFactory<string, IDrawerCreator> drawerFactory)
            {
                this.figureFactory = figureFactory;
                this.drawerFactory = drawerFactory;
            }

            public override void Draw(IDrawerAdapter adapter, Facade facade)
            {
                foreach (var figure in facade.figures)
                {
                    figure.Draw(adapter);
                }
            }

            public override void MouseDown(Point point, Facade facade)
            {
                pointDown = point;
            }

            public override void MouseUp(Point point, Facade facade)
            {
                var figure = CreateFigure(point);
                var createCommand = new FigureCreateCommand(facade.figures, figure);
                facade.history.ExecuteCommand(createCommand);
            }

            private IFigure CreateFigure(Point pointUp)
            {
                var drawer = drawerCreator.CreateDrawer();
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

            public override void KeyDown(string key)
            {
                return;
            }

            public override void KeyUp(string key)
            {
                return;
            }

            public override void MouseHold(Point point, Facade facade)
            {
                return;
            }
        }
        private class SelectionState : FacadeState
        {
            public override void KeyDown(string key)
            {
                throw new NotImplementedException();
            }

            public override void KeyUp(string key)
            {
                throw new NotImplementedException();
            }

            public override void MouseDown(Point point, Facade facade)
            {
                throw new NotImplementedException();
            }

            public override void MouseHold(Point point, Facade facade)
            {
                throw new NotImplementedException();
            }

            public override void MouseUp(Point point, Facade facade)
            {
                throw new NotImplementedException();
            }

            public override void UpdateDrawerCreator(string factoryKey)
            {
                throw new NotImplementedException();
            }

            public override void UpdateFigureCreator(string factoryKey)
            {
                throw new NotImplementedException();
            }
            public override void Draw(IDrawerAdapter adapter, Facade facade)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class FigureCreateCommand : ICommand
    {
        private ICollection<IFigure> figures;
        private IFigure newFigure;

        public FigureCreateCommand(ICollection<IFigure> figures, IFigure newFigure)
        {
            this.figures = figures;
            this.newFigure = newFigure;
        }

        public void Execute()
        {
            figures.Add(newFigure);
        }

        public void Undo()
        {
            figures.Remove(newFigure);
        }
    }



    public interface ISelectOption
    {
        IFigure GetFigureByPoint(IEnumerable<IFigure> figures, Point location);
    }
    public class CtrlPressed : ISelectOption
    {
        private IFigure figure;

        public IFigure GetFigureByPoint(IEnumerable<IFigure> figures, Point location)
        {
            if (figure == null)
            {
                FindFirst(figures, location);
                return figure;
            }

            FindNext(figures, location);
            return figure;
        }

        private void FindFirst(IEnumerable<IFigure> figures, Point location)
        {
            foreach (var figure in figures)
            {
                if (IsTouchFigure(figure, location))
                {
                    this.figure = figure;
                }
            }

            this.figure = null;
        }

        private void FindNext(IEnumerable<IFigure> figures, Point location)
        {
            IEnumerator<IFigure> iterator = figures.GetEnumerator();
            MoveToPrevious(iterator);
            FindFigureAfterPrevious(iterator, location);
        }

        private void MoveToPrevious(IEnumerator<IFigure> figures) 
        {
            while (figures.MoveNext() && figures.Current != figure) 
            {
                continue;
            }
        }

        private void FindFigureAfterPrevious(IEnumerator<IFigure> figures, Point location) 
        {
            while (figures.MoveNext())  // Maybe do/while
            {
                if (IsTouchFigure(figures.Current, location)) 
                {
                    figure = figures.Current;
                    return;
                }
            }
            figure = null;
        }


      

        private bool IsTouchFigure(IFigure figure, Point location)
        {
            return figure.GetFigureSnapshot().ContainsPoint(location);
        }
    }

    public class DefaultOption : ISelectOption
    {
        public IFigure GetFigureByPoint(IEnumerable<IFigure> figures, Point location)
        {
            foreach (IFigure figure in figures)
            {
                if (IsTouchFigure(figure, location))
                {
                    return figure;
                }
            }
            return null;
        }

        private bool IsTouchFigure(IFigure figure, Point location)
        {
            return figure.GetFigureSnapshot().ContainsPoint(location);
        }
    }


    public class ShiftPressed : ISelectOption
    {
        private Composite composite;

        public ShiftPressed()
        {
            composite = new Composite();
        }

        public IFigure GetFigureByPoint(IEnumerable<IFigure> figures, Point location)
        {
            var figure = GetFigure(figures, location);

            if (figure != null)
            {
                if (!composite.Contains(figure))
                {
                    composite.AddFigure(figure);
                }
                else composite.RemoveFigure(figure);
            }
            else
            {
                return null;
            }

            return composite;
        }

        private IFigure GetFigure(IEnumerable<IFigure> figures, Point location)
        {
            foreach (IFigure figure in figures)
            {
                if (IsTouchFigure(figure, location))
                {
                    return figure;
                }
            }
            return null;
        }

        private bool IsTouchFigure(IFigure figure, Point location)
        {
            return figure.GetFigureSnapshot().ContainsPoint(location);
        }
    }
}

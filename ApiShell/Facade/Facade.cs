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

        public void MouseDown(Point point) 
        {
            state.MouseDown(point, this);
        }
        
        public  void MouseHold(Point point)
        {
            state.MouseDown(point, this);
        }
        
        public  void MouseUp(Point point)
        {
            state.MouseUp(point, this);
        }
        
        public void KeyDown(string key)
        {
            state.KeyDown(key, this);
        }
        
        public void KeyUp(string key)
        {
            state.KeyUp(key, this);
        }
        
        public void Draw(IDrawerAdapter adapter)
        {
            state.Draw(adapter, this);
        }

        public void UpdateFigureCreator(string factoryKey)
        {
            state.UpdateFigureCreator(factoryKey);
        }

        public void UpdateDrawerCreator(string factoryKey)
        {
            state.UpdateDrawerCreator(factoryKey);
        }
    }
     
    public partial class Facade
    {
        private abstract class FacadeState 
        {
            public abstract void MouseDown(Point point, Facade facade);
            public abstract void MouseHold(Point point, Facade facade);
            public abstract void MouseUp(Point point, Facade facade);
            public abstract void KeyDown(string key, Facade facade);
            public abstract void KeyUp(string key, Facade facade);
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

            public override void KeyDown(string key, Facade facade)
            {
                return;
            }

            public override void KeyUp(string key, Facade facade)
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
            private FigureManipulator manipulator;
            private ISelectOption selectOption;
            private Point pointDown;
            private bool isMouseDown;

            public override void KeyDown(string key, Facade facade)
            {
                if (key == "Shift")
                {
                    selectOption = new ShiftPressed();
                }
                else if (key == "Ctrl") 
                {
                    selectOption = new CtrlPressed();
                }
            }

            public override void KeyUp(string key, Facade facade)
            {
                selectOption = new DefaultOption();
            }

            //TODO: handle touch
            public override void MouseDown(Point point, Facade facade)
            {
                var selectedFigure = selectOption.GetFigureByPoint(facade.figures, point);

                if (selectedFigure != null)
                {
                    manipulator.AttachTo(selectedFigure);
                }

                if (!manipulator.GetFigureSnapshot().ContainsPoint(point))
                {
                    manipulator.Detach();
                }
                pointDown = point;
                isMouseDown = true;
            }

            public override void MouseHold(Point point, Facade facade)
            {
                if (isMouseDown) 
                {
                    float dx = point.X - pointDown.X;
                    float dy = point.Y - pointDown.Y;
                    
                    manipulator.Drag(dx, dy);

                    pointDown = point;
                }
            }

            public override void MouseUp(Point point, Facade facade)
            {
                manipulator.Detach();
                isMouseDown = false;
            }

            public override void UpdateDrawerCreator(string factoryKey)
            {
                return;
            }

            public override void UpdateFigureCreator(string factoryKey)
            {
                return;
            }
            public override void Draw(IDrawerAdapter adapter, Facade facade)
            {
                foreach (var figure in facade.figures) 
                {
                    figure.Draw(adapter);
                }
                manipulator.Draw(adapter);
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
            while (figures.MoveNext())  //TODO: Maybe do/while
            {
                if (IsTouchFigure(figures.Current, location)) 
                {
                    figure = figures.Current;
                    return;
                }
            }
            figure = null;
        }


      
        //TODO: удОли это из всех классов
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

    //TODO: Fix all ISelectOption
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

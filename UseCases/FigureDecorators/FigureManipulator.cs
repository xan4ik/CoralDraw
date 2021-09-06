using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases
{
    public class FigureManipulator : IFigure, IToucheable
    {
        private class DummyFigure : IFigure
        {
            public void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
            {
                return;
            }

            public Snapshot GetFigureSnapshot()
            {
                return default;
            }

            public void Move(float deltaX, float deltaY)
            {
                return;
            }

            public void Resize(float deltaWigth, float deltaHeight)
            {
                return;
            }
        }
        private readonly DummyFigure dummy = new DummyFigure();

        private Dictionary<Corner, ITouchAction> handlers;
        private IFigure attachedFigure;

        public FigureManipulator()
        {
            handlers = new Dictionary<Corner, ITouchAction>()
            {


            };
        }

        public void AttachTo(IFigure figure) 
        {
            attachedFigure = figure;
        }

        public void Detach() 
        {
            attachedFigure = dummy;
        }

        public Snapshot GetFigureSnapshot()
        {
            return attachedFigure.GetFigureSnapshot();
        }

        public void Move(float deltaX, float deltaY)
        {
            return;
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            return;
        }

        public void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            attachedFigure.Draw(adapter, visitor);
        }

        public void HandleTouch(Point point)
        {
            
        }
    }

    public interface IToucheable
    {
        void HandleTouch(Point point);
    }

    public enum Corner 
    {
        Highleft,
        HighRight,
        LowLeft,
        LowRight,
        Center,
        None
    }

    public interface ITouchAction 
    {
        Corner Corner { get; }
        bool IsTouch(IFigure figure);
        void Handle(IFigure figure, float deltaX, float deltaY);
    }
}

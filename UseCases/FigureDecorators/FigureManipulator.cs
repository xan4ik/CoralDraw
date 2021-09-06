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
            public void Draw(IDrawerAdapter adapter)
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
        private static readonly DummyFigure dummy;

        private Dictionary<Corner, ITouchAction> handlers;
        private IFigure attachedFigure;

        static FigureManipulator() 
        {
            dummy = new DummyFigure();
        }

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

        public void Draw(IDrawerAdapter adapter)
        {
            attachedFigure.Draw(adapter);
        }

        public void HandleTouch(Point point)
        {
            
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            return;
        }
    }

    public interface IToucheable
    {
        void HandleTouch(Point point);
    }

    public enum Corner 
    {
        HighLeft,
        HighRight,
        LowLeft,
        LowRight,
        Center,
        None
    }

    public interface ITouchAction 
    {
        Corner Corner { get; }
        bool IsTouch(Snapshot pose, Point touch);
        void Handle(IFigure figure, float deltaX, float deltaY);
    }

    class HighLeftTouchHandler : ITouchAction
    {
        public Corner Corner 
        {
            get 
            {
                return Corner.HighLeft;
            }
        }

        public bool IsTouch(IFigure figure)
        {
            return true;
        }

        public void Handle(IFigure figure, float deltaX, float deltaY)
        {
            throw new NotImplementedException();
        }
    }
}

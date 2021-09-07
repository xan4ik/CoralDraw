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

        private Dictionary<Corner, IDragAction> handlers;
        private IFigure attachedFigure;
        private Corner activeHandler;

        static FigureManipulator()
        {
            dummy = new DummyFigure();
        }

        public FigureManipulator()
        {
            var handlerSize = new Size(5, 5);
            handlers = new Dictionary<Corner, IDragAction>()
            {
                { Corner.HighRight, new HighRightHandler(handlerSize) },
                { Corner.LowRight, new LowRightHandler(handlerSize) },
                { Corner.HighLeft, new HighLeftHandler(handlerSize) },
                { Corner.LowLeft, new LowLeftHandler(handlerSize) },
                { Corner.Center, new CenterHandler(handlerSize) },
                { Corner.None, new DummyHandler() },
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
            attachedFigure.Move(deltaX, deltaY);
        }


        public void Resize(float deltaWigth, float deltaHeight)
        {
            attachedFigure.Resize(deltaWigth, deltaHeight);
        }

        public void HandleTouch(Point touch)
        {
            var figureSnapshot = attachedFigure.GetFigureSnapshot();
            foreach (var handler in handlers.Values)
            {
                if (handler.IsTouch(figureSnapshot, touch)) 
                {
                    activeHandler = handler.Corner;
                    return;
                }
            }
        }

        public void Drag(float deltaX, float deltaY)
        {
            handlers[activeHandler].Handle(attachedFigure, deltaX, deltaY);
        }

        public void Draw(IDrawerAdapter adapter)
        {
            attachedFigure.Draw(adapter);
            
        }
    }

    public interface IToucheable
    {
        void HandleTouch(Point point);
        void Drag(float deltaX, float deltaY);
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
}

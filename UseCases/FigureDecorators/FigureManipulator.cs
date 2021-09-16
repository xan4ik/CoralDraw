using Core;
using System.Collections.Generic;

namespace UseCases
{
    public enum Corner
    {
        HighLeft,
        HighRight,
        LowLeft,
        LowRight,
        Center,
        None
    }

    public class FigureManipulator : IFigure, IToucheable
    {
        private Dictionary<Corner, TouchHandler> handlers;
        private IFigure attachedFigure;
        private Corner activeHandler;

        public FigureManipulator()
        {
            var handlerSize = new Size(10, 10);
            handlers = new Dictionary<Corner, TouchHandler>()
            {
                { Corner.HighRight, new TouchHandler(new HighRightHandler(handlerSize), Corner.HighRight) },
                { Corner.LowRight, new TouchHandler(new LowRightHandler(handlerSize), Corner.LowRight) },
                { Corner.HighLeft, new TouchHandler(new HighLeftHandler(handlerSize), Corner.HighLeft) },
                { Corner.LowLeft, new TouchHandler(new LowLeftHandler(handlerSize), Corner.LowLeft) },
                { Corner.Center, new TouchHandler(new CenterHandler(handlerSize), Corner.Center) },
                { Corner.None, new TouchHandler(new DummyHandler(), Corner.None) }
            };
            attachedFigure = DummyFigure.GetInstance();
        }

        public void AttachTo(IFigure figure)
        {
            attachedFigure = figure;
        }

        public void Detach()
        {
            attachedFigure = DummyFigure.GetInstance();
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
            var figure = attachedFigure.GetFigureSnapshot();
            activeHandler = FindActiveHanderFor(figure, touch);
        }

        private Corner FindActiveHanderFor(Snapshot figureSnapshot, Point touch) 
        {
            foreach (var handler in handlers.Values)
            {
                if (handler.IsTouch(figureSnapshot, touch))
                {
                    return handler.Corner;
                }
            }
            return Corner.None;
        }
            
        public void Drag(float deltaX, float deltaY)
        {
            handlers[activeHandler].Handle(attachedFigure, deltaX, deltaY);
        }

        public void Draw(IDrawerAdapter adapter)
        {
            if (attachedFigure.IsNotDummy())
            {
                attachedFigure.Draw(adapter);
                DrawHandlers(adapter);
            }
        }

        private void DrawHandlers(IDrawerAdapter adapter)
        {
            var figureSnapshot = attachedFigure.GetFigureSnapshot();
            foreach (var handler in handlers.Values)
            {
                handler.Draw(adapter, figureSnapshot);
            }
        }
    }
}

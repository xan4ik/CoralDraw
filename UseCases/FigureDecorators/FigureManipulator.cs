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

        static FigureManipulator() 
        {
            dummy = new DummyFigure();
        }

        public FigureManipulator()
        {
            handlers = new Dictionary<Corner, IDragAction>()
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

    public interface IDragAction 
    {
        Corner Corner { get; }
        bool IsTouch(Snapshot figurePose, Point touch);
        void Handle(IFigure figure, float deltaX, float deltaY);
    }

    abstract class TouchHandler : IDragAction
    {
        protected readonly Size handlerSize;
        protected TouchHandler(Size size, Corner corner)
        {
            this.handlerSize = size;
            this.Corner = corner;
        } 
        public Corner Corner { get; private set; }
        public bool IsTouch(Snapshot figurePose, Point touch)
        {
            var handlerSnapshot = GetHandlerSnapshotFrom(figurePose);
            return handlerSnapshot.ContainsPoint(touch);
        }

        protected abstract Snapshot GetHandlerSnapshotFrom(Snapshot figurePose);
        public abstract void Handle(IFigure figure, float deltaX, float deltaY);
    }

    class HighLeftHandler : TouchHandler
    {
        public HighLeftHandler(Size size) : base(size, Corner.HighLeft)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(deltaX, deltaY);
            figure.Resize(-deltaX, -deltaY);
        }

        protected override Snapshot GetHandlerSnapshotFrom(Snapshot figurePose)
        {
            return new Snapshot(figurePose.Location, handlerSize);
        }
    }
    class HighRightHandler : TouchHandler
    {
        public HighRightHandler(Size size) : base(size, Corner.HighRight)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Resize(deltaX, 0);
        }

        protected override Snapshot GetHandlerSnapshotFrom(Snapshot figurePose)
        {
            var location = new Point() {
                X = figurePose.Location.X + figurePose.Size.Width - handlerSize.Width,
                Y = figurePose.Location.Y 
            };
            return new Snapshot(location, handlerSize);
        }
    }
    class LowRightHandler : TouchHandler
    {
        public LowRightHandler(Size size) : base(size, Corner.LowRight)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Resize(deltaX, deltaY);
        }

        protected override Snapshot GetHandlerSnapshotFrom(Snapshot figurePose)
        {
            var location = new Point() 
            {
                X = figurePose.Location.X + figurePose.Size.Width - handlerSize.Width,
                Y = figurePose.Location.Y + figurePose.Size.Height - handlerSize.Height
            };
            return new Snapshot(location, handlerSize);
        }
    }
    class LowLeftHandler : TouchHandler
    {
        public LowLeftHandler(Size size) : base(size, Corner.LowLeft)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Resize(0, deltaY);
        }

        protected override Snapshot GetHandlerSnapshotFrom(Snapshot figurePose)
        {
            var location = new Point()
            {
                Y = figurePose.Location.Y + figurePose.Size.Height - handlerSize.Height,
                X = figurePose.Location.X
            };
            return new Snapshot(location, handlerSize);
        }
    }
    class CenterHandler : TouchHandler
    {
        public CenterHandler(Size size) : base(size, Corner.Center)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(deltaX, deltaY);
        }

        protected override Snapshot GetHandlerSnapshotFrom(Snapshot figurePose)
        {
            return figurePose;
        }
    }
    class DummyHandler : IDragAction
    {
        public Corner Corner 
        {
            get 
            {
                return Corner.None;
            }
        }
        public void Handle(IFigure figure, float deltaX, float deltaY)
        {
            return;
        }

        public bool IsTouch(Snapshot figurePose, Point touch)
        {
            return true;
        }
    }
}

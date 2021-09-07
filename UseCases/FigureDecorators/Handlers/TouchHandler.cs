using Core;

namespace UseCases
{
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
}

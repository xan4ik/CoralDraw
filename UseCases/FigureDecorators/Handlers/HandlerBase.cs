using Core;

namespace UseCases
{
    abstract class HandlerBase : IFigureTouchHandler
    {
        protected readonly Size handlerSize;
        protected HandlerBase(Size handlerSize)
        {
            this.handlerSize = handlerSize;
        }

        public abstract Snapshot GetHandlerSnapshotRelativeTo(Snapshot figureSnapshot);

        public abstract void Handle(IFigure figure, float deltaX, float deltaY);
    }
}

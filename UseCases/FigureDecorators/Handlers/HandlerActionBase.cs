using Core;

namespace UseCases
{
    abstract class HandlerActionBase : IFigureTouchHandler
    {
        protected readonly Size handlerSize;
        protected HandlerActionBase(Size handlerSize)
        {
            this.handlerSize = handlerSize;
        }

        public abstract Snapshot GetHandlerSnapshotRelativeTo(Snapshot figureSnapshot);

        public abstract void Handle(IFigure figure, float deltaX, float deltaY);
    }
}

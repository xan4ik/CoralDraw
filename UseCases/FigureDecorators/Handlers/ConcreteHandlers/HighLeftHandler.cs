using Core;

namespace UseCases
{
    class HighLeftHandler : HandlerActionBase
    {
        public HighLeftHandler(Size handlerSize) : base(handlerSize)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(deltaX, deltaY);
            figure.Resize(-deltaX, -deltaY);
        }

        public override Snapshot GetHandlerSnapshotRelativeTo(Snapshot figurePose)
        {
            return new Snapshot(figurePose.Location, handlerSize);
        }
    }
}

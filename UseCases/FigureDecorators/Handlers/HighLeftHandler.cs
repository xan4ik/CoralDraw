using Core;

namespace UseCases
{
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
}

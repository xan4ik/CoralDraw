using Core;

namespace UseCases
{
    class LowLeftHandler : HandlerActionBase
    {
        public LowLeftHandler(Size handlerSize) : base(handlerSize)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(deltaX, 0);
            figure.Resize(-deltaX, deltaY);
        }

        public override Snapshot GetHandlerSnapshotRelativeTo(Snapshot figurePose)
        {
            var location = new Point()
            {
                Y = figurePose.Location.Y + figurePose.Size.Height - handlerSize.Height,
                X = figurePose.Location.X
            };
            return new Snapshot(location, handlerSize);
        }
    }
}

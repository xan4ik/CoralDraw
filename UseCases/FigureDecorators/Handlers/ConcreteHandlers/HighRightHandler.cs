using Core;

namespace UseCases
{
    class HighRightHandler : HandlerBase
    {
        public HighRightHandler(Size handlerSize) : base(handlerSize)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(0, deltaY);
            figure.Resize(deltaX, -deltaY);
        }

        public override Snapshot GetHandlerSnapshotRelativeTo(Snapshot figurePose)
        {
            var location = new Point() 
            {
                X = figurePose.Location.X + figurePose.Size.Width - handlerSize.Width,
                Y = figurePose.Location.Y 
            };
            return new Snapshot(location, handlerSize);
        }
    }
}

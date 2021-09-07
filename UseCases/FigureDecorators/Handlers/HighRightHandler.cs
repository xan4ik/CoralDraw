using Core;

namespace UseCases
{
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
}

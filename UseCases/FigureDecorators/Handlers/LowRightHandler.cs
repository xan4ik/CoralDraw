using Core;

namespace UseCases
{
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
}

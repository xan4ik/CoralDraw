using Core;

namespace UseCases
{
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
}

using Core;

namespace UseCases
{
    class LowRightHandler : HandlerBase
    {
        public LowRightHandler(Size handlerSize) : base(handlerSize)
        { }

        public override Snapshot GetHandlerSnapshotParentTo(Snapshot figureSnapshot)
        {
            var location = new Point()
            {
                X = figureSnapshot.Location.X + figureSnapshot.Size.Width - handlerSize.Width,
                Y = figureSnapshot.Location.Y + figureSnapshot.Size.Height - handlerSize.Height
            };
            return new Snapshot(location, handlerSize);
        }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Resize(deltaX, deltaY);
        }
    }
}

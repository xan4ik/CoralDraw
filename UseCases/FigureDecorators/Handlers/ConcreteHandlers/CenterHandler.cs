using Core;

namespace UseCases
{
    class CenterHandler : HandlerBase
    {
        public CenterHandler(Size handlerSize) : base(handlerSize)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(deltaX, deltaY);
        }
        
        public override Snapshot GetHandlerSnapshotRelativeTo(Snapshot figureSnapshot)
        {
            var location = new Point() 
            {
                X = figureSnapshot.Location.X + figureSnapshot.Size.Width/2 - handlerSize.Width/2,
                Y = figureSnapshot.Location.Y + figureSnapshot.Size.Height/2 - handlerSize.Height/2
            };
            return new Snapshot(location, handlerSize);
        }
    }
}

using Core;

namespace UseCases
{
    class CenterHandler : TouchHandler
    {
        public CenterHandler(Size size) : base(size, Corner.Center)
        { }

        public override void Handle(IFigure figure, float deltaX, float deltaY)
        {
            figure.Move(deltaX, deltaY);
        }

        protected override Snapshot GetHandlerSnapshotFrom(Snapshot figurePose)
        {
            return figurePose;
        }
    }
}

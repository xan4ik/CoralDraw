using Core;

namespace UseCases
{
    class DummyHandler : IFigureTouchHandler
    {
        public void Handle(IFigure figure, float deltaX, float deltaY)
        {
            return;
        }
        public Snapshot GetHandlerSnapshotParentTo(Snapshot figureSnapshot)
        {
            return default;    
        }
    }
}

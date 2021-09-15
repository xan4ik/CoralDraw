using Core;

namespace UseCases
{
    class DummyHandler : IFigureTouchHandler
    {
        public void Handle(IFigure figure, float deltaX, float deltaY)
        {
            return;
        }
        public Snapshot GetHandlerSnapshotRelativeTo(Snapshot figureSnapshot)
        {
            return default;    
        }
    }
}

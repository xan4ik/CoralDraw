using Core;

namespace UseCases
{
    public interface IFigureTouchHandler
    {
        Snapshot GetHandlerSnapshotParentTo(Snapshot figureSnapshot);
        void Handle(IFigure figure, float deltaX, float deltaY);
    }
}

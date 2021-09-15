using Core;

namespace UseCases
{
    public interface IFigureTouchHandler
    {
        Snapshot GetHandlerSnapshotRelativeTo(Snapshot figureSnapshot);
        void Handle(IFigure figure, float deltaX, float deltaY);
    }
}

using Core;

namespace UseCases
{
    public interface IDragAction 
    {
        Corner Corner { get; }
        bool IsTouch(Snapshot figurePose, Point touch);
        void Handle(IFigure figure, float deltaX, float deltaY);
    }
}

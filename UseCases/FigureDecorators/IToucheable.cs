using Core;

namespace UseCases
{
    public interface IToucheable
    {
        void HandleTouch(Point point);
        void Drag(float deltaX, float deltaY);
    }
}

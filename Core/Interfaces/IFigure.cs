namespace Core
{
    public interface IFigure 
    {
        Snapshot GetFigureSnapshot();
        void Resize(float deltaWigth, float deltaHeight);
        void Move(float deltaX, float deltaY);
        void Draw(IDrawerAdapter adapter);
    }



}

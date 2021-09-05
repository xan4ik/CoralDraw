namespace Core
{
    public interface IDrawerFigureVisitor
    {
        void Draw(IDrawerAdapter adapter, Ellipse ellipse);
        void Draw(IDrawerAdapter adapter, Rectngle rectngle);
    }



}

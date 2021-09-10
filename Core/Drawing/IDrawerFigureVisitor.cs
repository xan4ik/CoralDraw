namespace Core
{
    public interface IDrawerFigureVisitor
    {
        void SetColor(Color color);
        void Draw(IDrawerAdapter adapter, Ellipse ellipse);
        void Draw(IDrawerAdapter adapter, Rectngle rectngle);
    }



}

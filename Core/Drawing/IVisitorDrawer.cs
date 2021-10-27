namespace Core
{
    public interface IVisitorDrawer
    {
        void Draw(IDrawerAdapter adapter, Ellipse ellipse);
        void Draw(IDrawerAdapter adapter, Rectngle rectngle);
    }



}

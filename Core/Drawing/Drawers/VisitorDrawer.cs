namespace Core
{
    public class VisitorDrawer<T> : IVisitorDrawer
        where T: IVisitorDrawer, new()
    {
        private T flyweight;
        private Color color;

        public VisitorDrawer(T flyweight, Color color)
        {
            this.flyweight = flyweight;
            this.color = color;
        }

        public void Draw(IDrawerAdapter adapter, Ellipse ellipse)
        {
            adapter.SetColor(color);
            flyweight.Draw(adapter, ellipse);
        }

        public void Draw(IDrawerAdapter adapter, Rectngle rectngle)
        {
            adapter.SetColor(color);
            flyweight.Draw(adapter, rectngle);
        }
    }

}

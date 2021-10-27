using Core;

namespace UseCases
{
    public class VisitorDrawerCreator<T> : IDrawerCreator
        where T : IVisitorDrawer, new()
    {
        private T flyweight;

        public VisitorDrawerCreator()
        {
            flyweight = new T();
        }

        public IVisitorDrawer CreateDrawer(Color color)
        {
            return new VisitorDrawer<T>(flyweight, color);
        }
    }
}

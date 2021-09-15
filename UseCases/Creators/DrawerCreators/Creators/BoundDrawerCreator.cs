using Core;

namespace UseCases
{
    public class BoundDrawerCreator : IDrawerCreator
    {
        public IDrawerFigureVisitor CreateDrawer(Color color)
        {
            return new BoundDrawer(color);
        }
    }
}

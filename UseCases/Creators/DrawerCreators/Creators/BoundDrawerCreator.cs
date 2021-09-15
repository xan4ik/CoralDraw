using Core;

namespace UseCases
{
    public class BoundDrawerCreator : IDrawerCreator
    {
        public IDrawerFigureVisitor CreateDrawer()
        {
            return new BoundDrawer();
        }
    }
}

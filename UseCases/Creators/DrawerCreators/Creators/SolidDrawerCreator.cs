using Core;

namespace UseCases
{
    public class SolidDrawerCreator : IDrawerCreator
    {
        public IDrawerFigureVisitor CreateDrawer(Color color)
        {
            return new SolidDrawer(color);
        }
    }
}

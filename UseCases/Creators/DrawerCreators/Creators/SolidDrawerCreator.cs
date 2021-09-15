using Core;

namespace UseCases
{
    public class SolidDrawerCreator : IDrawerCreator
    {
        public IDrawerFigureVisitor CreateDrawer()
        {
            return new SolidDrawer();
        }
    }
}

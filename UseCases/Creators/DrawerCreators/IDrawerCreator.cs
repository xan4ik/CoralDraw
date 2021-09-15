using Core;

namespace UseCases
{
    public interface IDrawerCreator
    {
        public IDrawerFigureVisitor CreateDrawer(Color color);
    }
}

using Core;

namespace UseCases
{
    public interface IDrawerFactory
    {
        public IDrawerFigureVisitor CreateDrawer();
    }
}

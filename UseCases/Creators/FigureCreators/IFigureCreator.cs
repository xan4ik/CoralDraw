using Core;

namespace UseCases
{
    public interface IFigureCreator
    {
        public IFigure CreateFigure(IDrawerFigureVisitor visitor, Snapshot snapshot);
    }
}

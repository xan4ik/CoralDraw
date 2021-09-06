using Core;

namespace UseCases
{
    public interface IFigureCreator
    {
        public IFigure CreateFigure(Snapshot snapshot);
        public IFigure CreateFigure();
    }
}

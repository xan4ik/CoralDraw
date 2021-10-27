using Core;

namespace UseCases
{
    public interface IFigureCreator
    {
        public IFigure CreateFigure(IVisitorDrawer visitor, Snapshot snapshot);
    }
}

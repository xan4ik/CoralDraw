using Core;

namespace UseCases
{
    [FactoryKey("Rectangle")]
    class RectangleCreator : IFigureCreator
    {
        public IFigure CreateFigure(IDrawerFigureVisitor visitor, Snapshot snapshot)
        {
            return new Rectngle(visitor, snapshot);
        }
    }
}

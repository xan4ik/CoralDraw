using Core;

namespace UseCases
{
    [CreatorKey("Rectangle")]
    class RectangleCreator : IFigureCreator
    {
        public IFigure CreateFigure(IDrawerFigureVisitor visitor, Snapshot snapshot)
        {
            return new Rectngle(visitor, snapshot);
        }
    }
}

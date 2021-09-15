using Core;

namespace UseCases
{
    [CreatorKey("Ellipse")]
    class EllipceCreator : IFigureCreator
    {
        public IFigure CreateFigure(IDrawerFigureVisitor visitor, Snapshot snapshot)
        {
            return new Ellipse(visitor, snapshot);
        }
    }
}

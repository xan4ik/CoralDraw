using Core;

namespace UseCases
{
    [FactoryKey("Ellipse")]
    class EllipceCreator : IFigureCreator
    {
        public IFigure CreateFigure(IDrawerFigureVisitor visitor, Snapshot snapshot)
        {
            return new Ellipse(visitor, snapshot);
        }
    }
}

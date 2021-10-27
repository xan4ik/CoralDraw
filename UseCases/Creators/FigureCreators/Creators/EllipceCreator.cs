using Core;

namespace UseCases
{
    [CreatorKey("Ellipse")]
    class EllipceCreator : IFigureCreator
    {
        public IFigure CreateFigure(IVisitorDrawer visitor, Snapshot snapshot)
        {
            return new Ellipse(visitor, snapshot);
        }
    }
}

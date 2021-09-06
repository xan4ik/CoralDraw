using Core;

namespace UseCases
{
    [FactoryKey("Ellipse")]
    class EllipceCreator : IFigureCreator
    {
        public IFigure CreateFigure(Snapshot snapshot)
        {
            return new Ellipse(snapshot);
        }

        public IFigure CreateFigure()
        {
            return new Ellipse();
        }
    }
}

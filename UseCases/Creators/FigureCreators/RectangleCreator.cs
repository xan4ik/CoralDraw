using Core;

namespace UseCases
{
    [FactoryKey("Rectangle")]
    class RectangleCreator : IFigureCreator
    {
        public IFigure CreateFigure(Snapshot snapshot)
        {
            return new Rectngle(snapshot);
        }

        public IFigure CreateFigure()
        {
            return new Rectngle();
        }
    }
}

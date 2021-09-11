using Core;

namespace UseCases
{
    public sealed class DummyFigure : IFigure
    {
        private static readonly DummyFigure dummy;
        static DummyFigure() 
        {
            dummy = new DummyFigure();
        }

        private DummyFigure() { }

        public static bool IsDummy(IFigure figure) 
        {
            return figure.Equals(dummy);
        }

        public static bool IsNotDummy(IFigure figure)
        {
            return !figure.Equals(dummy);
        }

        public static IFigure GetInstance() 
        {
            return dummy;
        }

        public void Draw(IDrawerAdapter adapter)
        {
            return;
        }

        public Snapshot GetFigureSnapshot()
        {
            return default;
        }

        public void Move(float deltaX, float deltaY)
        {
            return;
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            return;
        }
    }
    
}

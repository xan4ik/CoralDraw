using Core;

namespace UseCases
{
    public static class FigureExtension
    {
        public static bool IsTouched(this IFigure figure, Point touch) 
        {
            return Snapshot.SnapshotContainsPoint(figure.GetFigureSnapshot(), touch);
        }

        public static bool IsDummy(this IFigure figure) 
        {
            return DummyFigure.IsDummy(figure);
        }

        public static bool IsNotDummy(this IFigure figure) 
        {
            return DummyFigure.IsNotDummy(figure);
        }
    }
}

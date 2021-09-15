using Core;

namespace UseCases
{
    public static class FigureExtension
    {
        public static bool IsTouched(this IFigure figure, Point touch) 
        {
            return figure.GetFigureSnapshot().ContainsPoint(touch);
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

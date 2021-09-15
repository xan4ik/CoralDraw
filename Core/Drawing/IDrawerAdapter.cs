namespace Core
{
    public interface IDrawerAdapter
    {
        void SetColor(Color color);
        void DrawBoundEllipse(Point location, Size size);
        void DrawSolidEllipse(Point location, Size size);
        void DrawBoundRectngle(Point location, Size size);
        void DrawSolidRectngle(Point location, Size size);
    }



}

namespace Core
{
    public class Ellipse : Figure
    {
        public override void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }
    }
}

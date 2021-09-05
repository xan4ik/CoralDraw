namespace Core
{
    public class Rectngle : Figure
    {
        public override void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }
    }
}

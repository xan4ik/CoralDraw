namespace Core
{
    public class Rectngle : Figure
    {
        public Rectngle(Point location, Size size) : base(location, size)
        { }

        public Rectngle() : base(new Point(), new Size())
        { }

        public override IFigure CreateClone()
        {
            return new Rectngle();
        }

        public override void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }
    }
}

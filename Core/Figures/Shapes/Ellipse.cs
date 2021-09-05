namespace Core
{
    public class Ellipse : Figure
    {
        public Ellipse(Point location, Size size) : base(location, size)
        { }

        public Ellipse() : base(new Point(), new Size()) 
        { }

        public override IFigure CreateClone()
        {
            return new Ellipse();
        }

        public override void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }
    }
}

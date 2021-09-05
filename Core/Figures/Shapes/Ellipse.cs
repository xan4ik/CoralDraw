namespace Core
{
    public class Ellipse : Figure
    {
        public Ellipse(Snapshot snapshot) : base(snapshot)
        { }

        public Ellipse() : base(new Snapshot()) 
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

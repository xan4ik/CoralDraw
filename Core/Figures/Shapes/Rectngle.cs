namespace Core
{
    public class Rectngle : Figure
    {
        public Rectngle(Snapshot snapshot) : base(snapshot)
        { }

        public Rectngle() : base(new Snapshot())
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

namespace Core
{
    public class Rectngle : Figure
    {
        public Rectngle(Snapshot snapshot, IDrawerFigureVisitor visitor) : base(snapshot, visitor)
        { }

        public Rectngle(IDrawerFigureVisitor visitor) : base(default, visitor)
        { }

        protected override IFigure OnCreateClone(IDrawerFigureVisitor clone)
        {
            return new Rectngle(clone);
        }

        protected override void OnDraw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }

    }
}

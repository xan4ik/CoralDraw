namespace Core
{
    public class Ellipse : Figure
    {
        public Ellipse(IDrawerFigureVisitor visitor, Snapshot snapshot) : base(snapshot, visitor)
        { }

        public Ellipse(IDrawerFigureVisitor visitor) : base(default, visitor)
        { }

        protected override IFigure OnCreateClone(IDrawerFigureVisitor clone)
        {
            return new Ellipse(clone);
        }

        protected override void OnDraw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }
    }
}

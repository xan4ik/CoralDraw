namespace Core
{
    public class Ellipse : Shape
    {
        public Ellipse(IVisitorDrawer visitor, Snapshot snapshot) : base(snapshot, visitor)
        { }

        public Ellipse(IVisitorDrawer visitor) : base(default, visitor)
        { }

        protected override IFigure OnCreateClone(IVisitorDrawer clone)
        {
            return new Ellipse(clone, GetFigureSnapshot());
        }

        protected override void OnDraw(IDrawerAdapter adapter, IVisitorDrawer visitor)
        {
            visitor.Draw(adapter, this);
        }
    }
}

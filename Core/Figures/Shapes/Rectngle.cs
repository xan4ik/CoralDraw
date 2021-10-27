namespace Core
{
    public class Rectngle : Shape
    {
        public Rectngle(IVisitorDrawer visitor, Snapshot snapshot) : base(snapshot, visitor)
        { }

        public Rectngle(IVisitorDrawer visitor) : base(default, visitor)
        { }

        protected override IFigure OnCreateClone(IVisitorDrawer clone)
        {
            return new Rectngle(clone, GetFigureSnapshot());
        }

        protected override void OnDraw(IDrawerAdapter adapter, IVisitorDrawer visitor)
        {
            visitor.Draw(adapter, this);
        }

    }
}

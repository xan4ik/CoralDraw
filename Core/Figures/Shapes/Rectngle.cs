namespace Core
{
    public class Rectngle : Shape
    {
        public Rectngle(IDrawerFigureVisitor visitor, Snapshot snapshot) : base(snapshot, visitor)
        { }

        public Rectngle(IDrawerFigureVisitor visitor) : base(default, visitor)
        { }

        protected override IFigure OnCreateClone(IDrawerFigureVisitor clone)
        {
            return new Rectngle(clone, GetFigureSnapshot());
        }

        protected override void OnDraw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            visitor.Draw(adapter, this);
        }

    }
}

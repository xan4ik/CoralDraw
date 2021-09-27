namespace Core
{
    public class BoundDrawer : DrawerVisitor
    {
        public BoundDrawer(Color color) : base(color)
        { }

        public override IDrawerFigureVisitor CreateClone()
        {
            return new BoundDrawer(GetColor());
        }

        protected override void OnDrawEllipse(IDrawerAdapter adapter, Snapshot figureSnapshot)
        {
            adapter.DrawBoundEllipse(figureSnapshot.Location, figureSnapshot.Size);
        }

        protected override void OnDrawRectangle(IDrawerAdapter adapter, Snapshot figureSnapshot)
        {
            adapter.DrawBoundRectngle(figureSnapshot.Location, figureSnapshot.Size);
        }
    }
}

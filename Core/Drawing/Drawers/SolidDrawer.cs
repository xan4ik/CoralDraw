namespace Core
{
    public class SolidDrawer : DrawerVisitor
    {
        public SolidDrawer(Color color) : base(color)
        { }

        public override IDrawerFigureVisitor CreateClone()
        {
            return new SolidDrawer(GetColor());
        }

        protected override void OnDrawEllipse(IDrawerAdapter adapter, Snapshot figureSnapshot)
        {
            adapter.DrawSolidEllipse(figureSnapshot.Location, figureSnapshot.Size);
        }

        protected override void OnDrawRectangle(IDrawerAdapter adapter, Snapshot figureSnapshot)
        {
            adapter.DrawSolidRectngle(figureSnapshot.Location, figureSnapshot.Size);
        }
    }
}

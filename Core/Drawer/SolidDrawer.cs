namespace Core
{
    public class SolidDrawer : DrawerVisitor
    {
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

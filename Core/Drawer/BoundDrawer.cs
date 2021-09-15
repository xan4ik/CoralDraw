namespace Core
{
    public class BoundDrawer : DrawerVisitor
    {
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

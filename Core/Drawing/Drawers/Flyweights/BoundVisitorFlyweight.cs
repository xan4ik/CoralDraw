namespace Core
{
    public class BoundVisitorFlyweight : IVisitorDrawer
    {
        public void Draw(IDrawerAdapter adapter, Ellipse ellipse)
        {
            var snapshot = ellipse.GetFigureSnapshot();
            adapter.DrawBoundEllipse(snapshot.Location, snapshot.Size);
        }

        public void Draw(IDrawerAdapter adapter, Rectngle rectngle)
        {
            var snapshot = rectngle.GetFigureSnapshot();
            adapter.DrawBoundRectngle(snapshot.Location, snapshot.Size);
        }
    }

}

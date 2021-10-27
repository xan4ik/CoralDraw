namespace Core
{

    public class SolidVisitorFlyweight : IVisitorDrawer
    {
        public void Draw(IDrawerAdapter adapter, Ellipse ellipse)
        {
            var snapshot = ellipse.GetFigureSnapshot();
            adapter.DrawSolidEllipse(snapshot.Location, snapshot.Size);
        }

        public void Draw(IDrawerAdapter adapter, Rectngle rectngle)
        {
            var snapshot = rectngle.GetFigureSnapshot();
            adapter.DrawSolidRectngle(snapshot.Location, snapshot.Size);
        }
    }

}

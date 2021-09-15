namespace Core
{
    public abstract class DrawerVisitor : IDrawerFigureVisitor
    {
        private Color color = default;

        public DrawerVisitor(Color color)
        {
            this.color = color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void Draw(IDrawerAdapter adapter, Ellipse ellipse)
        {
            adapter.SetColor(color);
            OnDrawEllipse(adapter, ellipse.GetFigureSnapshot());
        }

        protected abstract void OnDrawEllipse(IDrawerAdapter adapter, Snapshot figureSnapshot);

        public void Draw(IDrawerAdapter adapter, Rectngle rectngle)
        {
            adapter.SetColor(color);
            OnDrawRectangle(adapter, rectngle.GetFigureSnapshot());
        }

        protected abstract void OnDrawRectangle(IDrawerAdapter adapter, Snapshot figureSnapshot);
    }
}

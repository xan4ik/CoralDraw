namespace Core
{
    public abstract class DrawerVisitor : IDrawerFigureVisitor
    {
        private Core.Color color = default;

        public void SetColor(Core.Color color)
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

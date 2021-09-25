using Core;

namespace UseCases
{
    class TouchHandler 
    {
        private IFigureTouchHandler handler;
        private Color color;
        public TouchHandler(IFigureTouchHandler handler, Corner corner, Color color = default)
        {
            this.handler = handler;
            this.Corner = corner;
        } 

        public Corner Corner { get; private set; }

        public void SetColor(Color color) 
        {
            this.color = color;
        }

        public void Handle(IFigure figure, float deltaX, float deltaY) 
        {
            handler.Handle(figure, deltaX, deltaY);
        }
  
        public bool IsTouch(Snapshot figureSnapshot, Point touch)
        {
            var handlerSnapshot = handler.GetHandlerSnapshotRelativeTo(figureSnapshot);
            return handlerSnapshot.ContainsPoint(touch);
        }

        public void Draw(IDrawerAdapter adapter, Snapshot figureSnapshot)
        {
            var handlerSnapshot = handler.GetHandlerSnapshotRelativeTo(figureSnapshot);
            adapter.SetColor(color);
            adapter.DrawBoundRectngle(handlerSnapshot.Location, handlerSnapshot.Size);
        }

    }
}

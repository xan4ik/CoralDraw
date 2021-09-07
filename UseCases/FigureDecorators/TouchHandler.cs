using Core;

namespace UseCases
{
    class TouchHandler 
    {
        private IFigureTouchHandler handler;
        public TouchHandler(IFigureTouchHandler handler, Corner corner)
        {
            this.handler = handler;
            this.Corner = corner;
        } 

        public Corner Corner { get; private set; }
        public void Handle(IFigure figure, float deltaX, float deltaY) 
        {
            handler.Handle(figure, deltaX, deltaY);
        }
  
        public bool IsTouch(Snapshot figureSnapshot, Point touch)
        {
            var handlerSnapshot = handler.GetHandlerSnapshotParentTo(figureSnapshot);
            return handlerSnapshot.ContainsPoint(touch);
        }

        public void Draw(IDrawerAdapter adapter, Snapshot figureSnapshot)
        {
            var handlerSnapshot = handler.GetHandlerSnapshotParentTo(figureSnapshot);
            adapter.DrawRectngle(handlerSnapshot.Location, handlerSnapshot.Size);
        }

    }
}

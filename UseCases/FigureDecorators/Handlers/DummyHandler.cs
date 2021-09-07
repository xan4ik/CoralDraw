using Core;

namespace UseCases
{
    class DummyHandler : IDragAction
    {
        public Corner Corner 
        {
            get 
            {
                return Corner.None;
            }
        }
        public void Handle(IFigure figure, float deltaX, float deltaY)
        {
            return;
        }

        public bool IsTouch(Snapshot figurePose, Point touch)
        {
            return true;
        }
    }
}

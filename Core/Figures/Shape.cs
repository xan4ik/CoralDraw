namespace Core
{
    public abstract class Shape : IFigure, IPrototype<IFigure>
    {
        private IDrawerFigureVisitor visitor;
        private Transform transform;
        protected Shape(Snapshot snapshot, IDrawerFigureVisitor visitor)
        {
            this.transform = new Transform(snapshot);
            this.visitor = visitor;
        }

        public void Move(float deltaX, float deltaY)
        {
            transform.Move(deltaX, deltaY);
        }
    
        public Snapshot GetFigureSnapshot()
        {
            return transform.CreateSnapshot();
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            transform.Resize(deltaWigth, deltaHeight);
        }

        public void DrawWith(IDrawerAdapter adapter) 
        {
            OnDraw(adapter, visitor);
        }

        protected abstract void OnDraw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor);
        public IFigure CreateClone() 
        {
            if (visitor is IPrototype<IDrawerFigureVisitor> prototype)
            {
                return OnCreateClone(prototype.CreateClone());
            }
            else throw new CloneCreateException(visitor.GetType(), typeof(IDrawerFigureVisitor));
        }

        protected abstract IFigure OnCreateClone(IDrawerFigureVisitor clone);

    }



}

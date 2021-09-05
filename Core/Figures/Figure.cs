using System;

namespace Core
{
    public abstract class Figure : IFigure, IFigurePrototype
    {
        private Transform transform;
        protected Figure(Snapshot snapshot)
        {
            transform = new Transform(snapshot);
        }

        public Transform GetTransform() 
        {
            return transform;
        }

        public void Move(float deltaX, float deltaY)
        {
            transform.Relocate(deltaX, deltaY);
        }
    
        public Snapshot GetFigureSnapshot()
        {
            return transform.CreateSnapshot();
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            transform.Resize(deltaWigth, deltaHeight);
        }

        public abstract void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor);
        public abstract IFigure CreateClone();

    }



}

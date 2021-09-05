using System;

namespace Core
{
    public abstract class Figure : IFigure, IFigurePrototype
    {
        private Transform transform;
        protected Figure(Point location, Size size)
        {
            transform = new Transform(location, size);
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

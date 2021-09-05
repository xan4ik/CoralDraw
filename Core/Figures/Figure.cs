using System;

namespace Core
{
    abstract class Figure : IFigure
    {
        private Transform transform;
        protected Figure() 
        {
            transform = new Transform();
        }

        public void Move(float deltaX, float deltaY)
        {
            transform.Relocate(deltaX, deltaY);
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            transform.Resize(deltaWigth, deltaHeight);
        }

        public void Draw(IDrawerFigureVisitor visitor) 
        {
            try
            {
                OnDrawInvoke(visitor, transform);
            }
            catch (Exception exception) 
            {
                throw new DrawMethodException(visitor, exception);
            }
        }

        protected abstract void OnDrawInvoke(IDrawerFigureVisitor visitor, Transform snapShot);
    }



}

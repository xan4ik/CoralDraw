using System;

namespace Core
{
    public abstract class Figure : IFigure, IPrototype<IFigure>
    {
        private IDrawerFigureVisitor visitor;
        private Transform transform;
        protected Figure(Snapshot snapshot, IDrawerFigureVisitor visitor)
        {
            this.transform = new Transform(snapshot);
            this.visitor = visitor;
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

        public void Draw(IDrawerAdapter adapter) 
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

    public class CloneCreateException : Exception 
    {
        public CloneCreateException(Type type, Type prototype ) : base($"Type: {type.ToString()} have to implement IPrototype<{prototype.ToString()}> interface")
        {
            RequiredPrototype = prototype;            
            Type = type;
        }

        public readonly Type Type;
        public readonly Type RequiredPrototype;
    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Composite : IFigure, IPrototype<IFigure>
    {
        private List<IFigure> figures;
        public Composite(params IFigure[] figures)
        {
            this.figures = new List<IFigure>();
            foreach (var figure in figures)
            {
                this.figures.Add(figure);
            }
        }

        public bool IsEmpty() 
        {
            return figures.Count == 0;
        }

        public void AddFigure(IFigure figure) 
        {
            if (NotContains(figure))
            {
                figures.Add(figure);
            }
            else throw new ArgumentException("This figure already in list");
        }

        public bool NotContains(IFigure figure)
        {
            return !figures.Contains(figure);
        }

        public void RemoveFigure(IFigure figure) 
        {
            if (Contains(figure))
            {
                figures.Remove(figure);
            }
            else throw new ArgumentException("This figure not in list");
        }

        public bool Contains(IFigure figure) 
        {
            return figures.Contains(figure);
        }


        public void Move(float deltaX, float deltaY)
        {
            foreach (var figure in figures) 
            {
                figure.Move(deltaX, deltaY);
            }
        }

        public void Resize(float deltaWigth, float deltaHeight)
        {
            foreach (var figure in figures)
            {
                figure.Resize(deltaWigth, deltaHeight);
            }
        }
        
        public void DrawWith(IDrawerAdapter adapter)
        {
            foreach (var figure in figures) 
            {
                figure.DrawWith(adapter);
            }
        }

        public Snapshot GetFigureSnapshot()
        {
            var snapshots = GetAllSnapshots();
            var minX = snapshots.Min(snap => snap.Location.X);
            var minY = snapshots.Min(snap => snap.Location.Y);
            var maxX = snapshots.Max(snap => snap.Location.X + snap.Size.Width);
            var maxY = snapshots.Max(snap => snap.Location.Y + snap.Size.Height);
            var size = new Size(){
                Width = Math.Abs(maxX - minX),
                Height = Math.Abs(maxY - minY)};
            var location = new Point(minX, minY);

            return new Snapshot(location, size); 
        }

        private IEnumerable<Snapshot> GetAllSnapshots() 
        {
            foreach (var figure in figures) 
            {
                yield return figure.GetFigureSnapshot();
            }
        }

        public IFigure CreateClone()
        {
            var composite = new Composite();
            foreach (var figure in figures)
            {
                var clone = CreateCloneOf(figure);
                composite.AddFigure(clone);
            }
            return composite;
        }

        private IFigure CreateCloneOf(IFigure figure) 
        {
            if (figure is IPrototype<IFigure> prototype)
            {
                return prototype.CreateClone();
            }
            else throw new CloneCreateException(figure.GetType(), typeof(IFigure));
        }
    }
}

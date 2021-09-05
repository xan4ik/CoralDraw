using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Composite : IFigure, IFigurePrototype
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

        public void AddFigure(IFigure figure) 
        {
            if (NotInList(figure))
            {
                figures.Add(figure);
            }
            else throw new ArgumentException("This figure already in list");
        }

        private bool NotInList(IFigure figure)
        {
            return !figures.Contains(figure);
        }

        public void RemoveFigure(IFigure figure) 
        {
            if (InList(figure))
            {
                figures.Remove(figure);
            }
            else throw new ArgumentException("This figure not in list");
        }

        private bool InList(IFigure figure) 
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
            throw new NotImplementedException();
        }
        
        public void Draw(IDrawerAdapter adapter, IDrawerFigureVisitor visitor)
        {
            foreach (var figure in figures) 
            {
                figure.Draw(adapter, visitor);
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

        //TODO: Ask for it
        public IFigure CreateClone()
        {
            var composite = new Composite();
            foreach (var figure in figures) 
            {
                AddFigureToClone(composite, figure);
            }
            return composite;
        }

        private void AddFigureToClone(Composite composite, IFigure figure)
        {
            if (figure is IFigurePrototype) 
            {
                composite.AddFigure(((IFigurePrototype)figure).CreateClone());
            }
        }
    }
}

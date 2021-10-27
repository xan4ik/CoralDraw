using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Composite : IFigure, IPrototype<IFigure>, ICompositeFigure
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

        public void Add(IFigure figure) 
        {
            if (NotContains(figure))
            {
                figures.Add(figure);
            }
            else throw new ArgumentException("This figure already in list");
        }


        public bool NotContains(IFigure figure)
        {
            return !figures.Contains(figure) && figure != this;
        }

        public void Remove(IFigure figure) 
        {
            if (Contains(figure))
            {
                figures.Remove(figure);
            }
            else throw new ArgumentException("This figure not in list");
        }

        public bool Contains(IFigure figure) 
        {
            return figures.Contains(figure) && figure == this;
        }

        public IEnumerable<IFigure> EnumerateFigures()
        {
            return figures;
        }

        public void Move(float deltaX, float deltaY)
        {
            foreach (var figure in figures) 
            {
                figure.Move(deltaX, deltaY);
            }
        }

        public void Resize(float deltaWidth, float deltaHeight)
        {
            var parent = GetFigureSnapshot();
            var scaleFactor = CountScaleFactor(parent, deltaWidth, deltaHeight);

            foreach (var figure in figures)
            {
                var chield = figure.GetFigureSnapshot();
                var size = EvaluateSizeVector(chield, scaleFactor);
                var pose = EveluateLocationVector(parent, chield, scaleFactor);               

                figure.Resize(size.X, size.Y);
                figure.Move(pose.X, pose.Y);
            }

        }

        private Point CountScaleFactor(Snapshot snapshot, float deltaWidth, float deltaHeight)
        {
            var scaledWidth = snapshot.Size.Width + deltaWidth;
            var scaledHeight = snapshot.Size.Height + deltaHeight;

            return new Point()
            {
                X = scaledWidth / snapshot.Size.Width,
                Y = scaledHeight / snapshot.Size.Height,
            };
        }

        private Point EvaluateSizeVector(Snapshot figure, Point scaleFactor) 
        {
            return new Point() 
            {
                 X = (figure.Size.Width * scaleFactor.X) - figure.Size.Width,
                 Y = (figure.Size.Height * scaleFactor.Y) - figure.Size.Height,
            };
        }

        private Point EveluateLocationVector(Snapshot parent, Snapshot chield, Point scaleFactor) 
        {
            var offset = Point.OffsetFromTo(parent.Location, chield.Location);
            return new Point() 
            {
                X = offset.X * scaleFactor.X - offset.X,
                Y = offset.Y * scaleFactor.Y - offset.Y
            };
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

            return new Snapshot()
            {
                Size = new Size()
                {
                    Width = Math.Abs(maxX - minX),
                    Height = Math.Abs(maxY - minY)
                },
                Location = new Point(minX, minY)
            };
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
                composite.Add(clone);
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

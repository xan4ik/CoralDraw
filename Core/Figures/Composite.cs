using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Figures
{
    class Composite : IFigure
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
    }
}

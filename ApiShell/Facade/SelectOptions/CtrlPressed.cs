using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public class CtrlPressed : ISelectOption
    {
        private IFigure lastFigure;
        public CtrlPressed()
        {
            lastFigure = DummyFigure.GetInstance();
        }

        public IFigure GetFigureByTouch(IEnumerable<IFigure> figures, Point touch)
        {
            if (lastFigure.IsDummy())
            {
                FindFirst(figures, touch);
                return lastFigure;
            }

            FindNext(figures, touch);
            return lastFigure;
        }

        private void FindFirst(IEnumerable<IFigure> figures, Point touch)
        {
            lastFigure = DummyFigure.GetInstance();
            foreach (var figure in figures)
            {
                if (figure.IsTouched(touch))
                {
                    lastFigure = figure;
                }
            }
        }

        private void FindNext(IEnumerable<IFigure> figures, Point location)
        {
            IEnumerator<IFigure> iterator = figures.GetEnumerator();
            MoveToPrevious(iterator);
            FindFigureAfterPrevious(iterator, location);
        }

        private void MoveToPrevious(IEnumerator<IFigure> figures) 
        {
            while (figures.MoveNext() && !figures.Current.Equals(lastFigure)) 
            {
                continue;
            }
        }

        private void FindFigureAfterPrevious(IEnumerator<IFigure> figures, Point touch) 
        {
            while (figures.MoveNext())
            {
                if (figures.Current.IsTouched(touch))
                {
                    lastFigure = figures.Current;
                }
            }
            lastFigure = DummyFigure.GetInstance();
        }
    }
}

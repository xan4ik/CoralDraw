using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public class CtrlPressed : ISelectOption, IPrototype<ISelectOption>
    {
        private DefaultOption option;
        private IFigure lastFigure;

        public CtrlPressed()
        {
            lastFigure = DummyFigure.GetInstance();
            option = new DefaultOption();
        }

        public IFigure GetFigureByTouch(IEnumerable<IFigure> figures, Point touch)
        {
            if (lastFigure.IsDummy())
            {
                lastFigure = option.GetFigureByTouch(figures, touch); //find first touch figure
                return lastFigure;
            }
            else
            {
                lastFigure = FindNext(figures, touch);
                return lastFigure;
            }
        }

        private IFigure FindNext(IEnumerable<IFigure> figures, Point touch)
        {
            IEnumerator<IFigure> iterator = figures.GetEnumerator();
            MoveToPrevious(iterator);
            return GetNextAfterPrevious(iterator, touch);
        }

        private void MoveToPrevious(IEnumerator<IFigure> iterator)
        {
            while (iterator.MoveNext()) 
            {
                if (iterator.Current.Equals(lastFigure))
                {
                    return;
                }
            }
        }

        private IFigure GetNextAfterPrevious(IEnumerator<IFigure> iterator, Point touch)
        {
            while (iterator.MoveNext()) 
            {
                if (iterator.Current.IsTouched(touch))
                {
                    return iterator.Current;
                }
            }
            return DummyFigure.GetInstance();
        }

        public ISelectOption CreateClone()
        {
            return this;
        }
    }
}

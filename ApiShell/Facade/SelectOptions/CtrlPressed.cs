using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public class CtrlPressed : ISelectOption
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
                lastFigure = option.GetFigureByTouch(figures, touch);
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
            return GetNextAfterPrevious(iterator, touch);
        }

        private IFigure GetNextAfterPrevious(IEnumerator<IFigure> iterator, Point touch)
        {
            bool previousFinded = false;
            do
            {
                if (iterator.Current == lastFigure)
                {
                    previousFinded = true;
                    continue;
                }
                if (previousFinded && iterator.Current.IsTouched(touch))
                {
                    return iterator.Current;
                }

            }  while (iterator.MoveNext());
            
            return DummyFigure.GetInstance();
        }
    }
}

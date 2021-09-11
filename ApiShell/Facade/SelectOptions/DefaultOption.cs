using Core;
using UseCases;
using System.Collections.Generic;

namespace ApiShell
{
    public class DefaultOption : ISelectOption
    {
        public IFigure GetFigureByTouch(IEnumerable<IFigure> figures, Point touch)
        {
            foreach (IFigure figure in figures)
            {
                if (figure.IsTouched(touch))
                {
                    return figure;
                }
            }
            return DummyFigure.GetInstance();
        }
    }
}

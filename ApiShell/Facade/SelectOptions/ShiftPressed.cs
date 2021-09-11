using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public class ShiftPressed : ISelectOption
    {
        private Composite composite;
        private DefaultOption option; 

        public ShiftPressed()
        {
            composite = new Composite();
            option = new DefaultOption();
        }

        public IFigure GetFigureByTouch(IEnumerable<IFigure> figures, Point touch)
        {
            var figure = option.GetFigureByTouch(figures, touch);
            if (figure.IsNotDummy())
            {
                HandleFigure(figure);
                return composite;
            }
            CleanUpComposite();
            return DummyFigure.GetInstance();
        }

        private void CleanUpComposite()
        {
            composite = new Composite();
        }

        private void HandleFigure(IFigure figure)
        {
            if (composite.NotContains(figure))
            {
                composite.AddFigure(figure);
            }
            else composite.RemoveFigure(figure);
        }
    }
}

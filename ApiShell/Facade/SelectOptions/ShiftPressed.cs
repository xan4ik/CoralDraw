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
                return GetResult();
            }
            CleanUpComposite();
            return DummyFigure.GetInstance();
        }

        private void HandleFigure(IFigure figure)
        {
            if (composite.NotContains(figure))
            {
                composite.Add(figure);
            }
            else composite.Remove(figure); 
        }

        private IFigure GetResult()
        {
            if (composite.IsEmpty()) 
            {
                return DummyFigure.GetInstance();
            }
            return composite;
        }

        private void CleanUpComposite()
        {
            //TODO: Maybe clear() ?
            composite = new Composite();
        }
    }
}

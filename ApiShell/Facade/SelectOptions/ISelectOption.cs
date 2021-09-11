using Core;
using System.Collections.Generic;

namespace ApiShell
{
    public interface ISelectOption
    {
        IFigure GetFigureByTouch(IEnumerable<IFigure> figures, Point touch);
    }
}

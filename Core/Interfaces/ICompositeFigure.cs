using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface ICompositeFigure : IFigure
    {
        void Add(IFigure element);
        void Remove(IFigure element);
        bool Contains(IFigure element);
        bool NotContains(IFigure element);
        bool IsEmpty();
        IEnumerable<IFigure> EnumerateFigures();
    }
}

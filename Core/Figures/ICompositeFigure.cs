using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface IComposite<T> : IFigure
    {
        void Add(T element);
        void Remove(T element);
        bool Contains(T element);
        bool NotContains(T element);
        bool IsEmpty();
        IEnumerable<T> EnumerateFigures();
    }
}

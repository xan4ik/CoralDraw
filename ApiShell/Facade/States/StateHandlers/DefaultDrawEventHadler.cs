using Core;
using System;

namespace ApiShell
{
    internal class DefaultDrawEventHadler : IStateHandler<IDrawerAdapter>
    {
        public void Handle(IDrawerAdapter args, Redactor redactor)
        {
            foreach (var figure in redactor.Figures)
            {
                figure.DrawWith(args);
            }
        }

        public void Handle(object args, Redactor redactor)
        {
            if (args is IDrawerAdapter adapter) 
            {
                Handle(adapter, redactor);
            }
            else throw new InvalidCastException("Can't cast to IDrawerAdapter");
        }
    }
}

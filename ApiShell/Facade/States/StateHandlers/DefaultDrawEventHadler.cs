using Core;
using System;

namespace ApiShell
{
    internal class DefaultDrawEventHadler : IStateHandler<IDrawerAdapter>
    {
        public void Handle(IDrawerAdapter args, RedactorCore core)
        {
            foreach (var figure in core.Figures)
            {
                figure.DrawWith(args);
            }
        }

        public void Handle(RedactorCore core)
        {
            return;
        }
    }
}

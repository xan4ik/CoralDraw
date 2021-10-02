using Core;
using System.Collections.Generic;
using System.Text;

namespace ApiShell
{

    class GroupCommand : ICommand
    {
        private ICollection<IFigure> figures;
        private ICompositeFigure composite;

        public GroupCommand(ICollection<IFigure> figures, ICompositeFigure composite)
        {
            this.figures = figures;
            this.composite = composite;
        }

        public void Execute()
        {
            foreach (var figure in composite.EnumerateFigures())
            {
                figures.Remove(figure);
            }
            figures.Add(composite);
        }

        public void Undo()
        {
            foreach (var figure in composite.EnumerateFigures())
            {
                figures.Add(figure);
            }
            figures.Remove(composite);
        }
    }
}

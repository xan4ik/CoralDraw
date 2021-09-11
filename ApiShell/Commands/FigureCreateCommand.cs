using Core;
using System.Collections.Generic;

namespace ApiShell
{
    public class FigureCreateCommand : ICommand
    {
        private ICollection<IFigure> figures;
        private IFigure newFigure;

        public FigureCreateCommand(ICollection<IFigure> figures, IFigure newFigure)
        {
            this.figures = figures;
            this.newFigure = newFigure;
        }

        public void Execute()
        {
            figures.Add(newFigure);
        }

        public void Undo()
        {
            figures.Remove(newFigure);
        }
    }
}

using Core;
using System;
using System.Collections.Generic;

namespace ApiShell
{

    class UngroupException : Exception
    {
        public UngroupException(string message) : base(message)
        { }
    }

    class UngroupCommand : ICommand
    {
        private GroupCommand groupCommand;
        public UngroupCommand(ICollection<IFigure> figures, ICompositeFigure composite)
        {
            bool compositeNotExist = !figures.Contains(composite);
            if (compositeNotExist) // select option returns composite, but it doesn't save  in <Redactor> 
            {
                throw new UngroupException("Can't ungroup figure, that hadn't been save");
            }
            groupCommand = new GroupCommand(figures, composite);
        }


        public void Execute()
        {
            groupCommand.Undo();
        }

        public void Undo()
        {
            groupCommand.Execute();
        }
    }
}

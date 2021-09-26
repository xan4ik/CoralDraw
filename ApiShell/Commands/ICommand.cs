using System;
using System.Collections.Generic;
using System.Text;

namespace ApiShell
{
    interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class CommandHistory 
    {
        private Stack<ICommand> history;
        public CommandHistory()
        {
            history = new Stack<ICommand>();
        }

        public void ExecuteCommand(ICommand command) 
        {
            command.Execute();
            history.Push(command);
        }

        public void UndoLastCommand() 
        {
            if (HistoryNotEmpty())
            {
                var lastCommand = history.Pop();
                lastCommand.Undo();
            }
        }

        private bool HistoryNotEmpty() 
        {
            return history.Count != 0;
        }
    }
}

using System;
using System.Windows.Input;

namespace VSConventionalCommitMessage.Base
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand( Action execute, Func<bool> canExecute = null )
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute( object parameter )
        {
            return canExecute == null || canExecute();
        }

        public void Execute( object parameter )
        {
            execute();
        }

        private readonly Action execute;
        private readonly Func<bool> canExecute;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIV2.classes
{
    public class RelayCommand : ICommand
    {

        private Action execute;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public void Execute()
        {
            execute();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            execute();
        }
    }
}

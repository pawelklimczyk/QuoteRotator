using System;
using System.Windows.Input;

namespace QuotesRotatorApp
{
    public class SwitchGroupCommand : ICommand
    {
        private readonly QuotesEngine engine;

        public SwitchGroupCommand(QuotesEngine engine)
        {
            this.engine = engine;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            QuotesGroup group = parameter as QuotesGroup;
            engine.ChangeCurrentGroup(group);
        }

        public event EventHandler CanExecuteChanged;
    }
}
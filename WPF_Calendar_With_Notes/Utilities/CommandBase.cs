using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Calendar_With_Notes.Utilities
{
    class CommandBase : ICommand
    {
        private Action<object> m_action;
        private Func<object, bool> m_functor;

        public CommandBase(Action<object> _action, Func<object, bool> _func)
        {
            m_action = _action;
            m_functor = _func;
        }

        public bool CanExecute(object parameter)
        {
            if (m_functor != null)
                return m_functor(parameter);
            else
                return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            m_action(parameter);
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        private Action action;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// Metoden kan alltid exekveras
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}

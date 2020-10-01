using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class MenuItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public ICommand Command { get; set; }
    }
}

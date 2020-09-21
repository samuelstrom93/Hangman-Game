using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    class LoginPageViewModel : BaseViewModel
    {        
        public string ErrorMessage { get; set; }

        public LoginPageViewModel()
        {
            
        }
    }
}

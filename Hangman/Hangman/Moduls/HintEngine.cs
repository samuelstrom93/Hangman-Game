using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.Moduls
{
    class HintEngine : BaseViewModel
    {
        public string Hint { get; set; }
        public ICommand ShowHintCommand { get; set; }
        public bool IsHintShown { get; set; }

        public HintEngine(string hint)
        {
            Hint = hint;
            IsHintShown = false;
            ShowHintCommand = new RelayCommand(ShowHint);
        }

        public void ShowHint()
        {
            if (IsHintShown == true)
            {
                IsHintShown = false;
            }
            else
            {
                IsHintShown = true;
            }
        }

    }
}

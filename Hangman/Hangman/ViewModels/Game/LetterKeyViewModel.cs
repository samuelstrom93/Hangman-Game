using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class LetterKeyViewModel : BaseViewModel
    {
        public char Content { get; set; }
        public ICommand LetterKeyCommand { get; set; }
        public object CommandParameter => Content;

        public double Opacity { get; set; } = 1;
        public string Foreground { get; set; } = "White";
        public string FontWeight { get; set; } = "Normal";

        public readonly int QwertyOrder;

        public LetterKeyViewModel(int qwertyOrder, char content, ICommand keyCommand)
        {
            QwertyOrder = qwertyOrder;
            Content = content;
            LetterKeyCommand = keyCommand;
        }
    }
}

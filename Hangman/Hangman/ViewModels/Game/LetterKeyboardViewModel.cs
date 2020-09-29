using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hangman.ViewModels
{
    public class LetterKeyboardViewModel : BaseViewModel
    {
        private static readonly char[] _lettersQWERTY = "QWERTYUIOPÅASDFGHJKLÖÄZXCVBNM".ToCharArray();

        public ObservableCollection<LetterKeyViewModel> Keys { get; set; }
        public bool IsQWERTYChecked { get; set; } = true;
        public ICommand KeyboardLayoutCommand { get; set; }

        private readonly ICommand _keyCommand;

        public LetterKeyboardViewModel(ICommand keyCommand)
        {
            _keyCommand = keyCommand;
            KeyboardLayoutCommand = new RelayCommand(OrderKeys);

            Keys = new ObservableCollection<LetterKeyViewModel>();

            int qwertyIndex = 0;
            foreach (var c in _lettersQWERTY)
            {
                Keys.Add(new LetterKeyViewModel(qwertyIndex, c, _keyCommand));
                qwertyIndex++;
            }
        }

        public void OrderKeys()
        {
            Keys = new ObservableCollection<LetterKeyViewModel>(Keys.OrderBy(o => IsQWERTYChecked ? o.QwertyOrder : o.Content));
        }

        public void MarkLetterCorrect(char letter)
        {
            var key = Keys.SingleOrDefault(o => o.Content == letter);

            key.Opacity = 0.3;
            key.Foreground = "Green";
            key.FontWeight = "Bold";
        }

        public void MarkLetterIncorrect(char letter)
        {
            var key = Keys.SingleOrDefault(o => o.Content == letter);

            key.Opacity = 0.3;
            key.Foreground = "Red";
            key.FontWeight = "Bold";
        }
    }
}

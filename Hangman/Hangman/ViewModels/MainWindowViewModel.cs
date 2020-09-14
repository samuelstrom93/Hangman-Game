using Hangman.GameLogics;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Views 
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string PlayerName { get; set; } = "Demo Player";

        public MainWindowViewModel()
        {

            if (PlayerEngine.ActivePlayer !=null)
                PlayerName = PlayerEngine.ActivePlayer.Name;
        }

        public void UpdateActivePlayer()
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
        }
    }
}

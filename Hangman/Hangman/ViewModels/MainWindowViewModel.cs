using Hangman.GameLogics;
using Hangman.Models;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Views 
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string PlayerName { get; set; } 
        private string selectedMenu ;

        public MainWindowViewModel()
        {

        }

        public void UpdateActivePlayer()
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
        }

        public void TakeSelectedMenu(string SelectedMenu)
        {
            selectedMenu = SelectedMenu;
        }
    }
}

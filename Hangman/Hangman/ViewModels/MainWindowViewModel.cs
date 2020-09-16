using Hangman.GameLogics;
using Hangman.Models;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace Hangman.Views 
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string PlayerName { get; set; } = "Meny";
        public Visibility Visibility { get; set; }

        public MainWindowViewModel()
        {

        }

        public void UpdateActivePlayer()
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
        }
    }
}

using Hangman.Modules;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public IPlayer Player;
        private GamePageViewModel gamePageViewModel;

        public GamePage(IPlayer player = null, bool isPlayAgain = false, BaseViewModel specificModel = null)
        {

            InitializeComponent();
            Player = player;

            gamePageViewModel = new GamePageViewModel(isPlayAgain);
            DataContext = gamePageViewModel;

        }
    }
}

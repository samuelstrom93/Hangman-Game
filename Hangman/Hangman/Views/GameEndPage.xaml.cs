using Hangman.Models;
using Hangman.ViewModels;
using static Hangman.Repositories.PlayerRepository;
using System;
using System.Collections.Generic;
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
using Hangman.ViewModels.Base;

namespace Hangman.Views
{
    /// <summary>
    /// Gameover_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class GameEndPage : Page
    {
        private GameEndPageViewModel gameEndPageViewModel;

        public GameEndPage(Game game, Word word)
        {
            InitializeComponent();
            gameEndPageViewModel = new GameEndPageViewModel(game, word);
            DataContext = gameEndPageViewModel;
        }

        public GameEndPage(BaseViewModel specificModel)
        {
            InitializeComponent();
            DataContext = specificModel;
        }
        public GameEndPage()
        {
            InitializeComponent();
        }

    }
}

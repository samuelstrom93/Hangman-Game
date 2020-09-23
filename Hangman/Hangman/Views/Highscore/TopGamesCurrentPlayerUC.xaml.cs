using Hangman.Repositories;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Hangman.Views.Highscore
{
    /// <summary>
    /// Interaction logic for TopGamesCurrentPlayerUC.xaml
    /// </summary>
    public partial class TopGamesCurrentPlayerUC : UserControl
    {
        public TopGamesCurrentPlayerUC(int? playerId = null)
        {
            InitializeComponent();

            HighscoresViewModel vm = new HighscoresViewModel()
            {
                TopCurrentPlayerHighscores = HighscoreRepository.GetLeaderboard(playerId).ToList(),
                Title = playerId.HasValue ? "Dina 10 bästa spel" : " ",
            };

            DataContext = vm;
        }
    }
}

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
        public TopGamesCurrentPlayerUC()
        {
            InitializeComponent();

            HighscoreUCViewModel highscoreUCViewModel = new HighscoreUCViewModel(new HighscoreRepository());
            highscoreUCViewModel.Title = highscoreUCViewModel.TopCurrentPlayerHighscores == null 
                || highscoreUCViewModel.TopCurrentPlayerHighscores.Count() == 0
                ? "Inga spel registrerade" : "Dina bästa spel";
            
            DataContext = highscoreUCViewModel;
        }
    }
}

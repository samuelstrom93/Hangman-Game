using Hangman.Repositories;
using Hangman.ViewModels;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for TopPlayersUC.xaml
    /// </summary>
    public partial class TopPlayersUC : UserControl
    {
        public TopPlayersUC()
        {
            InitializeComponent();

            HighscoreUCViewModel highscoreUCViewModel = new HighscoreUCViewModel(new HighscoreRepository())
            {
                Title = "Topp flitigaste spelarna"
            };
            DataContext = highscoreUCViewModel;
        }
    }
}

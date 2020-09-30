
using Hangman.Repositories;
using Hangman.ViewModels;
using System.Linq;
using System.Windows.Controls;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for TopGamesUC.xaml
    /// </summary>
    public partial class TopGamesUC : UserControl
    {
        public TopGamesUC(int? playerId = null)
        {
            InitializeComponent();

            HighscoreUCViewModel highscoreUCViewModel = new HighscoreUCViewModel(new HighscoreRepository())
            {
                Title = "Topp tio bästa spel"
            };
            DataContext = highscoreUCViewModel;
        }
    }
}

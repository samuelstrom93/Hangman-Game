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

            HighscoresViewModel vm = new HighscoresViewModel()
            {
                TopHighscores = HighscoreRepository.GetLeaderboard(playerId).ToList(),
                Title = playerId.HasValue ? "Dina 10 bästa spel" : "Top 10 bästa spel",
            };
            DataContext = vm;
        }
    }
}

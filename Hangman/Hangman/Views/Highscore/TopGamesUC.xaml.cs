
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
                HighscoreRepository = new HighscoreRepository(),
                Title = playerId.HasValue ? "Dina 10 bästa spel" : "Topp 10 bästa spel",
            };
            vm.TopHighscores = vm.HighscoreRepository.GetLeaderboard(playerId).ToList();
            if (vm.TopHighscores.Count == 0)
            {
                vm.Title = "Du har inga spel registrerade.";
            }
            DataContext = vm;
        }
    }
}

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

            HighscoresViewModel vm = new HighscoresViewModel()
            {
                TopDiligentPlayers = HighscoreRepository.GetTopDiligentPlayers() as Dictionary<string, long>
            };

            DataContext = vm;
        }
    }
}

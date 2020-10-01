using System.Windows.Controls;

using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for PlayerStatsUC.xaml
    /// </summary>
    public partial class PlayerStatsUC : UserControl
    {

        private PlayerStatsUCViewModel model;

        public PlayerStatsUC()
        {
            InitializeComponent();
            model = new PlayerStatsUCViewModel();
            DataContext = model;             
        }
    }
}

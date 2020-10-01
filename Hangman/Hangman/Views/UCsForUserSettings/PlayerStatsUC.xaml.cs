using System.Windows.Controls;
using Hangman.Views.UCsForUserSettings;
using Hangman.ViewModels;

namespace Hangman.Views.UCsForUserSettings
{
    /// <summary>
    /// Interaction logic for PlayerStatsUC.xaml
    /// </summary>
    public partial class PlayerStatsUC : UserControl
    {


        public PlayerStatsUC()
        {
            InitializeComponent();
            DataContext = new PlayerStatsUCViewModel();
        }
    }
}

using System.Windows.Controls;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using Hangman.Views.UCsForUserSettings;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for UserSettingsPage.xaml
    /// </summary>
    public partial class UserSettingsPage : Page
    {
        public UserSettingsPage(BaseViewModel specificModel = null)
        {
            InitializeComponent();
            DataContext = specificModel ?? new UserSettingsViewModel();
        }
    }
}

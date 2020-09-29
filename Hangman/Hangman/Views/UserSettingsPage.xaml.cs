using System;
using System.Collections.Generic;
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
using Hangman.Modules;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using Hangman.Views.Menu;
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
            MessageFrame.Content = new SendMessageUC();
            UserStatsFrame.Content = new PlayerStatsUC();
        }
    }
}

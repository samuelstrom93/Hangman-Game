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

            UserStatsFrame.Content = new PlayerStatsUC();
        }

        //private void Button_DeleteUser_Click(object sender, RoutedEventArgs e)
        //{
        //    string name = txtBoxInsertUser.Text;
        //    bool result;

        //    result = usVM.CheckIfDeletable(name);

        //    if (result)
        //    {
        //        usVM.DeleteUser();
        //        MessageBox.Show("Din användare är nu radarad, du loggas nu ut.");
        //        this.NavigationService.Content = new LoginPage();
        //    }

        //    txtBoxInsertUser.Clear();
        //    DataContext = usVM;

        //}

        //private void Button_UpdateUser_Click(object sender, RoutedEventArgs e)
        //{
        //    string name = txtBoxInsertName.Text;

        //    //Uppdatera Player Engine
        //    usVM.UpdateUser(PlayerModule.GetActivePlayer(), name);

        //    txtBoxInsertName.Clear();
        //    DataContext = usVM;
        //}

    }
}

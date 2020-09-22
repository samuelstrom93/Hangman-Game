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
using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using Hangman.Views.ErrorMessage;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(BaseViewModel specificModel = null)
        {
            InitializeComponent();
            DataContext = specificModel ?? new LoginPageViewModel();
          
        }

    }
}

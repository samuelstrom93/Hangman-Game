using Hangman.Modules;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;
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

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GameStartPage.xaml
    /// </summary>
    public partial class GameStartPage : Page
    {
        public GameStartPage(BaseViewModel specificModel = null)
        {
            InitializeComponent();
            DataContext = new HighscoresViewModel();

            DataContext = specificModel ?? new GameStartViewModel();
        }
    }
}

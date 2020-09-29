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
using Hangman.Repositories;
using Hangman;
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

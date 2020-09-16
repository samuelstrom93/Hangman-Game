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

namespace Hangman.Views.PlayerStats
{
    /// <summary>
    /// Interaction logic for PlayerStats_Page.xaml
    /// </summary>
    public partial class PlayerStats_Page : Page
    {
        public PlayerStats_Page()
        {
            InitializeComponent();
            PlayerStats.Content = new PlayerStatsUC();
        }
    }
}

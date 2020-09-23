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
using Microsoft.Win32;
using System.Media;


namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for StartUppPage.xaml
    /// </summary>
    public partial class StartUppPage : Page
    {
        SoundPlayer sounds = new SoundPlayer();

        public StartUppPage()
        {
            InitializeComponent();
            sounds.SoundLocation = "Assets/Sounds/PunchesFX.wav";
            sounds.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Content = new GameIntroPage();
            sounds.Stop();
        }
    }
}

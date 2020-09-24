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
using Hangman.ViewModels;
using Hangman.ViewModels.Base;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for StartUppPage.xaml
    /// </summary>
    public partial class StartUpPage : Page
    {
        SoundPlayer sounds = new SoundPlayer();

        public StartUpPage(BaseViewModel specificModel = null)
        {
            InitializeComponent(); 
            DataContext = specificModel ?? new StartUpPageViewModel();
            //  sounds.SoundLocation = "Assets/Sounds/PunchesFX.wav";
            //  sounds.Play();
        }



    }
}

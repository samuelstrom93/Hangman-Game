using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels;
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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
            //DataContext = new GamePageViewModel();
            DataContext = gamePageViewModel;
        
        }
        private GamePageViewModel gamePageViewModel = new GamePageViewModel();


        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            if (gamePageViewModel.IsGameStart)
            {
                ChangeBtnStyle((Button)sender);
                ((Button)sender).IsEnabled = false;
            }


        }

        private void ChangeBtnStyle(Button sender)
        {
            if (gamePageViewModel.IsGuessCorrect)
            {
                //((Button)sender).Background = Brushes.Green;
                sender.Opacity = 0.5;
                sender.BorderThickness = new Thickness(0, 0, 0, 0);
                sender.BorderBrush = null;
                sender.Foreground = Brushes.Green;
            }
            else
            {
                //((Button)sender).Background = Brushes.Red;
                sender.Opacity = 0.5;
                sender.BorderThickness = new Thickness(0, 0, 0, 0);
                sender.BorderBrush = null;
                sender.Foreground = Brushes.Red;

            }

        }
    }
}

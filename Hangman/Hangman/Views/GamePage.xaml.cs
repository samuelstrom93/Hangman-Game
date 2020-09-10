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
            DataContext = new GamePageViewModel();
        }
        Word word;

        private void TryWordContainsLetter(string s)
        {
            try
            {
                if (word.Name.Contains(s.ToLower()) || word.Name.Contains(s.ToUpper()))
                {
                    btnA.Background = Brushes.Green;
                }
                else
                {
                    btnA.Background = Brushes.Red;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void StartaSpel_click(object sender, RoutedEventArgs e)
        {
            word = Word_Repository.GetRandomWord();
            // Gör en metod för att konverta/resetta tillbaka knappar till originalfärg?

        }

        private void Hint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblHint.Content = word.Hint;
            }
            catch (Exception)
            {

                throw;
            }
            
        }



        #region Knappar A-Ö
        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            TryWordContainsLetter(btnA.Content.ToString());
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            TryWordContainsLetter(btnB.Content.ToString());
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            TryWordContainsLetter(btnC.Content.ToString());
        }



        #endregion


    }
}

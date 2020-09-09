using Hangman.Models;
using Hangman.Repositories;
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
        }


        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void StartaSpel_click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            // id för orden i databasen är just mellan dessa siffror för tillfället
            int randNum = random.Next(46, 50);
            Word word = Word_Repository.GetRandomWord(randNum);
        }
    }
}

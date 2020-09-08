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
    /// CreateUser_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class CreateUser_Page : Page
    {
        public CreateUser_Page()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GamePage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}

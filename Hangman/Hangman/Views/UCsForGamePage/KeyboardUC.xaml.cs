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

namespace Hangman.Views.UCsForGamePage
{
    /// <summary>
    /// Keyboard.xaml の相互作用ロジック
    /// </summary>
    public partial class KeyboardUC : UserControl
    {
        public KeyboardViewModel KeyboardViewModel { get; set; }
        public KeyboardUC()
        {
            InitializeComponent();
            KeyboardViewModel = new KeyboardViewModel();
            DataContext = KeyboardViewModel;

        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            KeyboardViewModel.SetSelectedKey(button.Content.ToString());
        }
    }
}

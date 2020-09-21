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
        public KeyboardUC()
        {
            InitializeComponent();
        }

        public KeyboardViewModel SetDataContext(Button button)
        {
            KeyboardViewModel keyboardViewModel = new KeyboardViewModel(button.Content.ToString());
            DataContext = keyboardViewModel;
            return keyboardViewModel;
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            SetDataContext((Button)sender);
            /*JudgeGameFromLetterClick(((Button)sender));
            ViewGameEndPage();*/
        }
    }
}

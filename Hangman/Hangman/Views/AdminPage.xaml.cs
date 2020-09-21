using Hangman.ViewModels;
using Hangman.ViewModels.Base;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private readonly AdminViewModel _vm;
        public AdminPage()
        {
            InitializeComponent();

            _vm = new AdminViewModel();
            DataContext = new AdminViewModel();

            MessageView.Content = new MessageBoardUC();
        }

        public AdminPage(BaseViewModel specificModel)
        {
            InitializeComponent();
            DataContext = specificModel;
        }

        private void TextBoxWord_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxWord.Text.Equals(AdminViewModel.WordPlaceholder))
            {
                TextBoxWord.Text = string.Empty;
            }
        }

        private void TextBoxWord_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxWord.Text))
            {
                TextBoxWord.Text = AdminViewModel.WordPlaceholder;
            }
        }

        private void TextBoxDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxDescription.Text.Equals(AdminViewModel.DescriptionPlaceholder))
            {
                TextBoxDescription.Text = string.Empty;
            }
        }

        private void TextBoxDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxDescription.Text))
            {
                TextBoxDescription.Text = AdminViewModel.DescriptionPlaceholder;
            }
        }
    }
}

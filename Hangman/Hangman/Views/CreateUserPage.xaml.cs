using System.Windows.Controls;
using Hangman.ViewModels;

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
            DataContext = new CreateUserViewModel();
        }
    }
}

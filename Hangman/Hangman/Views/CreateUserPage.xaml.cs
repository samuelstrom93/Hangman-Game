using System.Windows.Controls;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;

namespace Hangman.Views
{
    /// <summary>
    /// CreateUser_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class CreateUser_Page : Page
    {
        public CreateUser_Page(BaseViewModel specificModel = null)
        {
            InitializeComponent();
            DataContext = specificModel ?? new CreateUserViewModel();
        }
    }
}

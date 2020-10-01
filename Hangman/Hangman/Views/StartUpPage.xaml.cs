using System.Windows.Controls;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for StartUppPage.xaml
    /// </summary>
    public partial class StartUpPage : Page
    {

        public StartUpPage(BaseViewModel specificModel = null)
        {
            InitializeComponent(); 
            DataContext = specificModel ?? new StartUpPageViewModel();
        }
    }
}

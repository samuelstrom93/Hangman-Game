using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GameIntroPage.xaml
    /// </summary>
    public partial class GameIntroPage : Page
    {
        public GameIntroPage(BaseViewModel specificModel = null)
        {
            InitializeComponent();
        }
    }
}

using System.Windows.Controls;
using Hangman.ViewModels;

namespace Hangman.Views.Menu
{
    /// <summary>
    /// Interaction logic for TopMenuUC.xaml
    /// </summary>
    public partial class TopMenuUC : UserControl
    {
        public TopMenuUC()
        {
            InitializeComponent();
            DataContext = new TopMenuViewModel();
        }
    }
}

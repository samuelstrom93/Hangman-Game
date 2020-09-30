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

namespace Hangman.Views.UCsForUserSettings
{
    /// <summary>
    /// Interaction logic for SendMessageUC.xaml
    /// </summary>
    public partial class SendMessageUC : UserControl
    {
        public SendMessageUCViewModel sendMessageVM;
        public SendMessageUC()
        {
            InitializeComponent();
            sendMessageVM = new SendMessageUCViewModel();
            DataContext = sendMessageVM;
        }
    }
}

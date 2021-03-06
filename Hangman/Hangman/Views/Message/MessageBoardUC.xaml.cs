﻿using Hangman.Modules;
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

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for MessageBoardUC.xaml
    /// </summary>
    public partial class MessageBoardUC : UserControl
    {
        public MessageBoardUC()
        {
            InitializeComponent();

            DataContext = new MessagesViewModel();
        }
    }
}

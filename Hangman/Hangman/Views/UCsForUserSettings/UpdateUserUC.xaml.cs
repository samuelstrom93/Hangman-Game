﻿using Hangman.ViewModels;
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
    /// Interaction logic for UpdateUserUC.xaml
    /// </summary>
    public partial class UpdateUserUC : UserControl
    {
        public UpdateUserUCViewModel updateVM;
        public UpdateUserUC()
        {
            InitializeComponent();
            updateVM = new UpdateUserUCViewModel();
            DataContext = updateVM;
        }
    }
}

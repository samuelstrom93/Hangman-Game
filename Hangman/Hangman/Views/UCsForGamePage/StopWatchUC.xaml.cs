﻿using Hangman.Moduls;
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
    /// StopWatchUC.xaml の相互作用ロジック
    /// </summary>
    public partial class StopWatchUC : UserControl
    {
        public StopWatchUC(StopWatchUCViewModel stopWatchEngine)
        {
            InitializeComponent();
            DataContext = stopWatchEngine;
        }
    }
}

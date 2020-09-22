﻿using Hangman.ViewModels;
using System.Windows.Controls;

namespace Hangman.Views
{
    /// <summary>
    /// HighScore_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class HighScore_Page : Page
    {
        public HighScore_Page()
        {
            InitializeComponent();
            DataContext = new HighscoresViewModel();
        }
    }
}

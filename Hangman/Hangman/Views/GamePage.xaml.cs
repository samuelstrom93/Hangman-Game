﻿using Hangman.Models;
using Hangman.Repositories;
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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {

        public IPlayer Player;
        #region private field
        #endregion

        public GamePage()
        {
            InitializeComponent();
        }

        public GamePage(IPlayer player)
        {

            InitializeComponent();
            Player = player;
            DataContext = new GamePageViewModel(player);

        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            this.NavigationService.Content = new LoginPage();
        }

        private void mnuDeleteUser(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new DeleteUserPage(Player);
        }

        private void mnuUpdateUser(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new UpdateUserPage(Player);
        }

    }
}

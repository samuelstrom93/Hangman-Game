using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using Hangman.Views;
using Hangman.Views.PlayGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Hangman.Helper
{
    public static class NavigateToPageHelper
    {
        public static Page GetPage(ApplicationPage page, BaseViewModel model = null)
        {
            return page switch
            {
                ApplicationPage.Login => new LoginPage(model),
                ApplicationPage.GameIntro => new GameIntroPage(model),
                ApplicationPage.Admin => new AdminPage(model),
                ApplicationPage.CreateUser => new CreateUser_Page(model),
                ApplicationPage.GamePage => new PlayGamePage(model),
                ApplicationPage.UserSettings => new UserSettingsPage(model),
                ApplicationPage.HighscorePage => new HighScore_Page(model),
                ApplicationPage.StartUpPage => new StartUpPage(model),
                _ => null,
            };
        }
    }
}

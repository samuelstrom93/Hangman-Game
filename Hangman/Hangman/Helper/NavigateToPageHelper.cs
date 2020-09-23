using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Hangman.Helper
{
    public static class NavigateToPageHelper
    {
        /// <summary>
        /// Returnerar en Page utifrån vald sida
        /// </summary>
        /// <param name="page">Indata av  typen <see cref="ApplicationPage"/></param>
        /// <param name="model">Specifik vymodell som ska laddas, default = null <</param>
        /// <returns></returns>
        /// 
        
            //Ändrat Application -> Page - rätt eller fel?
        public static Page GetPage(ApplicationPage page, BaseViewModel model = null)
        {
            return page switch
            {
                ApplicationPage.Login => new LoginPage(model),
                ApplicationPage.GameIntro => new GameIntroPage(model),
                ApplicationPage.Admin => new AdminPage(model),
                ApplicationPage.CreateUser => new CreateUser_Page(model),
                ApplicationPage.GameStart => new GameStartPage(model), 
                ApplicationPage.GamePage => new GamePage(specificModel: model),
                ApplicationPage.GameEnd => new GameEndPage(model),
                ApplicationPage.UserSettings => new UserSettingsPage(model),
                ApplicationPage.HighscorePage => new HighScore_Page(model),
                ApplicationPage.StartUpPage => new StartUpPage(model),
                _ => null,

            };
        }
    }
}

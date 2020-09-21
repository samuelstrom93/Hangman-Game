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
                ApplicationPage.Login => model != null ? new LoginPage(model) : new LoginPage(),
                ApplicationPage.Admin => model != null ? new AdminPage(model) : new AdminPage(),
                ApplicationPage.CreateUser => model != null ? new CreateUser_Page(model) : new CreateUser_Page(),
                ApplicationPage.GameStart => model != null ? new GameStartPage(model) : new GameStartPage(), 
                ApplicationPage.GamePage => model != null ? new GamePage(model) : new GamePage(),
                ApplicationPage.GameEnd => model != null ? new GameEnd_Page(model) : new GameEnd_Page(),
                ApplicationPage.UserSettings => model != null ? new UserSettingsPage(model) : new UserSettingsPage(),
                _ => null,

            };
        }
    }
}

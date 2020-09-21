using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.Views.Menu;

namespace Hangman.GameLogics
{
    /// <summary>
    /// En klass som håller reda på properties och metoder som rör användaren
    /// </summary>
    class PlayerEngine
    {
        #region Properties
        public static IPlayer ActivePlayer { get; set; }
        #endregion

        public static readonly TopMenuUC _menu = new TopMenuUC();

        #region methods

        /// <summary>
        /// En metod för att kontrollera om namnet på Player används redan.
        /// </summary>
        public static bool IsNameUsed(string name)
        {
            Player player = PlayerRepository.GetPlayer(name);

            if (player != null)
            {
                return true;
            }

            else
                return false;
        }

        /// <summary>
        /// En metod för att tilldela ActiveUser en Player
        /// </summary>
        /// <param name="name"></param>
        public static void SetActivePlayer(string name)
        {
            PlayerEngine.ActivePlayer = PlayerRepository.GetPlayer(name);

            _menu.PlayerStatusChanged(ActivePlayer);
        }
        #endregion
    }

}


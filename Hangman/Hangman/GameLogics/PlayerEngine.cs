using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Models;
using Hangman.Repositories;

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

        #region methods

        /// <summary>
        /// En metod för att kontrollera om namnet på Player används redan.
        /// </summary>
        public static bool IsNameUsed(string name)
        {
            Player player = Player_Repository.GetPlayer(name);

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
            PlayerEngine.ActivePlayer = Player_Repository.GetPlayer(name);
        }
        #endregion
    }

}

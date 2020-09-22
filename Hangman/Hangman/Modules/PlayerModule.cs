using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.Views.Menu;

namespace Hangman.Modules
{
    /// <summary>
    /// En klass som håller reda på properties och metoder som rör användaren
    /// </summary>
    public class PlayerModule : IPlayerModule
    {
        private static IPlayer ActivePlayer { get; set; }

        #region methods

        /// <summary>
        /// En metod för att kontrollera om namnet på Player används redan.
        /// </summary>
        private bool IsNameUsed(string name)
        {
            Player player = PlayerRepository.GetPlayer(name);

            if (player != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// En metod för att tilldela ActiveUser en Player
        /// </summary>
        /// <param name="name"></param>
        private void SetActivePlayer(string name)
        {
            ActivePlayer = PlayerRepository.GetPlayer(name);

            TopMenuUC.Instance.PlayerStatusChanged(ActivePlayer);
        }

        public static IPlayer GetActivePlayer()
        {
            return ActivePlayer;
        }

        public void LogOutPlayer()
        {
            ActivePlayer = null;
        }

        public bool TryLogInPlayer(string name)
        {
            if (!IsNameUsed(name))
            {
                SetActivePlayer(name);
                return true;
            }

            return false;
        }

        public bool TryAddPlayer(string name, out IPlayer added)
        {
            added = null;
            if (!IsNameUsed(name))
            {
                added = PlayerRepository.CreatePlayer(name);
                if (added != null)
                {
                    SetActivePlayer(name);
                    return true;
                }
            }

            return false;
        }
        #endregion
    }

}


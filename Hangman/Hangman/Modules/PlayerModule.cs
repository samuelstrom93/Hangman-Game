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
        private bool IsNameUsed(string name)
        {
            Player player = PlayerRepository.GetPlayer(name);

            if (player != null)
                return true;
            else
                return false;
        }

        public bool TryLogInPlayer(string name)
        {
            if (IsNameUsed(name))
            {
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
                    return true;
                }
            }

            return false;
        }
    }

}


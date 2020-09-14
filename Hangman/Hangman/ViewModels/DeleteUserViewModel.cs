using System;
using System.Collections.Generic;
using System.Text;
using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Repositories;
using Npgsql;

namespace Hangman.ViewModels
{
    class DeleteUserViewModel
    {
        public string PlayerName { get; set; }
        public string Message { get; set; }

        public DeleteUserViewModel(IPlayer player)
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
        }
        #region methods
        /// <summary>
        /// Metod för att ta bort en användare
        /// </summary>
        public void DeleteUser(string name)
        {
            if (name != "")
            {
                try
                {
                    Player_Repository.DeletePlayer(PlayerEngine.ActivePlayer.Id);
                    Message = "Din användare är nu raderad. Hoppas vi ses snart igen.";
                }

                catch (PostgresException ex)
                {
                    //ta fram koden om användaren inte existerar mha returning id.
                    if (ex.SqlState.Contains("23505"))
                    {
                        Message = "Du har valt ett namn som är upptaget - försök igen";
                    }

                    else
                    {
                        Message = "Något gick fel - försök igen";
                    }
                }

            }

            else
            {
                Message = "Du måste ange ditt namn";
            }
        }
    }
    #endregion
}

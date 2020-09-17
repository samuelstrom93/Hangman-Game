using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Repositories;
using Npgsql;
using Hangman.ViewModels.Base;

namespace Hangman.ViewModels
{
    class CreateUserViewModel : BaseViewModel
    {
        public string PlayerName;
        public string Message;

        #region methods

        /// <summary>
        /// En metod för att kontrollera om användarmnamnet redan finns i databasen
        /// </summary>   
        public string CreatePlayer(string name)
        {
            string Message; 

            if(name!= "")
            {
                try
                {
                    Player_Repository.CreatePlayer(name);
                    Message = $"Grattis {name}! Du är nu medlem och kan logga in!";
                }

                catch (PostgresException ex)
                {
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
                Message = "Du måste skriva något.";
            }
            return Message;
        }
        #endregion;
    }
}


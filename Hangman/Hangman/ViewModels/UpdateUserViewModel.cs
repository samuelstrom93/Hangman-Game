using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
using Npgsql;

class UpdateUserViewModel : BaseViewModel
{
    #region properties
    public string Message { get; set; }
    public ICommand UppdateUserCommand { get; set; }
    public IPlayer Player { get; set; }
    public string PlayerName { get; set; }
    public string ButtonName { get; set; } = "Byt ditt namn";

    #endregion


    public UpdateUserViewModel(IPlayer player)
    {
        if(PlayerEngine.ActivePlayer.Name!= null)
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;

        }
    }

    public void UpdateButton()
    {
        ButtonName = "Ditt namn är nu bytt";
    }

    #region Methods
    /// <summary>
    /// Metod för att uppdatera en användare
    /// </summary>
    public void UpdateUser(IPlayer player, string wantedName)
    {

        if (wantedName != "" && wantedName != PlayerEngine.ActivePlayer.Name)
        {
            try
            {
                Player_Repository.UpdateNameOnPlayer(wantedName, PlayerEngine.ActivePlayer.Id);
                PlayerEngine.ActivePlayer = Player_Repository.GetPlayer(wantedName);
                PlayerName = PlayerEngine.ActivePlayer.Name;
                UppdateUserCommand = new RelayCommand(UpdateButton);
                Message = "Ditt användarnamn är nu bytt till " + wantedName;
            }

            catch (PostgresException ex)
            {
                //ta fram koden om användaren inte existerar
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

        else if (wantedName == PlayerEngine.ActivePlayer.Name)
        {
            Message = "Du måste ange ett nytt namn";
        }

        else if (wantedName=="")
            Message = "Du måste ange ett namn";

        else
        {
            Message = "Något gick fel";
        }
    }
    #endregion
}

using Hangman.ViewModels.Base;
using Hangman.Models;
using System;
using System.Windows.Media.Imaging;
using Hangman.Repositories;
using Hangman.Modules;
using System.Windows.Input;
using Npgsql;
using Hangman.Database;

namespace Hangman.ViewModels
{
    public class UpdateUserUCViewModel : BaseViewModel
    {
        #region Properties: Update User
        public string NewName { get; set; }
        public string UpdateMessage { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        public IPlayer Player { get; set; }
        public string TextColor { get; set; }
        public string BackGroundColorUpdateBox { get; set; }
        #endregion

        #region Repos
        public IPlayerRepository playerRepository;
        #endregion

        public UpdateUserUCViewModel()
        {
            playerRepository = new PlayerRepository();
            UpdateUserCommand = new RelayCommand(UpdateUser);

        }
        #region Methods: Update User

        public void UpdateUser()
        {
            if (!string.IsNullOrWhiteSpace(NewName) && NewName != ActivePlayerName && !NewName.Contains(" "))
            {
                try
                {
                    playerRepository.UpdateNameOnPlayer(NewName, ActivePlayer.Id);
                    var module = new PlayerModule();

                    module.TryLogInPlayer(NewName);
                    SetActivePlayer(NewName);
                    ActivePlayerName = NewName;

                    UpdateMessage = "Ditt användarnamn är nu bytt till " + NewName;
                    SetSuccessMessageBox();
                                     
                }

                catch (PostgresException ex)
                {
                    
                    if (ex.SqlState.Contains("23505"))
                    {
                        SetErrorMessageBox();
                        UpdateMessage = "Du har valt ett namn som är upptaget - försök igen";
                    }

                    else
                    {
                        SetErrorMessageBox();
                        UpdateMessage = "Något gick fel - försök igen";
                    }
                }
            }

            else if (NewName == ActivePlayerName)
            {
                SetErrorMessageBox();
                UpdateMessage = "Du måste ange ett nytt namn";
            }

            else if (NewName == null)
            {
                SetErrorMessageBox();
                UpdateMessage = "Du måste ange ett namn";
            }


            else if (NewName.Contains(" "))
            {
                SetErrorMessageBox();
                UpdateMessage = "Du får inte ha mellanslag i ditt namn";
            }


            else
            {
                SetErrorMessageBox();
                UpdateMessage = "Något gick fel";
            }
        }

        private void SetErrorMessageBox()
        {
            TextColor = "red";
            NewName = "";
            BackGroundColorUpdateBox = "white";
        }

        private void SetSuccessMessageBox()
        {
            NewName = "";
            TextColor = "green";
            BackGroundColorUpdateBox = "white";
        }
        #endregion
    }
}

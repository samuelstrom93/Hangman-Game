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
        #region Properties
        public string NewName { get; set; }
        public string UpdateMessage { get; set; }
        public IPlayer Player { get; set; }
        public string TextColor { get; set; }
        public string BackGroundColorUpdateBox { get; set; }
        #endregion

        #region Commands
        public ICommand UpdateUserCommand { get; set; }
        #endregion

        #region Repos
        private readonly IPlayerRepository playerRepository;
        public IPlayerModule _module;
        #endregion

        public UpdateUserUCViewModel()
        {
            playerRepository = new PlayerRepository();
            UpdateUserCommand = new RelayCommand(UpdateUser);
            _module = new PlayerModule();

        }
        #region Methods: Update User
        public void UpdateUser()
        {
            if (!string.IsNullOrWhiteSpace(NewName) && NewName != ActivePlayerName && !NewName.Contains(" "))
            {

                if (_module.TryUpdatePlayerName(ActivePlayer, NewName))
                {
                    SetNewName();                   
                    UpdateMessage = "Ditt användarnamn är nu bytt till \n" + NewName;
                    SetSuccessMessageBox();
                }

                else
                {
                    SetErrorMessageBox();
                    UpdateMessage = "Du har valt ett namn som är \nupptaget - försök igen";
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
        public void SetNewName()
        {
            var module = new PlayerModule();
            module.TryLogInPlayer(NewName);
            SetActivePlayer(NewName);
            ActivePlayerName = NewName;
        }
        #endregion

        #region Methods: Design
        private void SetErrorMessageBox()
        {
            TextColor = "red";
            NewName = null;
            BackGroundColorUpdateBox = "white";
        }

        private void SetSuccessMessageBox()
        {
            NewName = null;
            TextColor = "green";
            BackGroundColorUpdateBox = "white";
        }

        #endregion

        
    }
}

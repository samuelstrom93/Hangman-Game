using Hangman.ViewModels.Base;
using Hangman.Repositories;
using System.Windows.Input;
using System.Windows;
using Hangman.Database;
namespace Hangman.ViewModels
{
    public class DeleteUserUCViewModel : BaseViewModel
    {
        #region Properties: Delete User
        public string NameCheck { get; set; }
        public bool IsDeletable { get; set; }
        public string DeleteMessage { get; set; }
        public string BackGroundColorDeleteBox { get; set; }
        #endregion

        #region Commands
        public ICommand DeleteUserCommand { get; set; }
        #endregion

        #region Repos
        private readonly IPlayerRepository playerRepository;
        #endregion

        public DeleteUserUCViewModel()
        {
            playerRepository = new PlayerRepository();
            DeleteUserCommand = new RelayCommand(TryDeleteUser);
        }

        #region Methods: Delete User

        private void TryDeleteUser()
        {
            if (CheckIfDeletable(NameCheck))
            {
                DeleteUser();               
                GoToPage(ApplicationPage.StartUpPage);
            }

            else
            {
                UpdateDeleteMessage();
            }
        }

        public bool CheckIfDeletable(string name)
        {
            if (name == ActivePlayer.Name)
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        public void UpdateDeleteMessage()
        {
            BackGroundColorDeleteBox = "white";
            DeleteMessage = "Du har skrivit in fel användarnamn.";
        }

        public void DeleteUser()
        {
            playerRepository.DeletePlayer(ActivePlayer.Id);
            MessageBox.Show("Din användare är nu raderad, du loggas nu ut.");
            SetActivePlayer(null);
        }

        #endregion
    }
}

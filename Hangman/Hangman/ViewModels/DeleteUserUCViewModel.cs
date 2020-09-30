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
        public ICommand DeleteUserCommand { get; set; }
        public string BackGroundColorDeleteBox { get; set; }

        #endregion

        #region Repos
        public IPlayerRepository playerRepository;
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
                MessageBox.Show("Din användare är nu raderad, du loggas nu ut.");
                SetActivePlayer(null);
                GoToPage(ApplicationPage.Login);
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
                BackGroundColorDeleteBox = "white";
                DeleteMessage = "Du har skrivit in fel användarnamn.";
                return false;
            }

        }

        public void DeleteUser()
        {
            playerRepository.DeletePlayer(ActivePlayer.Id);
        }

        #endregion
    }
}

using Hangman.ViewModels.Base;
using System;
using System.Windows.Media.Imaging;
using Hangman.Repositories;
using Hangman.Modules;
using System.Windows.Input;
using Npgsql;
using Hangman.Database;

namespace Hangman.ViewModels
{
    class UserSettingsViewModel : BaseViewModel
    {
        public UpdateUserUCViewModel UpdateUserUCViewModel { get; set; }
        public DeleteUserUCViewModel DeleteUserUCViewModel { get; set; }
        public SendMessageUCViewModel SendMessageUCViewModel { get; set; }

        public PlayerStatsUCViewModel PlayerStatsUCViewModel { get; set; }
        public UserSettingsViewModel()
        {
            UpdateUserUCViewModel = new UpdateUserUCViewModel();
            DeleteUserUCViewModel = new DeleteUserUCViewModel();
            SendMessageUCViewModel = new SendMessageUCViewModel();
            PlayerStatsUCViewModel = new PlayerStatsUCViewModel();
        }
    }
}

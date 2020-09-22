using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.GameRepository;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hangman.Repositories;
using Hangman.Moduls;
using System.Windows.Controls;

namespace Hangman.ViewModels
{
    class GameEndPageViewModel : BaseViewModel
    {

        public ICommand DeleteGameScoreCommand { get; set; } //Binding i GameEnd_Page
        public GameEndEngine GameEndEngine { get; set; }

        public GameEndPageViewModel(Game game, Word word)
        {
            GameEndEngine = new GameEndEngine(game, word);
            
            
            DeleteGameScoreCommand = new RelayCommand(DeleteGameScore);
        }

        private void DeleteGameScore()
        {
            GameEndEngine.DeleteGameScore();
            GameEndEngine.ChangeBtnStyle();
        }


    }
}

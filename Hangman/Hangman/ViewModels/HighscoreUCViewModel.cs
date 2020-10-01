using Hangman.Models;
using Hangman.Repositories;
using Hangman.Views;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Hangman.Database;
using System.Linq;

namespace Hangman.ViewModels
{
    public class HighscoreUCViewModel : BaseViewModel
    {
        public ObservableCollection<HighscoreGame> TopHighscores { get; set; }
        public ObservableCollection<HighscoreGame> TopCurrentPlayerHighscores { get; set; }

        public Dictionary<string, long> TopDiligentPlayers { get; set; }

        public string Title { get; set; }
        
        public IHighscoreRepository HighscoreRepository { get; set; }


        public HighscoreUCViewModel(IHighscoreRepository highscoreRepository)
        {
            HighscoreRepository = highscoreRepository;

            TopDiligentPlayers = HighscoreRepository.GetTopDiligentPlayers(10) as Dictionary<string, long>;
            TopHighscores = HighscoreRepository.GetLeaderboard(null);

            if (ActivePlayer != null)
            {
                TopCurrentPlayerHighscores = HighscoreRepository.GetLeaderboard(ActivePlayer.Id);
            }
        }
    }
}

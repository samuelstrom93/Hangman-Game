using Hangman.Models;
using Hangman.Repositories;
using Hangman.Views;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hangman.ViewModels
{
    public class HighscoresViewModel : BaseViewModel
    {
        // Gör om till ObservableCollection?
        public List<HighscoreGame> TopHighscores { get; set; }
        public List<HighscoreGame> TopCurrentPlayerHighscores { get; set; }

        public Dictionary<string, long> TopDiligentPlayers { get; set; }

        public string Title { get; set; }

        public HighscoreRepository HighscoreRepository { get; set; }


        public HighscoresViewModel()
        {
        }
    }
}

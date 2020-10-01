using Hangman.Models;
using System;
using Hangman.Repositories;
using System.Windows.Media.Imaging;
using Hangman.ViewModels.Base;
using System.Windows.Input;
using Hangman.Views;
using Hangman.Database;

namespace Hangman.ViewModels
{
    class PlayerStatsUCViewModel : BaseViewModel
    {
        #region Properties: PlayerStatsUC
        public string GamesPlayed { get; set; }
        public string GamesWon { get; set; }
        private double WinRate { get; set; }
        public string WinRateString { get; set; }
        public string Title { get; set; }
        private string PlayerStatus { get; set; }
        public BitmapImage MemeForWinRate { get; set; }
        public string LabelColor { get; set; }
        public string BackgroundColorWinRate { get; set; } = "Transparent";
        public ICommand ViewImage { get; set; }
        #endregion

        #region Repositores
        private IPlayerStatsRepository playerStatsRepository;
        #endregion

        public PlayerStatsUCViewModel()
        {
            playerStatsRepository = new PlayerStatsRepository();
            UpdatePlayerStats();
        }

        #region Methods: PlayerStatsUC
        public void UpdatePlayerStats()
        {
            if (ActivePlayer != null)
            {
                GetGamesPlayed();
                GetGamesWon();
                CalculateWinRate();
                SetPlayerStatus();
                SetWinRate();
                ChangeMemeWithWinRate();
            }
        }
     
        #region Methods: Get games
        public void GetGamesPlayed()
        {
            GamesPlayed = playerStatsRepository.GetGamesPlayed(ActivePlayer).ToString();
        }

        public void GetGamesWon()
        {
            GamesWon = playerStatsRepository.GetGamesWon(ActivePlayer).ToString();
        }

        #endregion
        #region Methods: Calculations
        public void CalculateWinRate()
        {
            double gamesPlayed = playerStatsRepository.GetGamesPlayed(ActivePlayer);
            double gamesWon = playerStatsRepository.GetGamesWon(ActivePlayer);

            if (gamesPlayed == 0)
            {
                WinRate = 0;
            }
            else if (gamesWon>=1)
            {
                double dec = (gamesWon / gamesPlayed) * 100;
                WinRate = Math.Round(dec, 2);
            }
            else if (gamesWon < 1)
            {
                double gamesLost = gamesPlayed;
                double dec = (-gamesLost / gamesPlayed) * 100;
                WinRate = Math.Round(dec, 2);

            }
        }
        #endregion
        #region Methods: Update Design
        public void SetWinRate()
        {
            WinRateString = $"{WinRate} %";
        }
        private void ChangeMemeWithWinRate()
        {
            string imageAdress;
            imageAdress = $"../../../Assets/Images/{PlayerStatus}.jpg";

            string currentPath = Environment.CurrentDirectory;
            MemeForWinRate = new BitmapImage(new Uri(System.IO.Path.Combine(currentPath, imageAdress)));
        }
        public void SetPlayerStatus()
        {
            if (WinRate >= 50)
            {
                BackgroundColorWinRate = "black";
                PlayerStatus = "YouRock";
                LabelColor = "#FFD3F5C4";
            }

            if (WinRate == 13.37)
            {
                BackgroundColorWinRate = "black";
                PlayerStatus = "You1337";
                LabelColor = "yellow";
            }

            else if (WinRate < 50 && WinRate > 30)
            {
                BackgroundColorWinRate = "black";
                PlayerStatus = "YouAverage";
                LabelColor = "yellow";
            }

            else if (WinRate <= 30)
            {
                BackgroundColorWinRate = "black";
                PlayerStatus = "YouSuck";
                LabelColor = "red";
            }

            if (WinRate == 0)
            {
                BackgroundColorWinRate = "white";
                PlayerStatus = "YouEmpty";
                LabelColor = "black";
            }
        }
        #endregion

        #endregion

    }
}

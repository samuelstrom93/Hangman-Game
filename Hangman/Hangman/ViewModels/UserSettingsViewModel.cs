using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Hangman.Repositories;
using Hangman.GameLogics;
using System.Windows.Controls;

namespace Hangman.ViewModels
{
    class UserSettingsViewModel : BaseViewModel
    {
        public string GamesPlayed { get; set; }

        public string GamesWon { get; set; }

        public double WinRate { get; set; }

        public string WinRateString { get; set; }

        public string Title { get; set; }
        public string PlayerStatus { get; set; }
        public BitmapImage MemeForWinRate { get; set; } 
        
        public string LabelColor { get; set; }

        public UserSettingsViewModel()
        {
            
            GetGamesPlayed();
            GetGamesWon();
            CalculateWinRate();
            SetPlayerStatus();
            SetWinRate();
            ChangeMemeWithWinRate();
        }

        private void ChangeMemeWithWinRate()
        {
            string imageAdress;
            imageAdress = $"../../../Assets/Images/{PlayerStatus}.jpg";

            string currentPath = Environment.CurrentDirectory;
            MemeForWinRate = new BitmapImage(new Uri(System.IO.Path.Combine(currentPath, imageAdress)));
        }

        public void GetGamesPlayed()
        {
            GamesPlayed = PlayerStatsRepository.GetGamesPlayed(PlayerEngine.ActivePlayer).ToString();
        }

        public void GetGamesWon()
        {
            GamesWon = PlayerStatsRepository.GetGamesWon(PlayerEngine.ActivePlayer).ToString();
        }

        public void CalculateWinRate()
        {

            double gamesPlayed = PlayerStatsRepository.GetGamesPlayed(PlayerEngine.ActivePlayer);
            double gamesWon = PlayerStatsRepository.GetGamesWon(PlayerEngine.ActivePlayer);

            if (gamesPlayed == 0)
            {
                WinRate = 0;
            }
            else
            {
                double dec = (gamesWon / gamesPlayed) * 100;
                WinRate = Math.Round(dec, 2);
            }
        }

        public void SetWinRate()
        {
            WinRateString = $"{WinRate} %";
        }
        public void SetPlayerStatus()
        {
            if (WinRate >= 50)
            {
                //NAMNGE MEMES
                PlayerStatus = "YouRock";
                LabelColor = "green";
            }

            else if (WinRate < 50 && WinRate >30)
            {
                PlayerStatus = "YouAverage";
                LabelColor = "yellow";
            }

            else if (WinRate <= 30)
            {
                PlayerStatus = "YouSuck";
                LabelColor = "red";
            }

            if (WinRate == 0)
            {
                PlayerStatus = "YouEmpty";
                LabelColor = "black";
            }
        }

    }


}

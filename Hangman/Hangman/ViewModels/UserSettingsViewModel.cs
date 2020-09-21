using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Hangman.Repositories;
using Hangman.Modules;
using System.Windows.Controls;
using System.Windows.Input;
using Hangman.Models;
using Npgsql;

namespace Hangman.ViewModels
{
    class UserSettingsViewModel : BaseViewModel
    {

        #region Properties: PlayerStatsUC
        public string GamesPlayed { get; set; }
        public string GamesWon { get; set; }
        public double WinRate { get; set; }
        public string WinRateString { get; set; }
        public string Title { get; set; }
        public string PlayerStatus { get; set; }
        public BitmapImage MemeForWinRate { get; set; }        
        public string LabelColor { get; set; }
        #endregion

        #region Properties: Delete User
        public bool IsDeletable { get; set; }
        public string DeleteMessage { get; set; }
        #endregion

        #region Properties: Update User
        public string UpdateMessage { get; set; }
        public ICommand UppdateUserCommand { get; set; }
        public IPlayer Player { get; set; }

        #endregion

        public UserSettingsViewModel()
        {
            UpdatePlayerStats();
        }

        #region Methods: Delete User
        public bool CheckIfDeletable(string name)
        {
            if (name == PlayerModule.GetActivePlayer().Name)
            {
                DeleteMessage = "Din användare raderas. Du loggas nu ut.";
                return true;
            }

            else
            {
                DeleteMessage = "Du har skrivit in fel användarnamn.";
                return false;
            }

        }

        public void DeleteUser()
        {
            PlayerRepository.DeletePlayer(PlayerModule.GetActivePlayer().Id);
        }

        #endregion
        #region Methods: PlayerStatsUC
        private void ChangeMemeWithWinRate()
        {
            string imageAdress;
            imageAdress = $"../../../Assets/Images/{PlayerStatus}.jpg";

            string currentPath = Environment.CurrentDirectory;
            MemeForWinRate = new BitmapImage(new Uri(System.IO.Path.Combine(currentPath, imageAdress)));
        }

        public void GetGamesPlayed()
        {
            GamesPlayed = PlayerStatsRepository.GetGamesPlayed(PlayerModule.GetActivePlayer()).ToString();
        }

        public void GetGamesWon()
        {
            GamesWon = PlayerStatsRepository.GetGamesWon(PlayerModule.GetActivePlayer()).ToString();
        }

        public void CalculateWinRate()
        {

            double gamesPlayed = PlayerStatsRepository.GetGamesPlayed(PlayerModule.GetActivePlayer());
            double gamesWon = PlayerStatsRepository.GetGamesWon(PlayerModule.GetActivePlayer());

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

        public void UpdatePlayerStats()
        {
            GetGamesPlayed();
            GetGamesWon();
            CalculateWinRate();
            SetPlayerStatus();
            SetWinRate();
            ChangeMemeWithWinRate();
        }

        #endregion

        #region Methods: Update User

        public void UpdateUser(IPlayer player, string wantedName)
        {

            if (wantedName != "" && wantedName != PlayerModule.GetActivePlayer().Name)
            {
                try
                {
                    PlayerRepository.UpdateNameOnPlayer(wantedName, PlayerModule.GetActivePlayer().Id);
                    var module = new PlayerModule();
                    module.TryLogInPlayer(wantedName);
                    UpdateMessage = "Ditt användarnamn är nu bytt till " + wantedName;
                }

                catch (PostgresException ex)
                {
                    //ta fram koden om användaren inte existerar
                    if (ex.SqlState.Contains("23505"))
                    {
                        UpdateMessage = "Du har valt ett namn som är upptaget - försök igen";
                    }

                    else
                    {
                        UpdateMessage = "Något gick fel - försök igen";
                    }
                }
            }

            else if (wantedName == PlayerModule.GetActivePlayer().Name)
            {
                UpdateMessage = "Du måste ange ett nytt namn";
            }

            else if (wantedName == "")
                UpdateMessage = "Du måste ange ett namn";

            else
            {
                UpdateMessage = "Något gick fel";
            }
        }
        #endregion

    }


}

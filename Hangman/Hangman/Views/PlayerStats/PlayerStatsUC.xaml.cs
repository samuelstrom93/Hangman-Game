using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hangman.GameLogics;
using Hangman.Repositories;
using Hangman;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for PlayerStatsUC.xaml
    /// </summary>
    public partial class PlayerStatsUC : UserControl
    {
        public BitmapImage ImageForPlayersSkills { get; set; }
        private double winRate;

        public PlayerStatsUC()
        {
            InitializeComponent();
            txtBoxTotalGamesPlayed.Text = PlayerStatsRepository.GetGamesPlayed(PlayerEngine.ActivePlayer).ToString();
            txtBlockGamesWon.Text = PlayerStatsRepository.GetGamesWon(PlayerEngine.ActivePlayer).ToString();
            winRate = CalculateWinRate();
            AreYouAGoodPlayer(winRate);

            lblWinRate.Content = winRate + "%";
        }


        //Lägga det här i VM?
        public double CalculateWinRate()
        {
            double winRate;

            double gamesPlayed = PlayerStatsRepository.GetGamesPlayed(PlayerEngine.ActivePlayer);
            double gamesWon= PlayerStatsRepository.GetGamesWon(PlayerEngine.ActivePlayer);

            if (gamesPlayed == 0)
            {
                winRate = 0;
            }
            else
            {
                double dec = (gamesWon/gamesPlayed)*100;
                winRate = Math.Round(dec, 2);
            }


            return winRate;
        }

        public void AreYouAGoodPlayer(double winRate)
        {
            if (winRate >= 50)
            {
                lblWinRate.Foreground = Brushes.Green;
                GridYay.Visibility = Visibility.Visible;

            }

            else if (winRate < 50)
            {
                lblWinRate.Foreground = Brushes.Red;
                GridNay.Visibility = Visibility.Visible;
            }

            if (winRate == 0)
            {

            }
        }


    }
}

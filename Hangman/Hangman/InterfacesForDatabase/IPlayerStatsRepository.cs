using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Database
{
    interface IPlayerStatsRepository
    {
        double GetGamesPlayed(IPlayer player);
        double GetGamesWon(IPlayer player);
    }
}

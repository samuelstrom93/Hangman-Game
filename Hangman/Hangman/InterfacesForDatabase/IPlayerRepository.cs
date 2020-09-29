using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Database
{
    public interface IPlayerRepository
    {
        IPlayer CreatePlayer(string name);
        List<Player> GetPlayers();
        Player GetPlayer(string name);
        Player GetPlayerFromID(int id);
        void DeletePlayer(int id);
        void UpdateNameOnPlayer(string name, int id);


    }
}

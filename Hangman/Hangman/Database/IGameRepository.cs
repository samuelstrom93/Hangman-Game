using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Moduls.InterfacesForDatabase
{
    interface IGameRepository
    {
        int AddGame(Game game);
        Game GetGameFromID(int gameID);
        Game GetGameFromPlayerID(int playerID);
        Game GetGameFromWordID(int wordID);
        void DeleteGame(int gameID);
    }
}

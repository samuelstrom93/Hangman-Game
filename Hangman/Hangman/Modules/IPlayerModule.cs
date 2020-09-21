using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Modules
{
    public interface IPlayerModule
    {
        void LogOutPlayer();
        bool TryLogInPlayer(string name);

    }
}

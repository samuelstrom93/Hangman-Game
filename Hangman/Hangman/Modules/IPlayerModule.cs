using Hangman.Models;

namespace Hangman.Modules
{
    public interface IPlayerModule
    {
        void LogOutPlayer();
        bool TryLogInPlayer(string name);
        bool TryAddPlayer(string name, out IPlayer added);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Moduls
{
    public class GameEngine
    {
        public bool IsGuessCorrect { get; set; }
        public bool IsGameStart { get; set; }
        public void JudgeGame()
        {
            CompareWordAndSelectedKey();
            WorkCounters();
            ConvertShownWord();
            LinkAnswerForPlayer();
            SwitchGameStatus();
        }
    }
}

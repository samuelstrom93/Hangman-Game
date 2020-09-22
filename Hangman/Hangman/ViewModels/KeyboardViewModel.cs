using Hangman.Modules;
using Hangman.Moduls;
using Hangman.ViewModels.Base;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hangman.ViewModels
{
    public class KeyboardViewModel : BaseViewModel
    {
        public GameEngine GameEngine { get; set; }
        public GameEndPage GameEndPage { get; set; }
        public ICommand GuessDirectlyCommand { get; set; }

        public KeyboardViewModel()
        {
            GameEngine = new GameEngine();
            GuessDirectlyCommand = new RelayCommand(GuessDirectly);

            GuessDirectlyText = "";
        }

        public string GuessDirectlyText { get; set; }   //Binding i GamePage.xml

        private string playersGuessingAnswer;
        private void GuessDirectly()
        {
            if (GameEngine.IsGameStart)
            {
                playersGuessingAnswer = GuessDirectlyText.ToUpper();
                GameEngine.GuessDirectly(playersGuessingAnswer);
                GameEngine.SwitchGameStatus();
            }

            if(GameEngine.IsGameEnd)
            {
                GameEndPage = new GameEndPage(GameEngine.GetGame(), GameEngine.GetWord());
            }

        }

    }
}

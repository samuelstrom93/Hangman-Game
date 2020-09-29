using Hangman.Modules;
using Hangman.ViewModels.Base;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.Moduls
{
    public class HintUCViewModel : BaseViewModel
    {
        public GameEngine GameEngine { get; set; }
        public GameEndPage GameEndPage { get; set; }    // Binding i GamePage.xml
        public string Hint { get; set; }
        public ICommand ShowHintCommand { get; set; }
        public bool IsHintShown { get; set; }

        public HintUCViewModel(string hint)
        {
            GameEngine = new GameEngine();
            Hint = hint;
            IsHintShown = false;
            ShowHintCommand = new RelayCommand(ShowHint);
        }

        public void ShowHint()
        {
            if (IsHintShown == true)
            {
                IsHintShown = false;
            }
            else
            {
                IsHintShown = true;
                GameEngine.ProceedGameStage();
                GameEngine.SwitchGameStatus();
            }

            if (GameEngine.IsGameEnd)
            {
                GameEndPage = new GameEndPage(GameEngine.GetGame(), GameEngine.GetWord(), GameEngine.StopWatchEngine.Timer);
            }
        }

    }
}

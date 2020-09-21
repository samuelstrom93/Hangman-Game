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



       // public string SelectedKey { get; set; }
       // public ICommand LetterClickCommand { get; set; } //Binding i KeyboardUC.xml

        //public Button Button { get; set; }  //Binding i KeyboardUC.xml

        public KeyboardViewModel()
        {
            //Button = new Button();
            GameEngine = new GameEngine();
            GuessDirectlyCommand = new RelayCommand(GuessDirectly);

            GuessDirectlyText = "";
            //LetterClickCommand = new RelayCommand(Clickletter);
        }

        public string GuessDirectlyText { get; set; }
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

        /* public void SetSelectedKey(string selectedKey)
         {
             SelectedKey = selectedKey;
         }*/

        /*  private void Clickletter()
          {
              if (GameEngine.IsGameStart)
              {
                  GameEngine.JudgeGame(SelectedKey);
                  ChangeBtnStyle();
              }

          }*/

        /* private void ChangeBtnStyle()
         {
             if (GameEngine.IsGuessCorrect)
             {
                 Button.Opacity = 0.3;
                 Button.Foreground = Brushes.Green;
                 Button.FontWeight = FontWeights.Bold;
             }
             else
             {
                 Button.Opacity = 0.3;
                 Button.Foreground = Brushes.Red;
                 Button.FontWeight = FontWeights.Bold;

             }
             Button.IsEnabled = false;

         }*/


    }
}

using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.PlayerRepository;
using static Hangman.Repositories.WordRepository;
using static Hangman.Repositories.GameRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using Hangman.Views;
using Hangman.GameLogics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.IO;
using System.Drawing;
using System.Reflection;
using Hangman.Moduls;
using Hangman.Views.UCsForGamePage;

namespace Hangman.ViewModels
{
    class GamePageViewModel : BaseViewModel
    {
        public GameEndPage GameEndOverray { get; set; } //Binding i GamePage.xml
        public ICommand GameStartCommand { get; set; }  //Binding i GamePage.xml
        public HintUC HintUC { get; set; }  //Binding i GamePage.xml

        public GameEngine GameEngine { get; set; }

        public string PlayerName { get; set; }  // = PlayerEngine.ActivePlayer.Name

        public GamePageViewModel()  // UTAN inloggning
        {
            PlayerName = "Spela utan användare";

            SetCommands();

            MakeStopWatchUC();
            MakeKeyboardUC();
            MakeGameEngine();
            GameEngine.SetPlayerWithoutLoggIn();
            HintUC = new HintUC();
        }

        public GamePageViewModel(IPlayer player)    // MED inloggning
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;

            SetCommands();

            MakeStopWatchUC();
            MakeKeyboardUC();
            MakeGameEngine();
            GameEngine.SetPlayer(player);
            HintUC = new HintUC();
        }

        private void MakeKeyboardUC()
        {
            KeyboardUC = new KeyboardUC();
            KeyboardViewModel = (KeyboardViewModel)KeyboardUC.DataContext;
        }

        private void MakeGameEngine()
        {
            GameEngine = KeyboardViewModel.GameEngine;
            
            GameEngine.RefreshGame();
            GameEngine.ShowGameStage();
            GameEngine.SetStopWatch(StopWatchEngine);
        }

        private void SetCommands()
        {
            GameStartCommand = new RelayCommand(StartGame);
        }

        private IWord IWord { get; set; }
        private void StartGame()
        {
            GameEngine.StartGame();

            StopWatchEngine.StartStopWatch();
            IWord = GameEngine.IWord;
            HintUC.SetDataContext(IWord.Hint);

        }

        #region Keyboard

        public KeyboardUC KeyboardUC { get; set; }  //Binding i GamePage.xml
        public KeyboardViewModel KeyboardViewModel { get; set; }

        #endregion

        #region StopWatch
        public StopWatchUC StopWatchUC { get; set; } //Binding i GamePage.xml
        public StopWatchUCViewModel StopWatchEngine { get; set; }

        private void MakeStopWatchUC()
        {
            StopWatchEngine = new StopWatchUCViewModel();
            StopWatchUC = new StopWatchUC(StopWatchEngine);
        }

        #endregion
    }
}

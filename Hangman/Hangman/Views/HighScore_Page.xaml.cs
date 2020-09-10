using Hangman.Repositories;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman.Views
{
    /// <summary>
    /// HighScore_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class HighScore_Page : Page
    {
        public HighScore_Page()
        {
            InitializeComponent();

            var vm = new HighscoresViewModel
            {
                TopHighscores = HighscoreRepository.GetTopGames().ToList(),
                TopCurrentPlayerHighscores = HighscoreRepository.GetTopGames(1).ToList(),
                TopDiligentPlayers = (Dictionary<string, long>)HighscoreRepository.GetTopDiligentPlayers()
            };

            DataContext = vm;
        }
    }
}

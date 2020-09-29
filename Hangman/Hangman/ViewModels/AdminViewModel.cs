using Hangman.Models;
using Hangman.Moduls.InterfacesForDatabase;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        public static readonly string WordPlaceholder = "ORD du vill lägga till...";
        public static readonly string DescriptionPlaceholder = "BESKRIVNING av ordet...";

        public string Word { get; set; }
        public string Description { get; set; }
        public ICommand TryAddWordCommand { get; set; }

        private IWordRepository wordRepository;

        public AdminViewModel()
        {
            Word = WordPlaceholder;
            Description = DescriptionPlaceholder;

            TryAddWordCommand = new RelayCommand(TryAddWord);

            wordRepository = new WordRepository();
        }

        private void TryAddWord()
        {
            if (string.IsNullOrWhiteSpace(Word) || Word.Equals(WordPlaceholder)
                || string.IsNullOrWhiteSpace(Description) || Description.Equals(DescriptionPlaceholder))
            {
                //TODO: Ge feedback i gränssnittet
                return;
            }

            if (!wordRepository.TryAddWord(Word, Description, out _))
            {
                //TODO: Ge feedback i gränssnitt
                return;
            }
        }
    }
}

using Hangman.ViewModels.Base;
using Npgsql.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    public class HeartDisplayViewModel : BaseViewModel
    {
        public string AnimationDuration { get; set; } = TimeSpan.FromHours(100).ToString(@"hh\:mm\:ss\.ff");
    }
}

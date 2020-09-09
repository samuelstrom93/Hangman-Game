using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Hangman.Repositories
{
    class Game_Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;
    }
}

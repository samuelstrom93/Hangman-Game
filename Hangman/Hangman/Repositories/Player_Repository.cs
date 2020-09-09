using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Npgsql;


namespace Hangman.Repositories
{
    class Player_Repository
    {
        //   private static string connectionMainString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

        #region CREATE

        public static void CreatePlayer(Player player)
        {
            string stmt = "INSERT INTO player (name) values(@name)";

         //        using (var conn = new NpsqlConnection())
        }

        #endregion
    }
}
using System.Data.SQLite; //import SQlite so that we can use it
using System.IO; //so we can check if the database file already exists
using Wisej.Web;

namespace WisejWebApplication1
{
    public class Database
    {
        public SQLiteConnection myConnection;

        public Database()//constructor
        {

            myConnection = new SQLiteConnection("Data Source=database.sqlite3"); //constructor accepts a string

            //only create a new database if one does not already exist
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3"); //creates the database
                AlertBox.Show("Database created");
            }

        }
    }
}

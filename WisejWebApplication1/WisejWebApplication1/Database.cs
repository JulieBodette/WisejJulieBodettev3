using System.Data.SQLite; //import SQlite so that we can use it
using System.IO; //so we can check if the database file already exists
using System.Security.Cryptography.X509Certificates;
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
                AlertBox.Show("New Sqlite Database created");
            }
        }
        public void OpenConnection()
        {
                //if the connection is not already open
            if (myConnection.State != System.Data.ConnectionState.Open)
             {
               //open a connection to the database
               myConnection.Open();
             }
        }

        public void CloseConnection()
        {
            //if connection is not closed
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                //close the connection
                myConnection.Close();
            }
        }

        


    }
}

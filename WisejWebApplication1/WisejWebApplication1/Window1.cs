
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SQLite;
using Wisej.Web;

namespace WisejWebApplication1
{
    public partial class Window1 : Form
    {
        public Window1()
        {
            InitializeComponent();
        }
        BindingList<Organization> OrganizationList = new BindingList<Organization>();

        //create a new sqlite database. We defined the database class in Database.cs, it just makes a SQLite Connection
        Database databaseObject = new Database();

        //NOTE: databaseObject.myConnection is just a SQLiteConnection we created in the constructor of the databaseObject
        //you could do this without creating a databaseObject- just create the SQLiteConnection myConnection
        //OpenConnection() and CloseCOnnection() are also functions we created in the databaseObject


        private void Window1_Load(object sender, EventArgs e)
        {
            //read data from the sqlite server into the OrganizationList BindingList
            //SELECT FROM DATABASE
            string query = "SELECT * FROM Organizations";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection); //send it the query and the SQLite connection
            databaseObject.OpenConnection(); //open the connection to the database
            SQLiteDataReader result = myCommand.ExecuteReader(); //execute the query. this returns a SQLiteDataReader
            if(result.HasRows)
            {
                while(result.Read()) //do this for each row
                {
                    int zipcode = Int32.Parse((result["Zip"]).ToString());
                    OrganizationList.Add(new Organization() { Name = (result["Name"]).ToString(), City = (result["City"]).ToString(), Street = (result["Street"]).ToString(), Country = (result["Country"]).ToString(), Zip = zipcode });
                }
            }
            //close the connection to the database
            databaseObject.CloseConnection();

            dataGridView1.DataSource = OrganizationList;//connect the data list to the grid on the UI

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            int zipcode = 0;
            try
            {
                //this converts the text in the Zip box to an integer
                zipcode = int.Parse(typedTextBox3.Text);
                OrganizationList.Add(new Organization() { Name = typedTextBox1.Text, Street = typedTextBox2.Text, Zip = zipcode, City = typedTextBox4.Text, Country = typedTextBox5.Text });
                AlertBox.Show("You added an organization to the database");

                //write the data to a sql database

                //SQL query to insert data into the database
                string query = "INSERT INTO Organizations ('Name', 'Street', 'Zip', 'City', 'Country') VALUES (@Name, @Street, @Zip, @City, @Country)";
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection); //send it the query and the SQLite connection

                //open the connection to the database
                databaseObject.OpenConnection();

                //Here we tell the query what values to send in
                myCommand.Parameters.AddWithValue("@Name", typedTextBox1.Text);
                myCommand.Parameters.AddWithValue("@Street", typedTextBox2.Text);
                myCommand.Parameters.AddWithValue("@Zip", zipcode);
                myCommand.Parameters.AddWithValue("@City", typedTextBox4.Text);
                myCommand.Parameters.AddWithValue("@Country", typedTextBox5.Text);

                //execute the query. NonQuery because we are writing to, not reading from, the database
                myCommand.ExecuteNonQuery(); //this returns an integer- the number of rows affected in the database
                                             //you could store it in a variable if you wanted to

                //close the connection to the database
                databaseObject.CloseConnection();



            }
            catch(System.FormatException) //show alert if zip code is invalid
            {
                AlertBox.Show("Please enter a number for the zip code");
            }
            catch
            {
                AlertBox.Show("An error ocurred and your organization was not added to the database");

            }
            
        }
    }
}

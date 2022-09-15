
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
        private void Window1_Load(object sender, EventArgs e)
        {
            OrganizationList.Add(new Organization() { Name = "examplecompany", City = "Cleveland", Street = "101 Example Ave", Country = "USA", Zip = 44142 });
            dataGridView1.DataSource = OrganizationList;//connect the data list to the grid on the UI

            //create a new sqlite database. We defined the database class in Database.cs, it just makes a SQLite Connection
            Database databaseObject = new Database();

            //NOTE: myConnection is just a SQLiteConnection we created in the constructor of the databaseObject
            //you could do this without creating a databaseObject- just create the SQLiteConnection myConnection
            //OpenConnection() and CloseCOnnection() are also functions we created in the databaseObject


            

            //SQL query to insert data into the database
            string query = "INSERT INTO Organizations ('Name', 'Street', 'Zip', 'City', 'Country') VALUES (@Name, @Street, @Zip, @City, @Country)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection); //send it the query and the SQLite connection

            //open the connection to the database
            databaseObject.OpenConnection();


            //Here we tell the query what values to send in
            myCommand.Parameters.AddWithValue("@Name", "Company Name2 Here");
            myCommand.Parameters.AddWithValue("@Street", "Street Name2 Here");
            myCommand.Parameters.AddWithValue("@Zip", 22);
            myCommand.Parameters.AddWithValue("@City", "City Name Here2");
            myCommand.Parameters.AddWithValue("@Country", "Country Name Here2");

            //execute the query
            myCommand.ExecuteNonQuery(); //this returns an integer- the number of rows affected in the database
            //you could store it in a variable if you wanted to


            //close the connection to the database
            databaseObject.CloseConnection();

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //this converts the text in the Zip box to an integer
                OrganizationList.Add(new Organization() { Name = typedTextBox1.Text, Street = typedTextBox2.Text, Zip = int.Parse(typedTextBox3.Text), City = typedTextBox4.Text, Country = typedTextBox5.Text });
                AlertBox.Show("You added an organization to the database");

                //add code to also write the data to a sql database
                //backup plan in case i cannot writethe OrganizationList directly
            }
            catch //show alert if zip code is invalid
            {
                AlertBox.Show("Please enter a number for the zip code");
            }
            
        }
    }
}

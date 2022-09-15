
using System;
using System.ComponentModel;
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
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                OrganizationList.Add(new Organization() { Name = typedTextBox1.Text, Street = typedTextBox2.Text, Zip = int.Parse(typedTextBox3.Text), City = typedTextBox4.Text, Country = typedTextBox5.Text });
                AlertBox.Show("You added something to the database");
            }
            catch
            {
                AlertBox.Show("Please enter a number for the zip code");
            }
            
        }
    }
}

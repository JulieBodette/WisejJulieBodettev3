
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
            OrganizationList.Add(new Organization() { Name = "examplecompany", City = "Cleveland", Street = "101 Example Ave", Country = "USA", Zip = 44142 });
            AlertBox.Show("you added something to the database");
        }
    }
}

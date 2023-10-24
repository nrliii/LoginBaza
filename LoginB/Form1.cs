using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LoginB
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection;
        public Form1()
        {
            InitializeComponent();
            string connectionString = "";
            connection = new OleDbConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (Validacija(username, password))
            {
                MessageBox.Show("Login uspesan!");
                Form2 form2 = new Form2(this);
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Pogresan username ili sifra.");
            }
        }
        private bool Validacija(string username, string password)
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM Login WHERE Username = @username AND Password = @password", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

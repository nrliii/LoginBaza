using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LoginB
{
    public partial class Form2 : Form
    {
        private Form1 parentForm;
        private OleDbConnection connection;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            string connectionString = "";
            connection = new OleDbConnection(connectionString);
            AlgoritamUsername();
            parentForm = form1;
        }

        private void AlgoritamUsername()
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT Username FROM Login", connection);
                OleDbDataReader citacU = cmd.ExecuteReader();

                while (citacU.Read())
                {
                    comboBox1.Items.Add(citacU["Username"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selektovanUser = comboBox1.SelectedItem.ToString();

            try
            {
                connection.Open();
                string query = "SELECT ID, Password FROM Login WHERE Username = @username";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", selektovanUser);

                OleDbDataReader citac = cmd.ExecuteReader();
                if (citac.Read())
                {
                    string password = citac["Password"].ToString();
                    string id = citac["ID"].ToString();
                    label2.Text = "Password: " + password;
                    label3.Text = "ID: " + id;
                }
                else
                {
                    label2.Text = "Sifra nije pronadjena";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string noviUsername = noviUserTextBox.Text;
            string noviPassword = noviPassTextBox.Text;

            try
            {
                connection.Open();
                string query = "INSERT INTO Login (Username, [Password]) VALUES (@username, @password)";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", noviUsername);
                cmd.Parameters.AddWithValue("@password", noviPassword);

                int brojRedovaPromenjen = cmd.ExecuteNonQuery();
                if (brojRedovaPromenjen > 0)
                {
                    MessageBox.Show("Novi login uspesno dodan!");
                }
                else
                {
                    MessageBox.Show("Nije uspelo dodavanje novog logina.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

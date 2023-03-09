using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CollegeManagmentSystem
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");

        private void populate()
        {
            Con.Open();
            string query = "Select * From UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (UIdTb.Text == "" || UNameTb.Text == "" || UpassTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                } 
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl values("+UIdTb.Text+",'"+UNameTb.Text+"','"+UpassTb.Text+"')",Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfullly Added: !!");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong: !!");
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UIdTb.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            UNameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UpassTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(UIdTb.Text=="")
                {
                    MessageBox.Show("Enter The User ID");
                }
                else
                {
                    Con.Open();
                    string query = "delete from UserTbl where Userid="+UIdTb.Text+";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps User Not Deleted: !!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (UIdTb.Text == "" || UNameTb.Text == "" || UpassTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.Open();
                    string query = "Update UserTbl Set UserName='" + UNameTb.Text + "',password='" + UpassTb.Text + "'where UserId=" + UIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps User is not Edited");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm Home = new MainForm();
            Home.Show();
            this.Hide();
        }
    }
}

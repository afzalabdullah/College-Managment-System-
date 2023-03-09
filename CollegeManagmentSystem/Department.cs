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
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");
        private void populate()
        {
            Con.Open();
            string query = "Select * From DepTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DepDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Department_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepName.Text == "" || DepDesc.Text == "" || DepDura.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DepTbl values('" + DepName.Text + "','" + DepDesc.Text + "'," + DepDura.Text + ")", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Successfullly Added: !!");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong: !!");
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepName.Text == "")
                {
                    MessageBox.Show("Enter The Department Name");
                }
                else
                {
                    Con.Open();
                    string query = "delete from DepTbl where DepName='" + DepName.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Deleted SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps User Not Deleted: !!");
            }
        }

        private void DepDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DepName.Text = DepDGV.SelectedRows[0].Cells[0].Value.ToString();
            DepDesc.Text = DepDGV.SelectedRows[0].Cells[1].Value.ToString();
            DepDura.Text = DepDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepName.Text == "" || DepDesc.Text == "" || DepDura.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.Open();
                    string query = "Update DepTbl Set DepDesc='" + DepDesc.Text + "',DepDuration=" + DepDura.Text + "where DepName='" +DepName.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Updated SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps.... Department is not Edited");
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

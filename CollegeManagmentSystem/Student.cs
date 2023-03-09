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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");

        private void populate()
        {
            Con.Open();
            string query = "Select * From StudentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void noduelist()
        {
            Con.Open();
            string query = "Select * From StudentTbl where StdFees >"+0+"";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void fillDepartment()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select DepName from DepTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName", typeof(string));
            dt.Load(rdr);
            SDepCB.ValueMember = "DepName";
            SDepCB.DataSource = dt;
            Con.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sid.Text == "" || SName.Text == "" || Sphone.Text == "" || SFee.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StudentTbl values(" + Sid.Text + ",'" + SName.Text + "','" + SGender.SelectedItem.ToString() + "','" + Sdate.Text + "','" + Sphone.Text + "','" + SDepCB.SelectedItem.ToString() + "'," + SFee.Text + ")", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("StudentSuccessfullly Added: !!");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong: !!");
            }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sid.Text == "" || SName.Text == "" || Sphone.Text == "" || SFee.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.Open();
                    string query = "Update StudentTbl Set StdName='" + SName.Text + "',StdGender='" + SGender.SelectedItem.ToString() + "',StdDOB='" + Sdate.Text + "',StdPhone='" + Sphone.Text + "',StdDepartment='" + SDepCB.SelectedValue.ToString() + "',StdFees='" + SFee.Text + "' where StdId=" + Sid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Data Updated SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps....Student Data is not Edited");
            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            Sid.Text = StudentDGV.SelectedRows[0].Cells[0].Value.ToString();
            SName.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            SGender.SelectedItem = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            Sdate.Text = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            Sphone.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
            SFee.Text = StudentDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sid.Text == "")
                {
                    MessageBox.Show("Enter The Student Id");
                }
                else
                {
                    Con.Open();
                    string query = "delete from StudentTbl where StdId='" + Sid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Data Deleted SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps Student didn't Deleted: !!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm Home = new MainForm();
            Home.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            noduelist();
        }
    }
}

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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");
        private void populate()
        {
            Con.Open();
            string query = "Select * From TeacherTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
           TeacherDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void fillDepartment()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select DepName from DepTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName",typeof(string));
            dt.Load(rdr);
            TDepCb.ValueMember = "DepName";
            TDepCb.DataSource = dt;
            Con.Close(); 
        }

        private void Teacher_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TID.Text == "" || TName.Text == "" || TPhone.Text == "" || TAddre.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TeacherTbl values(" + TID.Text + ",'" + TName.Text + "','" + GenderC.SelectedItem.ToString() + "','" + Date.Text + "','" + TPhone.Text + "','" + TDepCb.SelectedItem.ToString() + "','" + TAddre.Text + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Successfullly Added: !!");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong: !!");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TeacherDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TID.Text = TeacherDGV.SelectedRows[0].Cells[0].Value.ToString();
            TName.Text = TeacherDGV.SelectedRows[0].Cells[1].Value.ToString();
            GenderC.SelectedItem = TeacherDGV.SelectedRows[0].Cells[2].Value.ToString();
            Date.Text = TeacherDGV.SelectedRows[0].Cells[3].Value.ToString();
            TPhone.Text = TeacherDGV.SelectedRows[0].Cells[4].Value.ToString();
            TAddre.Text = TeacherDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TID.Text == "")
                {
                    MessageBox.Show("Enter The Teacher Id");
                }
                else
                {
                    Con.Open();
                    string query = "delete from TeacherTbl where TeacherId='" + TID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Data Deleted SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps Teacher didn't Deleted: !!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TID.Text == "" || TName.Text == "" || TPhone.Text == "" || TAddre.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.Open();
                    string query = "Update TeacherTbl Set TeacherName='" + TName.Text + "',TeacherGender='" + GenderC.SelectedItem.ToString() + "',TeacherDOB='" + Date.Text + "',TeacherPhone='" + TPhone.Text + "',TeacherDep='" + TDepCb.SelectedValue.ToString() + "',TeacherAdd='" + TAddre.Text + "' where TeacherId=" + TID.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Data Updated SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps....Teacher Data is not Edited");
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

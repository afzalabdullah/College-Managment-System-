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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");
        
        private void Dashboard_Load(object sender, EventArgs e)
        {
            Con.Open();
            string query = "Select count(*) From StudentTbl";
            SqlDataAdapter sda1 = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            StdLbl.Text = dt.Rows[0][0].ToString();


            string query1 = "Select count(*) From TeacherTbl";
            SqlDataAdapter sd2 = new SqlDataAdapter(query1, Con);
            DataTable dt1 = new DataTable();
            sd2.Fill(dt1);
            TeacLbl.Text = dt1.Rows[0][0].ToString();

            string query2 = "Select count(*) From DepTbl";
            SqlDataAdapter sd3 = new SqlDataAdapter(query2, Con);
            DataTable dt2 = new DataTable();
            sd3.Fill(dt2);

            DeparLbl.Text = dt2.Rows[0][0].ToString();
            Con.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm Home = new MainForm();
            Home.Show();
            this.Hide();
        }
    }
}

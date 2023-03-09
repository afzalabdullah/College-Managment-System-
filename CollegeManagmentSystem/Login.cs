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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");
        private void button1_Click(object sender, EventArgs e)
        {
            
            MainForm Home = new MainForm();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count (*) from UserTbl where UserName='"+UserName.Text+"'and password="+Pass.Text+"",Con);
            DataTable dt = new DataTable();
            try { 
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Home.Show();
                    this.Hide();
                    Con.Close();
                }
                else
                {
                    MessageBox.Show("You Did not Enter Correct User Name or Password");
                }
            }
            catch { MessageBox.Show("Enter User Name or Password"); }
            
            
            Con.Close();


        }

        private void label5_Click(object sender, EventArgs e)
        {
            UserName.Clear(); ;
            Pass.Clear();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

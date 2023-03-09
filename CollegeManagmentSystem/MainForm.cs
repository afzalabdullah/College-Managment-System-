using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollegeManagmentSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Student std = new Student();
            std.Show();
            this.Hide();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Dashboard Das = new Dashboard();
            Das.Show();
            this.Hide();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Fees fees = new Fees();
            fees.Show();
            this.Hide();
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            Department Dep = new Department();
            Dep.Show();
            this.Hide();
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            UserForm UF = new UserForm();
            UF.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login ln = new Login();
            ln.Show();
            this.Hide();
        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {
            Teacher Te = new Teacher();
            Te.Show();
            this.Hide();
        }
    }
}

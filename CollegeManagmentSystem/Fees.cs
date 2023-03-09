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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\Documents\Collegedob.mdf;Integrated Security = True; Connect Timeout = 30");
        private void fillID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StdID from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(int));
            dt.Load(rdr);
            FidCB.ValueMember = "StdId";
            FidCB.DataSource = dt;
            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "Select * From FeesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void UpdateStd()
        {
            Con.Open();
           string query = "Update StudentTbl Set StdFees='" + FAmount.Text + "'where StdId=" + FidCB.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
           //MessageBox.Show("User Updated SuccessFully");
            Con.Close();
        }
        private void Fees_Load(object sender, EventArgs e)
        {
            fillID();
            populate();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (FNum.Text == "" || FName.Text == "" || FAmount.Text == "" )
                {
                    MessageBox.Show("Missing Information");
                }

                else
                {
                    Con.Open();
                    string date = FDate.Value.Year.ToString();
                    SqlCommand cmd = new SqlCommand("insert into FeesTbl values(" + FNum.Text + ",'" + FidCB.SelectedValue.ToString() + "','" +FName.Text + "','" + date + "'," + FAmount.Text + ")", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("FeesSuccessfullly Added: !!");
                    Con.Close();
                    populate();
                    UpdateStd();

                }
            }
            catch 
            {
                MessageBox.Show("Something Went Wrong....!!");
            }
            
        }

        private void FidCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "Select * From StudentTbl where StdId="+ FidCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd); 
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                FName.Text = dr["StdName"].ToString();
            }
            Con.Close();
        }

        private void FeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FNum.Text = FeeDGV.SelectedRows[0].Cells[0].Value.ToString();
            FidCB.SelectedItem = FeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            FName.Text = FeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            FAmount.Text = FeeDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm Home = new MainForm();
            Home.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (FNum.Text == "")
                {
                    MessageBox.Show("Enter The Department Name");
                }
                else
                {
                    Con.Open();
                    string query = "delete from FeesTbl where FeeNum='" + FNum.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fee Record Deleted SuccessFully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Opps Record does not Deleted: !!");
            }
        }
    }
}

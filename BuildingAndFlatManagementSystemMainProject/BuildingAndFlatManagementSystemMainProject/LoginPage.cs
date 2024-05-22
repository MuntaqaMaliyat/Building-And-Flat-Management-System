using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BuildingAndFlatManagementSystemMainProject
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\h\OneDrive\Documents\FlatDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static string UserName = "";
        private void Login_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTable where UserName= '"+UNametb.Text+"' and UserPass= '"+UPassTb.Text+"' ",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString()=="1")
            {
                UserName = UNametb.Text;
                Billing obj = new Billing();
                obj.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }
            Con.Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void errorProvider1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(UNametb.Text))
            {
                e.Cancel = true;
                UNametb.Focus();
                errorProvider1.SetError(UNametb, "Please Enter your User Name !");
            }
            else
            {
                //e.Cancel = true;
                errorProvider1.SetError(UNametb, null);
            }
        }

        private void guna2TextBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(UPassTb.Text))
            {
                e.Cancel = true;
                UPassTb.Focus();
                errorProvider2.SetError(UPassTb, "Please Enter your Password !");
            }
            else
            {
                //e.Cancel = true;
                errorProvider2.SetError(UPassTb, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void AdminLogin_Click(object sender, EventArgs e)
        {
            AdminLogin button = new AdminLogin();
            this.Hide();
            button.Show();
        }
    }
}

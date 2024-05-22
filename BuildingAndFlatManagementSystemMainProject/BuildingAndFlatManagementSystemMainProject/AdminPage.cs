using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BuildingAndFlatManagementSystemMainProject
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
            gridd();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\h\OneDrive\Documents\FlatDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Users_Load(object sender, EventArgs e)
        {

        }
        private void gridd()
        {
            Con.Open();
            string query = "select * from BTable";
            SqlDataAdapter sda = new SqlDataAdapter(query,Con);  
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); 
            sda.Fill(ds);
            DGVBT.DataSource = ds.Tables[0];
            Con.Close();
        }
        

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(BNameTb.Text == "" || FSizeTB.Text == "" || QtyTb.Text == ""  || BPriceTb.Text == "" || BAddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BTable values('" + BNameTb.Text + "', '" + FSizeTB.Text + "','" + QtyTb.Text + "', '" + BPriceTb.Text + "','" + BAddressTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved Successfully");
                    Con.Close();
                    gridd();
                    Reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DGVBT_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void CatCbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            gridd();
        }
        private void Reset()
        {
            BNameTb.Text = "";
            FSizeTB.Text = "";
            QtyTb.Text = "";
            BPriceTb.Text = "";
            BAddressTb.Text = "";

        }
        int key = 0;
        private void DGVBT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BNameTb.Text = DGVBT.SelectedRows[0].Cells[1].Value.ToString();
            FSizeTB.Text = DGVBT.SelectedRows[0].Cells[2].Value.ToString();
            QtyTb.Text = DGVBT.SelectedRows[0].Cells[3].Value.ToString();
            BPriceTb.Text = DGVBT.SelectedRows[0].Cells[4].Value.ToString();
            BAddressTb.Text = DGVBT.SelectedRows[0].Cells[5].Value.ToString();
            if(BNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(DGVBT.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void DelateBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from BTable where BuildingID=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delated Successfully");
                    Con.Close();
                    gridd();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || FSizeTB.Text == "" || QtyTb.Text == "" || BPriceTb.Text == "" || BAddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update BTable set BName= '" + BNameTb.Text + "',FSize='" + FSizeTB.Text + "',BQty='" + QtyTb.Text + "',BPrice='" + BPriceTb.Text + "',BAdd='" + BAddressTb.Text + "'where BuildingId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                    Con.Close();
                    gridd();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            this.Hide();
            loginPage.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            this.Hide();
            users.Show();
        }

        private void CatCbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CatCbSearch_SelectionChangeCommitted_1(object sender, EventArgs e)
        {

        }

        private void BCatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }
}

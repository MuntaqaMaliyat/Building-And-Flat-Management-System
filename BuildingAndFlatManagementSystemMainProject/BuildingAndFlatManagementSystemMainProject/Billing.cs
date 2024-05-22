using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingAndFlatManagementSystemMainProject
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            gridd();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\h\OneDrive\Documents\FlatDb.mdf;Integrated Security=True;Connect Timeout=30");
    
        private void gridd()
        {
            Con.Open();
            string query = "select * from BTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVBT.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int key = 0, stock = 0;
        private void DGVBT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BNameTb.Text = DGVBT.SelectedRows[0].Cells[1].Value.ToString();
            FSizeTB.Text = DGVBT.SelectedRows[0].Cells[2].Value.ToString();
            //QtyTb.Text = DGVBT.SelectedRows[0].Cells[3].Value.ToString();
            BPriceTb.Text = DGVBT.SelectedRows[0].Cells[4].Value.ToString();
            if (BNameTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(DGVBT.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(DGVBT.SelectedRows[0].Cells[3].Value.ToString());
            }

        }
        private void Reset()
        {
            BNameTb.Text = "";
            FSizeTB.Text = "";
            BPriceTb.Text = "";
            QtyTb.Text = "";
            ClientNameTb.Text = "";

        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            try
            {
                Con.Open();
                string query = "update BTable set BQty='" + newQty + "'where BuildingId =" + key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Updated Successfully");
                Con.Close();
                gridd();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int n = 0, Grdtotal = 0;

        private void PrintBtn_Click(object sender, EventArgs e)
        {
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Hide();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void BPriceTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLb.Text = LoginPage.UserName;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ClientNameTb.Text == "" || BNameTb.Text == "")
            {
                MessageBox.Show("Select Client Name");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTable values('" + UserNameLb.Text + "', '" + ClientNameTb.Text + "','" + Grdtotal + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Paid Successfully");
                    Con.Close();
                    //gridd();
                    //Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(QtyTb.Text == "" ||  Convert.ToInt32(QtyTb.Text)>stock)
            {
                MessageBox.Show("Not Available");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(BPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BNameTb.Text;
                newRow.Cells[2].Value = FSizeTB.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = BPriceTb.Text;
                newRow.Cells[5].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;
                UpdateBook();
                Grdtotal = Grdtotal + total;
                TotalLb.Text = Grdtotal + " Tk.";
            }
        }
    }
}

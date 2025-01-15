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

namespace laundryifa
{
    public partial class datadetailorder : Form
    {
        public datadetailorder()
        {
            InitializeComponent();


            SqlCommand cmd = new SqlCommand("select kodelayanan, namalayanan from [layanan]", conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "namalayanan";
            comboBox1.ValueMember = "kodelayanan";
            comboBox1.SelectedIndex = -1;
            tampildata();

        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [detailorder]", conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.Name = "Cetak Nota";
            link.HeaderText = "Cetak Nota";
            link.Text = "Cetak Nota";
            link.UseColumnTextForLinkValue = true;

            dataGridView1.DataSource = dt;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentRow.Cells;
            textBox2.Text = row["kodeorder"].Value.ToString();
            textBox4.Text = row["biaya"].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(row["biaya"].Value.ToString());
            comboBox1.SelectedValue = row["kodelayanan"].Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {

                SqlCommand cmd = new SqlCommand("SELECT biaya FROM Layanan WHERE kodelayanan = @kodelayanan", conn);
                cmd.CommandType = CommandType.Text;

                try
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@kodelayanan", comboBox1.SelectedValue);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox4.Text = dr["biaya"].ToString();
                    }
                    dr.Close();


                }
                catch
                {
                    conn.Close();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            textBox2.Text = "";
            textBox4.Text = "";
            numericUpDown1.Value = 0;
            comboBox1.SelectedValue = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

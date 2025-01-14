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

namespace laundryifa
{
    public partial class Detailorder : Form
    {
        
        public Detailorder(string kodeorder)
        {
            InitializeComponent();
            tampildata();

            this.kodeorder = kodeorder;

            SqlCommand cmd = new SqlCommand("select kodelayanan, namalayanan from [layanan] ", conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;  
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "kodelayanan";
            comboBox1.DisplayMember = "namalayanan";
            comboBox1.SelectedIndex = -1;
            conn.Close();
        }

        SqlConnection conn = Properti.conn;
        private string kodeorder;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [Detailorder] ", conn);  
            conn.Open();
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentRow.Cells;
            comboBox1.Text = row["namalayanan"].Value.ToString();
            textBox1.Text = row["biaya"].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            order o = new order();
            o.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                SqlCommand cmd = new SqlCommand("select biaya from [layanan] where kodelayanan=@kodelayanan", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@kodelayanan", comboBox1.SelectedValue);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["biaya"].ToString();
                }
                conn.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Detailorder VALUES(@kodeorder,@kodelayanan,@jumlahunit,@biaya)", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@kodeorder", kodeorder);
            cmd.Parameters.AddWithValue("@kodelayanan", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@jumlahunit", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@biaya", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            tampildata();
            MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
        }

        private void clear()
        {
            comboBox1.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            int kodeorder = Convert.ToInt32(row.Cells["kodeorder"].Value.ToString());
            SqlCommand cmd = new SqlCommand("UPDATE Detailorder SET kodeorder = @kodeorder, kodelayanan = @kodelayanan, jumlahunit = @jumlahunit , biaya = @biaya WHERE kodeorder= @kodeorder", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@kodeorder", kodeorder);
            cmd.Parameters.AddWithValue("@kodelayanan", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@jumlahunit", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@biaya", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            tampildata();
            MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
        }
    }
}

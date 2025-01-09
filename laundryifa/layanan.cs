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
    public partial class layanan : Form
    {
        public layanan()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [layanan]",conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["biaya"].DefaultCellStyle.Format = "C";
            dataGridView1.Columns["biaya"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("id-ID");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin diinput tidak boleh kosong!");

                } else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin ditambahkan sudah betul?", "Question",MessageBoxButtons.YesNo);
                    if (mess == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into [layanan] values (@namalayanan, @biaya)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@namalayanan", textBox1.Text);
                        cmd.Parameters.AddWithValue("@biaya", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil ditambahkan!");
                        tampildata();
                        clear();
                    }
                }
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["namalayanan"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["biaya"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin diubah tidak boleh kosong!");

                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin diubah sudah betul?", "Question", MessageBoxButtons.YesNo);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodelayanan = Convert.ToInt32(row.Cells["kodelayanan"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("update [layanan] set namalayanan=@namalayanan, biaya=@biaya where kodelayanan=@kodelayanan", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kodelayanan", kodelayanan);
                        cmd.Parameters.AddWithValue("@namalayanan", textBox1.Text);
                        cmd.Parameters.AddWithValue("@biaya", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil diubah!");
                        tampildata();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin dihapus tidak boleh kosong!");

                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin dihapus sudah betul?", "Question", MessageBoxButtons.YesNo);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodelayanan = Convert.ToInt32(row.Cells["kodelayanan"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("delete from [layanan] where kodelayanan=@kodelayanan", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kodelayanan", kodelayanan);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil dihapus!");
                        tampildata();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}

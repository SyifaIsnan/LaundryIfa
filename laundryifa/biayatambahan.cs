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
    public partial class biayatambahan : Form
    {
        public biayatambahan()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [biayatambahan]", conn);
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

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Text = dataGridView1.CurrentRow.Cells["keterangan"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["biaya"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin diinput tidak boleh kosong!");

                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin ditambahkan sudah betul?", "Question", MessageBoxButtons.YesNo);
                    if (mess == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into [biayatambahan] values (@keterangan, @biaya)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@biaya", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil ditambahkan!");
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

        private void clear()
        {
            richTextBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin diinput tidak boleh kosong!");

                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin diubah sudah betul?", "Question", MessageBoxButtons.YesNo);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodebiaya = Convert.ToInt32(row.Cells["kodebiaya"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("update [biayatambahan] set keterangan=@keterangan, biaya=@biaya where kodebiaya=@kodebiaya", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kodebiaya", kodebiaya);
                        cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
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
                    MessageBox.Show("Data yang ingin diinput tidak boleh kosong!");

                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin dihapus sudah betul?", "Question", MessageBoxButtons.YesNo);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodebiaya = Convert.ToInt32(row.Cells["kodebiaya"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("delete from [biayatambahan] where kodebiaya=@kodebiaya", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kodebiaya", kodebiaya);
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
    }
}

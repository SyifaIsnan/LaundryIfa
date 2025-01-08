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
    public partial class petugas : Form
    {
        public petugas()
        {
            InitializeComponent();
            tampildata();
        }

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [petugasantar]", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@namapetugas", textBox1.Text);
            cmd.Parameters.AddWithValue("@nomortelepon", textBox2.Text);
            DataTable dt = new DataTable();
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            dataGridView1.DataSource = dt;
            
        }

        SqlConnection conn = Properti.conn;


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["namapetugas"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["nomortelepon"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin ditambahkan tidak boleh kosong!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    var mess = MessageBox.Show("Apakah data yang anda ingin tambahkan sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into [petugasantar] values (@namapetugas,@nomortelepon)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@namapetugas", textBox1.Text);
                        cmd.Parameters.AddWithValue("@nomortelepon", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil ditambahkan!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin ditambahkan tidak boleh kosong!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang anda ingin ubah sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodepetugas = Convert.ToInt32(row.Cells["kodepetugas"].Value);
                        SqlCommand cmd = new SqlCommand("update [petugasantar] set namapetugas = @namapetugas, nomortelepon = @nomortelepon where kodepetugas= @kodepetugas", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kodepetugas", kodepetugas);
                        cmd.Parameters.AddWithValue("@namapetugas", textBox1.Text);
                        cmd.Parameters.AddWithValue("@nomortelepon", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil diubah!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin dihapus tidak boleh kosong!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang anda ingin hapus sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodepetugas = Convert.ToInt32(row.Cells["kodepetugas"].Value);
                        SqlCommand cmd = new SqlCommand("delete from [petugasantar] where kodepetugas= @kodepetugas", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kodepetugas", kodepetugas);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil diubah!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

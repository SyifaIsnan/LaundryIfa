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
    public partial class user : Form
    {
        public user()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [user]", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data tidak boleh kosong!");
                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin ditambahkan sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes) 
                    {
                        SqlCommand cmd = new SqlCommand("insert into [user] values (@namauser, @email, @password)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@namauser", textBox1.Text);
                        cmd.Parameters.AddWithValue("@email", textBox2.Text);
                        cmd.Parameters.AddWithValue("@password", textBox3.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil ditambahkan!");
                        tampildata();
                        clear();

                    }
                } conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Data gagal ditambahkan!", ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = string.Empty;   
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["namauser"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["password"].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data tidak boleh kosong!");
                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin diubah sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodeuser = Convert.ToInt32(row.Cells["kodeuser"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("update [user] set namauser=@namauser, email=@email, password=@password where kodeuser=@kodeuser", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("kodeuser", kodeuser);
                        cmd.Parameters.AddWithValue("@namauser", textBox1.Text);
                        cmd.Parameters.AddWithValue("@email", textBox2.Text);
                        cmd.Parameters.AddWithValue("@password", textBox3.Text);
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
                MessageBox.Show("Data gagal diubah!", ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data tidak boleh kosong!");
                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin dihapus sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int kodeuser = Convert.ToInt32(row.Cells["kodeuser"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("delete from [user] where kodeuser=@kodeuser", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("kodeuser", kodeuser);
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
                MessageBox.Show("Data gagal dihapus!", ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}

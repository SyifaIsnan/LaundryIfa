﻿using System;
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
    public partial class pelanggan : Form
    {
        public pelanggan()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("select * from [pelanggan]", conn);  
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
                    MessageBox.Show("Data yang ingin ditambahkan tidak boleh kosong!", "Warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                } else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin anda tambahkan sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into [pelanggan] values (@nomortelepon, @nama, @alamat)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@nomortelepon", textBox1.Text);
                        cmd.Parameters.AddWithValue("@nama", textBox2.Text);
                        cmd.Parameters.AddWithValue("@alamat", richTextBox1.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil ditambahkan!", "Information");
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
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["nomortelepon"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["nama"].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells["alamat"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Data yang ingin ditambahkan tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var mess = MessageBox.Show("Apakah data yang ingin anda ubah sudah betul?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("update [pelanggan] set nama = @nama, alamat = @alamat where nomortelepon = @nomortelepon", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@nomortelepon", textBox1.Text);
                        cmd.Parameters.AddWithValue("@nama", textBox2.Text);
                        cmd.Parameters.AddWithValue("@alamat", richTextBox1.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil diubah!", "Information");
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
                    MessageBox.Show("Data yang ingin ditambahkan tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var mess = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mess == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("delete from [pelanggan] where nomortelepon = @nomortelepon", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@nomortelepon", textBox1.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Data berhasil dihapus!", "Information");
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

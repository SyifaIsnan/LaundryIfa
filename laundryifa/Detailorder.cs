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
        
        private string kodeorder;

        public Detailorder(string kodeorder)
        {
            InitializeComponent();
            using (var conn = Properti.getconn())
            {
                this.kodeorder = kodeorder;
                tampildata();

                SqlCommand cmd = new SqlCommand("SELECT kodelayanan , namalayanan FROM Layanan", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "namalayanan";
                comboBox1.ValueMember = "kodelayanan";
                comboBox1.SelectedIndex = -1;
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

                conn.Close();
            }
            
        }

        

        private void tampildata()
        {
            using (var conn = Properti.getconn())
            {
                SqlCommand cmd = new SqlCommand("SELECT detailorder.kodeorder, layanan.namalayanan, detailorder.jumlahunit, detailorder.biaya FROM detailorder JOIN layanan ON detailorder.kodelayanan = layanan.kodelayanan", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                
                dataGridView1.DataSource = dt;
               

                dataGridView1.Columns["kodeorder"].HeaderText = "Kode Order";
                dataGridView1.Columns["namalayanan"].HeaderText = "Nama Layanan";
                dataGridView1.Columns["jumlahunit"].HeaderText = "Jumlah Unit";
                dataGridView1.Columns["biaya"].HeaderText = "Biaya";

                conn.Close();
            }
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentRow.Cells;
            comboBox1.Text = row["namalayanan"].Value.ToString();
            numericUpDown1.Value  = Convert.ToInt32(row["jumlahunit"].Value.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            order o = new order();
            o.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            

                using (var conn = Properti.getconn())
                {
                    if (comboBox1.SelectedIndex != -1)
                    {

                    try
                    {
                        if (comboBox1.SelectedValue != null)
                        {
                            int kodelayanan;
                            if (Int32.TryParse(comboBox1.SelectedValue.ToString(), out kodelayanan))
                            {
                                SqlCommand cmd = new SqlCommand("SELECT biaya FROM Layanan WHERE kodelayanan = @kodelayanan", conn);
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@kodelayanan", kodelayanan);

                                conn.Open();
                                SqlDataReader dr = cmd.ExecuteReader();
                                if (dr.Read())
                                {
                                    textBox1.Text = dr["biaya"].ToString();
                                }
                                conn.Close();
                            }

                            
                        }
                        else
                        {
                            MessageBox.Show("Tidak ada nilai yang dipilih.");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        conn.Close();
                    }


                }
            }

               

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var conn = Properti.getconn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Detailorder VALUES(@kodeorder,@kodelayanan,@jumlahunit,@biaya)", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@kodeorder", kodeorder);
                    cmd.Parameters.AddWithValue("@kodelayanan", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@jumlahunit", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@biaya", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampildata();
                    clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();


                }
            }
               
        }

        private void clear()
        {
            comboBox1.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (var conn = Properti.getconn())
            {
                try
                {
                    var row = dataGridView1.CurrentRow;
                    int kodeorder = Convert.ToInt32(row.Cells["kodeorder"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("UPDATE Detailorder SET kodeorder=@kodeorder, kodelayanan = @kodelayanan, jumlahunit = @jumlahunit , biaya = @biaya WHERE kodeorder= @kodeorder", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@kodeorder", kodeorder);
                    cmd.Parameters.AddWithValue("@kodelayanan", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@jumlahunit", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@biaya", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    
                    tampildata();
                    MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch
                {
                    conn.Close();
                }
            }

               
        }

        private void Detailorder_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

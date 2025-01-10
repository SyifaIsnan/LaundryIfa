using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace laundryifa
{
    public partial class order : Form
    {
        

        public order()
        {
            InitializeComponent();
            tampildata();
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            petugas();
        }

        private void petugas()
        {
            SqlCommand cmd = new SqlCommand("SELECT kodepetugas ,namapetugas FROM PetugasAntar", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "namapetugas";
            comboBox2.ValueMember = "kodepetugas";
            comboBox2.SelectedIndex = -1;
            conn.Close();
        }

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Order]", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
                combo.Name = "Status";
                combo.DataSource = new string[] { "PENDING", "DICUCI", "DIANTAR/DIJEMPUT" };
                combo.HeaderText = "Status";
                combo.DataPropertyName = "Status";

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                link.Name = "Pilih Layanan";
                link.Text = "Pilih Layanan";
                link.HeaderText = "Pilih Layanan";
                link.UseColumnTextForLinkValue = true;

                dataGridView1.Columns.Add("kodeorder", "kodeorder");
                dataGridView1.Columns.Add("nomortelepon", "nomortelepon");
                dataGridView1.Columns.Add("tanggalorder", "tanggalorder");
                dataGridView1.Columns.Add("tanggalselesai", "tanggalselesai");
                dataGridView1.Columns.Add("biayaantar", "biayaantar");
                dataGridView1.Columns.Add("biayajemput", "biayajemput");
                dataGridView1.Columns.Add("biayahari", "biayahari");
                dataGridView1.Columns.Add("petugasantar", "petugasantar");
                dataGridView1.Columns.Add(combo);
                dataGridView1.Columns.Add(link);

                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["kodeorder"],
                        row["nomortelepon"],
                        row["tanggalorder"],
                        row["tanggalselesai"],
                        row["biayaantar"],
                        row["biayajemput"],
                        row["biayahari"],
                        row["petugasantar"],
                        row["statusorder"]
                    );
                    dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                }
                 



            }

        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        SqlConnection conn = Properti.conn;

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime order = dateTimePicker1.Value;
            DateTime selesai = dateTimePicker2.Value;

            TimeSpan selisih = selesai - order;
            int lamahari = selisih.Days;
            textBox3.Text = lamahari.ToString();

            if (lamahari > 3)
            {
                SqlCommand cmd = new SqlCommand("SELECT biaya FROM BiayaTambahan WHERE kodebiaya = 1", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox4.Text = dr["biaya"].ToString();
                }
            }
            else
            {
                textBox4.Text = "0";
            }
            conn.Close();
            hitungtotal();






        }

        private void hitungtotal()
        {
            int jemput = Convert.ToInt32(textBox4.Text);
            int antar = Convert.ToInt32(textBox6.Text);
            int hari = Convert.ToInt32(textBox7.Text);

            int total = jemput + antar + hari;
            textBox5.Text = total.ToString("C", CultureInfo.GetCultureInfo("id-ID"));

        }
                 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SqlCommand cmd = new SqlCommand("select biaya from [biayatambahan] where kodebiaya = 3", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox6.Text = dr["biaya"].ToString() ;
                }
                
            } else
            {
                textBox6.Text = "0";
            }
            conn.Close();
            hitungtotal();
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                SqlCommand cmd = new SqlCommand("select biaya from [biayatambahan] where kodebiaya = 2", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox7.Text = dr["biaya"].ToString();
                }

            }
            else
            {
                textBox7.Text = "0";
            }
            conn.Close();
            hitungtotal();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

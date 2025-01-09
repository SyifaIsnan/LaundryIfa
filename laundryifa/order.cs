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

namespace laundryifa
{
    public partial class order : Form
    {
        public order()
        {
            InitializeComponent();
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
            int hari = Convert.ToInt32(textBox4.Text);
            int jemput = Convert.ToInt32(textBox6.Text);
            int antar = Convert.ToInt32(textBox7.Text);
            int total = hari + jemput + antar;

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
    }
}

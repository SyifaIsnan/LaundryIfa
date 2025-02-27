﻿using System;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace laundryifa
{
    public partial class report : Form
    {

        SqlConnection conn = Properti.conn;
        public report()
        {
            InitializeComponent();
            chart1.Series.Clear();
            string[] from = new string[12];
            string[] to = new string[12];

            for (int i = 0; i < from.Length; i++)
            {
                from[i] = new DateTime(DateTime.Now.Year, i + 1, 1).ToString("MMMM");
                to[i] = new DateTime(DateTime.Now.Year, i + 1, 1).ToString("MMMM");

            }

            for (int i = 0;i < to.Length; i++)
            {
                comboBox1.Items.Add(from[i]);
                comboBox2.Items.Add(to[i]);
            }
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string from = comboBox1.SelectedItem.ToString();
            string to = comboBox2.SelectedItem.ToString();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select MONTH([Order].tanggalorder) as 'Bulan', SUM(jumlahunit*biaya) as 'Income' from [order]  INNER JOIN [detailorder] ON [Order].kodeorder = [detailorder].kodeorder where tanggalorder >= @from and tanggalorder <= @to GROUP BY MONTH(tanggalorder)", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@from", new DateTime(DateTime.Now.Year, DateTime.ParseExact(from, "MMMM", CultureInfo.CurrentCulture).Month, 1));
            cmd.Parameters.AddWithValue("@to", new DateTime(DateTime.Now.Year, DateTime.ParseExact(to, "MMMM", CultureInfo.CurrentCulture).Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.ParseExact(to, "MMMM", CultureInfo.CurrentCulture).Month)));
            DataTable dt = new DataTable(); 
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dataGridView1.DataSource = dt.AsEnumerable().Select(d => new
            {
                Bulan = new DateTime(DateTime.Now.Year, d.Field<int>("Bulan"), 1).ToString("MMMM"),
                Income = d.Field<int>("Income").ToString("C", CultureInfo.GetCultureInfo("id-ID")),
            }
            ).ToList();

            chart1.Series.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string Bulan = new DateTime(DateTime.Now.Year, row.Field<int>("Bulan"), 1).ToString("MMMM");
                int Income = row.Field<int>("Income");
                
                if (chart1.Series.IndexOf("Pendapatan") == -1)
                {
                    chart1.Series.Add("Pendapatan");
                    chart1.Series["Pendapatan"].ChartType = SeriesChartType.Column;
                }

                chart1.Series["Pendapatan"].Points.AddXY(Bulan, Income);
            }
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laundryifa
{
    public partial class report : Form
    {
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

        }
    }
}

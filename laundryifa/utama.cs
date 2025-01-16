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
    public partial class utama : Form
    {
        public utama(string namauser)
        {
            InitializeComponent();
            if (namauser == "Admin")
            {
                lOGINToolStripMenuItem.Enabled = false;
                dATAToolStripMenuItem.Enabled = false;
                bIAYATAMBAHANToolStripMenuItem.Enabled = false;
            }
            else if (namauser == "kasir")
            {
                lOGINToolStripMenuItem.Enabled = false;
            }
        }

        private void lOGINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mess = MessageBox.Show("Apakah anda yakin ingin logout?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mess == DialogResult.Yes)
            {
                this.Hide();
                login login = new login();
                login.Show();
                
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mess = MessageBox.Show("Apakah anda yakin ingin keluar dari aplikasi?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mess == DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void oRDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            order order = new order();
            order.Show();
        }

        private void pETUGASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            petugas pt = new petugas();
            pt.Show();
        }

        private void pELANGGANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pelanggan pl = new pelanggan();
            pl.Show();
        }

        private void lAYANANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layanan an = new layanan();
            an.Show();
        }

        private void uSERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user user = new user();
            user.Show();
        }

        private void bIAYATAMBAHANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            biayatambahan bt = new biayatambahan();
            bt.Show();
        }

        private void hOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dETAILORDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datadetailorder dr = new datadetailorder();
            dr.Show();

        }

        private void rEPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report r = new report();
            r.Show();
        }
    }
}

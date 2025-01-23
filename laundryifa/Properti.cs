using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laundryifa
{
    internal class Properti
    {
        public static SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-18L8S2S;Initial Catalog=LaundrySyifa;Integrated Security=True;MultipleActiveResultSets=true");

        public static SqlConnection getconn() { return new SqlConnection(@"Data Source=DESKTOP-18L8S2S;Initial Catalog=LaundrySyifa;Integrated Security=True;MultipleActiveResultSets=true"); }

        public static string enkripsi(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
                
            }
            return sb.ToString();
        }

        public static bool validasi(Control.ControlCollection container, TextBoxBase kosong = null)
        {
            foreach (Control c in container)
            {
                if (c is TextBoxBase textBox && string.IsNullOrWhiteSpace(textBox.Text) && textBox != kosong)
                {
                    return true;
                }

            } return false;
        }

        public static string enkripsii(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2")); // Mengubah byte ke format heksadesimal
                }
                return sb.ToString();
            }
        }

    }
}

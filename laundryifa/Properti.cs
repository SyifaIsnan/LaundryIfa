﻿using System;
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
        public static SqlConnection conn = new SqlConnection(@"Data Source=localhost\MSSQLSERVER01;Initial Catalog=dblaundry;Integrated Security=True");

        public static SqlConnection getconn() 
        { 
            return new SqlConnection(@"Data Source=localhost\MSSQLSERVER01;Initial Catalog=dblaundry;Integrated Security=True"); 
        }

        

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

       

        public static SqlConnection konek()
        {
            return new SqlConnection(@"Data Source=localhost\MSSQLSERVER01;Initial Catalog=dblaundry;Integrated Security=True");
        }

        public static bool latihanvalid (Control.ControlCollection container, TextBox kosong = null)
        {
            foreach (Control c in container)
            {
                if (c is TextBoxBase textBox && string.IsNullOrWhiteSpace(textBox.Text) && textBox != kosong)
                {
                    return true;
                }
            } return false;

        }
    
        public static string latihanenk(string input)
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

       













    }
}

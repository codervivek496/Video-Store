using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoStore1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text==null || txtUsername.Text==null)
            {
                MessageBox.Show("Enter the UseeName and Password");
            }

            else
            {
                if (txtUsername.Text=="Admin" && txtPassword.Text=="12345")
                {
                    CustomerForm frm = new CustomerForm();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Enter correct ID and Password");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}

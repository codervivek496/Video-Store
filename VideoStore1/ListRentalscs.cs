using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VideoStore1
{
    public partial class ListRentalscs : Form
    {
        public ListRentalscs()
        {
            InitializeComponent();
        }

        private void cmbCustomerId_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("SELECT NAME FROM CUSTOMERS1 where Id=" + cmbCustomerId.Text, connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            txtCustomerName.Text = dr["Name"].ToString();

            connection.Close();

            SqlCommand cmd2 = new SqlCommand("USP_LISTRENTALSCUSTOMERS", connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            SqlParameter pCustomerid = new SqlParameter("@CustomerId", cmbCustomerId.Text.ToString());
            cmd2.Parameters.Add(pCustomerid);
            connection.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Tapeid", typeof(System.Int32));
            dt.Columns.Add("CustomerId", typeof(System.Int32));
            dt.Columns.Add("rented", typeof(System.String));
            dt.Columns.Add("DueBack", typeof(System.String));
            while(dr2.Read())
            {
                DataRow row = dt.NewRow();
                row["Tapeid"] = dr2["Tapeid"];
                row["CustomerId"] = dr2["CustomerId"];
                row["rented"] = dr2["rented"];
                row["DueBack"] = dr2["DueBack"];
                dt.Rows.Add(row);
            }
            dgvRentalsCustomers.DataSource = dt;
            connection.Close();

        }

        private void ListRentalscs_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("Select ID from customers1", connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbCustomerId.Items.Add(dr["Id"]);
            }
            connection.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            this.Hide();
            customerForm.Show();

        }
    }
}

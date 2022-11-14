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

namespace VideoStore1
{
    public partial class AllRentals : Form
    {
        public AllRentals()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            this.Hide();
            customerForm.Show();
        }

        private void AllRentals_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("USP_GETALLRENTALS", connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string str = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(System.Int32));
            dt.Columns.Add("Name", typeof(System.String));
            dt.Columns.Add("Address", typeof(System.String));
            dt.Columns.Add("City", typeof(System.String));
            dt.Columns.Add("State", typeof(System.String));
            dt.Columns.Add("Zip", typeof (System.String));
            dt.Columns.Add("phone", typeof(System.String));
            dt.Columns.Add("Member Since",typeof(System.String));
            dt.Columns.Add("Tapeid", typeof(System.Int32));
            dt.Columns.Add("CustomerId", typeof(System.Int32));
            dt.Columns.Add("rented", typeof(System.String));
            dt.Columns.Add("DueBack", typeof(System.String));
            while (dr.Read())
            {
                DataRow row = dt.NewRow();
                row["ID"] = dr["ID"];
                row["Name"] = dr["Name"];
                row["Address"] = dr["Address"];
                row["City"] = dr["City"];
                row["State"] = dr["State"];
                row["Zip"] = dr["Zip"];
                row["Phone"] = dr["Phone"];
                row["Member Since"] = dr["Membersince"];
                row["Tapeid"] = dr["Tapeid"];
                row["CustomerId"] = dr["CustomerId"];
                row["rented"] = dr["rented"];
                row["DueBack"] = dr["DueBack"];
                dt.Rows.Add(row);
            }
            dgvAllRentals.DataSource = dt;
            connection.Close();
        }
    }
}

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
    public partial class RentMovies : Form
    {
        public RentMovies()
        {
            InitializeComponent();
        }
        private void RentMovies_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("USP_GETCUSTOMER", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable  dt = new DataTable();
            dt.Columns.Add("ID", typeof(System.Int32));
            dt.Columns.Add("Name", typeof(System.String));
            dt.Columns.Add("Address", typeof(System.String));
            dt.Columns.Add("City", typeof(System.String));
            dt.Columns.Add("State", typeof(System.String));
            dt.Columns.Add("Zip", typeof(System.String));
            dt.Columns.Add("Phone", typeof(System.String));
            dt.Columns.Add("MemberSince", typeof(System.String));
            while (dr.Read())
            {
            
                DataRow row = dt.NewRow();
                row["ID"] = dr["ID"];
                row["Name"] = dr["Name"];
                row["Address"] = dr["Address"];
                row["City"] = dr["City"];
                row["State"] = dr["State"];
                row["Zip"] = dr["Zip"];
                row["phone"] = dr["Phone"];
                row["MemberSince"] = dr["MemberSince"];
                dt.Rows.Add(row);
            }
            dgvRentCustomer.DataSource = dt;
            connection.Close();


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            RentMovies2 rentMovies = new RentMovies2(Convert.ToInt32(dgvRentCustomer.SelectedRows[0].Cells[0].Value.ToString()));

            this.Hide();
            rentMovies.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerForm frm = new CustomerForm();
            this.Hide();
            frm.Show();
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Media;
using System.Windows.Forms;

namespace VideoStore1
{
    public partial class RentMovies2 : Form
    {
        public RentMovies2(int customerid)
        {
            InitializeComponent();
            txtCustomerId.Text = customerid.ToString();

            dgvCart.ColumnCount = 4;
            dgvCart.Columns[0].Name = "Taped ID";
            dgvCart.Columns[1].Name = "Customer ID";
            dgvCart.Columns[2].Name = "Rented";
            dgvCart.Columns[3].Name = "Due Back";
        }

        //public RentMovies2(int customerid, int tapeid, string rented, string dueBack)
        //{
        //    InitializeComponent();
        //    dgvCart.Rows.Add();
        //    dgvCart.Rows[0].Cells[0].Value = customerid;
        //}

        private void RentMovies2cs_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("USP_GETTAPES", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(System.String));
            dt.Columns.Add("Title", typeof(System.String));
            dt.Columns.Add("Cost", typeof(System.Int32));
            dt.Columns.Add("RENTALPRICE", typeof(System.Int32));
            dt.Columns.Add("QUANTITYAVAILABLE", typeof(System.Int32));
            dt.Columns.Add("QUANTITYRENTED", typeof(System.Int32));
            while (dr.Read())
            {
                DataRow row = dt.NewRow();
                row["ID"] = dr["ID"];
                row["Title"] = dr["Title"];
                row["Cost"] = dr["Cost"];
                row["RENTALPRICE"] = dr["RENTALPRICE"];
                row["QUANTITYAVAILABLE"] = dr["QUANTITYAVAILABLE"];
                row["QUANTITYRENTED"] = dr["QUANTITYRENTED"];
                dt.Rows.Add(row);
            }
            dgvMovies.DataSource = dt;
            connection.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtTapedId.Text == null)
                {
                    MessageBox.Show("Please Select Movie Name");
                }
                else if(txtCustomerId.Text == null)
                {
                    MessageBox.Show("Please go back and Select Customer ID");
                }
                else
                {
                    dgvCart.Rows.Add(txtTapedId.Text, txtCustomerId.Text, Convert.ToDateTime(dtpkrRented.Text), Convert.ToDateTime(dtpkrRented.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            txtCustomerId.Text = "";
            txtTapedId.Text = "";
            dtpkrDue.Text = "";
            dtpkrRented.Text = "";
            dgvCart.Rows.Clear();
        }

        private void dgvMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTapedId.Text = dgvMovies.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
                SqlCommand cmd = new SqlCommand("USP_ADDTOCART", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter pTapedId = new SqlParameter("@TapedId", txtTapedId.Text.ToString());
                SqlParameter pCustomerId = new SqlParameter("@CustomerId", txtCustomerId.Text.ToString());
                SqlParameter pRented = new SqlParameter("@Rented", Convert.ToDateTime(dtpkrRented.Text.ToString()));
                SqlParameter pDueBack = new SqlParameter("@DueBack", Convert.ToDateTime(dtpkrDue.Text.ToString()));
                cmd.Parameters.Add(pTapedId);
                cmd.Parameters.Add(pCustomerId);
                cmd.Parameters.Add(pRented);
                cmd.Parameters.Add(pDueBack);
                connection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Booked");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            RentMovies rentMovies = new RentMovies();
            this.Hide();
            rentMovies.Show();
        }
    }
}
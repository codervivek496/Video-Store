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
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("Select ID from customers1", connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbId.Items.Add(dr["Id"]);
            }
            connection.Close();

            SqlCommand cmd2 = new SqlCommand("USP_GETCUSTOMER", connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID", typeof(System.Int32));
            dt2.Columns.Add("Name", typeof(System.String));
            dt2.Columns.Add("Address", typeof(System.String));
            dt2.Columns.Add("City", typeof(System.String));
            dt2.Columns.Add("State", typeof(System.String));
            dt2.Columns.Add("Zip", typeof(System.String));
            dt2.Columns.Add("Phone", typeof(System.String));
            dt2.Columns.Add("MemberSince", typeof(System.String));
            while (dr2.Read())
            {
                DataRow row = dt2.NewRow();
                row["ID"] = dr2["ID"];
                row["Name"] = dr2["Name"];
                row["Address"] = dr2["Address"];
                row["City"] = dr2["City"];
                row["State"] = dr2["State"];
                row["Zip"] = dr2["Zip"];
                row["phone"] = dr2["Phone"];
                row["MemberSince"] = dr2["MemberSince"];
                dt2.Rows.Add(row);
            }
            dgvCustomers.DataSource = dt2;
            connection.Close();


        }

        private void cmbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("SELECT NAME, ADDRESS, CITY, STATE, ZIP, PHONE, MEMBERSINCE FROM CUSTOMERS1 where Id=" + cmbId.Text, connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            txtName.Text = dr["Name"].ToString();
            txtAddress.Text = dr["Address"].ToString();
            txtCity.Text = dr["City"].ToString();
            txtState.Text = dr["State"].ToString();
            txtZip.Text = dr["zip"].ToString();
            txtPhone.Text = dr["Phone"].ToString();
            txtMemberSince.Text = dr["MemberSince"].ToString();
            connection.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("USP_INSERT_CUSTOMERdETAILS", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pName = new SqlParameter("@Name", txtName.Text);
            SqlParameter pAddress = new SqlParameter("@Address", txtAddress.Text);
            SqlParameter pCity = new SqlParameter("@City", txtCity.Text);
            SqlParameter pState = new SqlParameter("@State", txtState.Text);
            SqlParameter pZip = new SqlParameter("@Zip", txtZip.Text);
            SqlParameter pPhone = new SqlParameter("@Phone", txtPhone.Text);
            SqlParameter pMemberSince = new SqlParameter("MemberSince", txtMemberSince.Text);
            cmd.Parameters.Add(pName);
            cmd.Parameters.Add(pAddress);
            cmd.Parameters.Add(pCity);
            cmd.Parameters.Add(pState);
            cmd.Parameters.Add(pZip);
            cmd.Parameters.Add(pPhone);
            cmd.Parameters.Add(pMemberSince);
            connection.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data inserted");
            connection.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("USP_EDIT_CUSTOMERDETAILS", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pName = new SqlParameter("@Name", txtName.Text);
            SqlParameter pAddress = new SqlParameter("@Address", txtAddress.Text);
            SqlParameter pCity = new SqlParameter("@City", txtCity.Text);
            SqlParameter pState = new SqlParameter("@State", txtState.Text);
            SqlParameter pZip = new SqlParameter("@Zip", txtZip.Text);
            SqlParameter pPhone = new SqlParameter("@Phone", txtPhone.Text);
            SqlParameter pMemberSince = new SqlParameter("MemberSince", txtMemberSince.Text);
            cmd.Parameters.Add(pName);
            cmd.Parameters.Add(pAddress);
            cmd.Parameters.Add(pCity);
            cmd.Parameters.Add(pState);
            cmd.Parameters.Add(pZip);
            cmd.Parameters.Add(pPhone);
            cmd.Parameters.Add(pMemberSince);
            connection.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Edited Details");
            connection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd = new SqlCommand("USP_DELETE_CUSTOMERDETAILS", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pId = new SqlParameter("@ID", cmbId.Text);
            cmd.Parameters.Add(pId);
            connection.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted");
            connection.Close();
        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtMemberSince.Text = "";

        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.SelectedRows[0].Cells[1].Value.ToString();
            txtAddress.Text = dgvCustomers.SelectedRows[0].Cells[2].Value.ToString();
            txtCity.Text= dgvCustomers.SelectedRows[0].Cells[3].Value.ToString();
            txtState.Text = dgvCustomers.SelectedRows[0].Cells[4].Value.ToString();
            txtZip.Text = dgvCustomers.SelectedRows[0].Cells[5].Value.ToString();
            txtPhone.Text = dgvCustomers.SelectedRows[0].Cells[6].Value.ToString();
            txtMemberSince.Text= dgvCustomers.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void mnuRentMovies_Click(object sender, EventArgs e)
        {
            RentMovies rentMovies = new RentMovies();
            this.Hide();
            rentMovies.Show();
        }

        private void mnuListRentals_Click(object sender, EventArgs e)
        {
            ListRentalscs listRentalscs = new ListRentalscs();
            this.Hide();
            listRentalscs.Show();
        }

        private void mnuListAllRentals_Click(object sender, EventArgs e)
        {
            AllRentals allRentals = new AllRentals();
            this.Hide();
            allRentals.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=HYDSQLDMO01\\TRN01; Database=WI_Training_VivekKumar; Integrated Security=true";
            SqlCommand cmd2 = new SqlCommand("USP_GETCUSTOMER", connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID", typeof(System.Int32));
            dt2.Columns.Add("Name", typeof(System.String));
            dt2.Columns.Add("Address", typeof(System.String));
            dt2.Columns.Add("City", typeof(System.String));
            dt2.Columns.Add("State", typeof(System.String));
            dt2.Columns.Add("Zip", typeof(System.String));
            dt2.Columns.Add("Phone", typeof(System.String));
            dt2.Columns.Add("MemberSince", typeof(System.String));
            while (dr2.Read())
            {
                DataRow row = dt2.NewRow();
                row["ID"] = dr2["ID"];
                row["Name"] = dr2["Name"];
                row["Address"] = dr2["Address"];
                row["City"] = dr2["City"];
                row["State"] = dr2["State"];
                row["Zip"] = dr2["Zip"];
                row["phone"] = dr2["Phone"];
                row["MemberSince"] = dr2["MemberSince"];
                dt2.Rows.Add(row);
            }
            dgvCustomers.DataSource = dt2;
            connection.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class Quản_Lý_Khách_Sạn_Nhà_Nghỉ : Form
    {
        SqlConnection con = new SqlConnection();
        public Quản_Lý_Khách_Sạn_Nhà_Nghỉ()
        {
            InitializeComponent();
        }

        private void Quản_Lý_Khách_Sạn_Nhà_Nghỉ_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            con.ConnectionString = connectionstring;
            con.Open();

            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Delete From tblPhong Where MaPhong = "P01"
            //Tạo câu lệnh xóa
            string sql = "Delete From tblPhong Where MaPhong = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "Select * From tblPhong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tabletblPhong = new DataTable();
            adp.Fill(tabletblPhong);

            dataGridView_tblPhong.DataSource = tabletblPhong;
        }

        private void dataGridView_tblPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDonGia.Text = dataGridView_tblPhong.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Text = dataGridView_tblPhong.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView_tblPhong.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtMaPhong.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("bạn cần nhập mã phòng");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("bạn cần nhập tên phòng");
                txtTenPhong.Focus();
            }
            else
            {
                //insert into tblPhong values()
                string sql = "insert into tblPhong values ('" + txtMaPhong.Text + "', '" + txtTenPhong.Text + "'";
                if (txtDonGia.Text != "")
                    sql = sql + "," + txtDonGia.Text.Trim();
                sql = sql + ")";
                //messageBox.Show(sql);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                loadDataToGridView();

            }
        }

        private void dataGridView_tblPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) ||
                (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaPhong.Enabled = false;
        }
        public void RunSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "UPDATE Phong SET TenPhong = N' " + txtTenPhong.Text + " ', DonGia = N' " + txtDonGia.Text + " ' WHERE MaPhong = ' "
                + txtMaPhong.Text + "'";
            RunSQL(sql);
            loadDataToGridView();
        }
    }
}

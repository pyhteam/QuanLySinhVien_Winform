using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using TP = BCrypt.Net.BCrypt;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QLDSV
{
    public partial class TaiKhoan : Form
    {
        MyDataTable dataTable = new MyDataTable();

        public TaiKhoan()
        {
            InitializeComponent();
            dataTable.OpenConnection();
            
        }

        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TaiKhoan");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator1.BindingSource = binding;

            // Liên kết dữ liệu từ DataGridView lên các control
            txtMaTaiKhoan.DataBindings.Clear();
            txtHoVaTen.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            txtTenDangNhap.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();
            cboQuyenHan.DataBindings.Clear();

            txtMaTaiKhoan.DataBindings.Add("Text", binding, "MaTaiKhoan");
            txtHoVaTen.DataBindings.Add("Text", binding, "HoVaTen");
            txtGhiChu.DataBindings.Add("Text", binding, "GhiChu");
            txtTenDangNhap.DataBindings.Add("Text", binding, "TenDangNhap");
            txtMatKhau.DataBindings.Add("Text", binding, "MatKhau");
            cboQuyenHan.DataBindings.Add("Text", binding, "QuyenHan");
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            // Lấy dữ liệu vào DataGridView và bindingNavigator
            LayDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaTaiKhoan"] = "TK";
            dataRow["HoVaTen"] = "";
            dataRow["GhiChu"] = "";
            dataRow["TenDangNhap"] = "";
            dataRow["MatKhau"] = "";
            dataRow["QuyenHan"] = "";
            dataTable.Rows.Add(dataRow);
            bindingNavigator1.BindingSource.MoveLast();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa dòng này không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator1.BindingSource.RemoveCurrent();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTra("MaTaiKhoan") &&
                           KiemTra("HoVaTen") &&
                           KiemTra("TenDangNhap") &&
                           KiemTra("MatKhau") &&
                           KiemTra("QuyenHan"))
            {
                int result = dataTable.Update();
                MessageBox.Show("Đã cập nhật thành công " + result + " dòng dữ liệu!", "Cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool KiemTra(string columnName)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                string value = row.Cells[columnName].Value.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Giá trị của ô không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {

            txtMaTaiKhoan.Clear();
            txtHoVaTen.Clear();
            txtGhiChu.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cboQuyenHan.Text = "";

            txtMaTaiKhoan.Focus();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {

            if (txtMaTaiKhoan.Text.Trim() == "")
                MessageBox.Show("Mã tài khoản không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoVaTen.Text.Trim() == "")
                MessageBox.Show("Họ và tên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenDangNhap.Text.Trim() == "")
                MessageBox.Show("Tên đăng nhập không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtMatKhau.Text.Trim() == "")
                MessageBox.Show("Mật khẩu không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboQuyenHan.Text.Trim() == "")
                MessageBox.Show("Quyền hạn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = @"INSERT INTO TaiKhoan VALUES(@MaTaiKhoan, @HoVaTen, @TenDangNhap, @MatKhau, @QuyenHan, @GhiChu)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@MaTaiKhoan", SqlDbType.NVarChar, 4).Value = txtMaTaiKhoan.Text;
                cmd.Parameters.Add("@HoVaTen", SqlDbType.NVarChar, 50).Value = txtHoVaTen.Text;
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 20).Value = txtTenDangNhap.Text;
                cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = TP.HashPassword(txtMatKhau.Text);
                cmd.Parameters.Add("@QuyenHan", SqlDbType.NVarChar, 10).Value = cboQuyenHan.Text;
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                dataTable.Update(cmd);

                LayDuLieu();
                bindingNavigator1.BindingSource.MoveLast();
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "MatKhau")
            {
                e.Value = "...........";
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "MatKhau")
            {
                string matKhauMaHoa = TP.HashPassword(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = matKhauMaHoa;
            }
        }
    }
}

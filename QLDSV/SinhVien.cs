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

namespace QLDSV
{
    public partial class SinhVien : Form
    {
        MyDataTable dataTable = new MyDataTable();
        public SinhVien()
        {
            InitializeComponent();
            dataTable.OpenConnection();

        }
        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM SinhVien");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator.BindingSource = binding;

            // Liên kết dữ liệu từ DataGridView lên các control
            txtMaSinhVien.DataBindings.Clear();
            txtHoVaTen.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            chkGioiTinhNu.DataBindings.Clear();
            dtpNgaySinh.DataBindings.Clear();
            cboQueQuan.DataBindings.Clear();
            cboMalop.DataBindings.Clear();

            txtMaSinhVien.DataBindings.Add("Text", binding, "MaSinhVien");
            txtHoVaTen.DataBindings.Add("Text", binding, "HoVaTen");
            txtDiaChi.DataBindings.Add("Text", binding, "DiaChi");
            chkGioiTinhNu.DataBindings.Add("Checked", binding, "GioiTinh");
            dtpNgaySinh.DataBindings.Add("Value", binding, "NgaySinh");
            cboQueQuan.DataBindings.Add("SelectedValue", binding, "MaQueQuan");
            cboMalop.DataBindings.Add("SelectedValue", binding, "MaLop");
        }

        public void LayDuLieu(DataGridViewComboBoxColumn columnName)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM QueQuan";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            columnName.DataSource = table;
            columnName.DisplayMember = "TenQueQuan";
            columnName.ValueMember = "MaQueQuan";
            columnName.DataPropertyName = "MaQueQuan";
        }

        public void LayDuLieu(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM QueQuan";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenQueQuan";
            comboBox.ValueMember = "MaQueQuan";
        }
        public void LayDuLieu1(DataGridViewComboBoxColumn columnName)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM LopHoc";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            columnName.DataSource = table;
            columnName.DisplayMember = "TenLop";
            columnName.ValueMember = "MaLop";
            columnName.DataPropertyName = "Malop";
        }

        public void LayDuLieu1(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM LopHoc";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenLop";
            comboBox.ValueMember = "MaLop";
        }

        private void SinhVien_Load(object sender, EventArgs e)
        {
            // Lấy dữ liệu vào ComboBox
            LayDuLieu(cboQueQuan);

            // Lấy dữ liệu vào các ComboBox của cột Quê quán
            LayDuLieu(MaQueQuan);

            // Lấy dữ liệu vào ComboBox
            LayDuLieu1(cboMalop);

            // Lấy dữ liệu vào các ComboBox của cột MaLop
            LayDuLieu1(MaLop);

            // Lấy dữ liệu vào DataGridView và bindingNavigator
            LayDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaSinhVien"] = "SV";
            dataRow["HoVaTen"] = "";
            dataRow["GioiTinh"] = 0;
            dataRow["NgaySinh"] = DateTime.Today;
            dataRow["DiaChi"] = "";
            dataRow["MaQueQuan"] = "";
            dataRow["MaLop"] = "";
            dataTable.Rows.Add(dataRow);
            bindingNavigator.BindingSource.MoveLast();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa dòng này không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTra("MaSinhVien") &&
               KiemTra("HoVaTen") &&
               KiemTra("MaLop") &&
               KiemTra("NgaySinh") &&
               KiemTra("MaQueQuan"))
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
            txtMaSinhVien.Clear();
            txtHoVaTen.Clear();
            txtDiaChi.Clear();
            chkGioiTinhNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Today;
            cboQueQuan.Text = "";
            cboMalop.Text = "";

            txtMaSinhVien.Focus();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            if (txtMaSinhVien.Text.Trim() == "")
                MessageBox.Show("Mã sinh viên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoVaTen.Text.Trim() == "")
                MessageBox.Show("Họ và tên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboQueQuan.Text.Trim() == "")
                MessageBox.Show("Quê quán không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = @"INSERT INTO SinhVien VALUES(@MaSinhVien,@MaLop, @HoVaTen, @GioiTinh, @NgaySinh, @DiaChi, @MaQueQuan)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@MaSinhVien", SqlDbType.NVarChar, 5).Value = txtMaSinhVien.Text;
                cmd.Parameters.Add("@HoVaTen", SqlDbType.NVarChar, 50).Value = txtHoVaTen.Text;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.TinyInt).Value = chkGioiTinhNu.Checked ? 1 : 0;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dtpNgaySinh.Value;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                cmd.Parameters.Add("@MaQueQuan", SqlDbType.NVarChar, 2).Value = cboQueQuan.SelectedValue.ToString();
                cmd.Parameters.Add("@MaLop", SqlDbType.NVarChar, 10).Value = cboMalop.SelectedValue.ToString();
                dataTable.Update(cmd);

                LayDuLieu();
                bindingNavigator.BindingSource.MoveLast();
            }
        }
    }
}
    
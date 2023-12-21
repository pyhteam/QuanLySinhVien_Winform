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
    public partial class CapNhat : Form
    {
        MyDataTable dataTable = new MyDataTable();
        public CapNhat()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }
        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM DiemHP");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator.BindingSource = binding;

            // Liên kết dữ liệu từ DataGridView lên các control
            txtMaSV.DataBindings.Clear();
            txtHoTen.DataBindings.Clear();
            cboLop.DataBindings.Clear();
            cboMonHoc.DataBindings.Clear();
            txtHocKy.DataBindings.Clear();
            txtDiemQT.DataBindings.Clear();
            txtDiemThi1.DataBindings.Clear();
            txtDiemThi2.DataBindings.Clear();
            txtDiemTK.DataBindings.Clear();

            txtMaSV.DataBindings.Add("Text", binding, "MaSinhVien");
            txtHoTen.DataBindings.Add("Text", binding, "HoVaTen");
            cboLop.DataBindings.Add("SelectedValue", binding, "MaLop");
            cboMonHoc.DataBindings.Add("SelectedValue", binding, "MaHocPhan");
            txtHocKy.DataBindings.Add("Text", binding, "HocKy");
            txtDiemQT.DataBindings.Add("Text", binding, "DiemQuaTrinh");
            txtDiemThi1.DataBindings.Add("Text", binding, "DiemLan1");
            txtDiemThi2.DataBindings.Add("Text", binding, "DiemLan2");
            txtDiemTK.DataBindings.Add("Text", binding, "DiemTongKet");

        }
        public void LayDuLieu(DataGridViewComboBoxColumn columnName)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM Khoa";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            columnName.DataSource = table;
            columnName.DisplayMember = "TenKhoa";
            columnName.ValueMember = "MaKhoa";
            columnName.DataPropertyName = "MaKhoa";
        }

        public void LayDuLieu(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM Khoa";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenKhoa";
            comboBox.ValueMember = "MaKhoa";
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
        public void LayDuLieu2(DataGridViewComboBoxColumn columnName)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM MonHoc";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            columnName.DataSource = table;
            columnName.DisplayMember = "TenMonHoc";
            columnName.ValueMember = "MaHocPhan";
            columnName.DataPropertyName = "MaHocPhan";
        }

        public void LayDuLieu2(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM MonHoc";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenHocPhan";
            comboBox.ValueMember = "MaHocPhan";
        }

        private void CapNhat_Load(object sender, EventArgs e)
        {
            // Lấy dữ liệu vào ComboBox
            LayDuLieu(cboKhoa);

            // Lấy dữ liệu vào các ComboBox của cột 
           

            // Lấy dữ liệu vào ComboBox
            LayDuLieu1(cboLop);

            // Lấy dữ liệu vào các ComboBox của cột MaLop
            LayDuLieu1(MaLop);

            // Lấy dữ liệu vào ComboBox
            LayDuLieu2(cboMonHoc);

            // Lấy dữ liệu vào các ComboBox của cột maHocPhan
            LayDuLieu2(MaHocPhan);

            // Lấy dữ liệu vào DataGridView và bindingNavigator
            LayDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaSinhVien"] = "";
            dataRow["HoVaTen"] = "";
            dataRow["MaLop"] = "";
            dataRow["MaHocPhan"] = "";
            dataRow["HocKy"] = "";
            dataRow["DiemQuaTrinh"] = "";
            dataRow["DiemLan1"] = "";
            dataRow["DiemLan2"] = "";
            dataRow["DiemTongKet"] = "";
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
               KiemTra("MaHocPhan") &&
               KiemTra("HocKy") &&
               KiemTra("DiemQuaTrinh") &&
               KiemTra("DiemLan1") &&
               KiemTra("DiemLan2") &&
               KiemTra("DiemTongKet"))
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
            txtMaSV.Clear();
            txtHoTen.Clear();
            cboKhoa.Text = "";
            cboLop.Text = "";
            cboMonHoc.Text = "";
            txtHocKy.Clear();
            txtDiemQT.Clear();
            txtDiemThi1.Clear();
            txtDiemThi2.Clear();
            txtDiemTK.Clear();


            txtMaSV.Focus();
        }

        private void btnLuuDuLieu1_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text.Trim() == "")
                MessageBox.Show("Mã môn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoTen.Text.Trim() == "")
                MessageBox.Show("Tên môn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboLop.Text.Trim() == "")
                MessageBox.Show("Mã khoa không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = @"INSERT INTO DiemHP VALUES(@MaSinhVien, @HoVaTen, @MaLop, @MaHocPhan, @HocKy,@DiemQuaTrinh,@DiemLan1,@DiemLan2,@DiemTongKet)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@MaSinhVien", SqlDbType.NVarChar, 10).Value = txtMaSV.Text;
                cmd.Parameters.Add("@HoVaTen", SqlDbType.NVarChar, 50).Value = txtHoTen.Text;
                cmd.Parameters.Add("@MaLop", SqlDbType.NVarChar, 5).Value = cboLop.SelectedValue.ToString();
                cmd.Parameters.Add("@MaHocPhan", SqlDbType.NVarChar, 10).Value = cboMonHoc.SelectedValue.ToString();
                cmd.Parameters.Add("@HocKy", SqlDbType.NVarChar, 5).Value = txtHocKy.Text;
                cmd.Parameters.Add("@DiemQuaTrinh", SqlDbType.NVarChar, 5).Value = txtDiemQT.Text;
                cmd.Parameters.Add("@DiemLan1", SqlDbType.NVarChar, 5).Value = txtDiemThi1.Text;
                cmd.Parameters.Add("@DiemLan2", SqlDbType.NVarChar, 5).Value = txtDiemThi2.Text;
                cmd.Parameters.Add("@DiemTongKet", SqlDbType.NVarChar, 5).Value = txtDiemTK.Text;
                dataTable.Update(cmd);

                LayDuLieu();
                bindingNavigator.BindingSource.MoveLast();
            }
        }
    }
}

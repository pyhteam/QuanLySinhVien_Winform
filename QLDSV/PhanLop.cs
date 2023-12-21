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
    public partial class PhanLop : Form
    {
        MyDataTable dataTable = new MyDataTable();
        public PhanLop()
        {
            InitializeComponent();
            dataTable.OpenConnection();
            
        }

        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PhanLop");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator.BindingSource = binding;

            // Liên kết dữ liệu từ DataGridView lên các control
            cboSinhVien.DataBindings.Clear();
            cboLop.DataBindings.Clear();
            cboNamHoc.DataBindings.Clear();

            cboSinhVien.DataBindings.Add("SelectedValue", binding, "MaSinhVien");
            cboLop.DataBindings.Add("SelectedValue", binding, "MaLop");
            cboNamHoc.DataBindings.Add("Text", binding, "NamHoc");
        }

        public void LayDuLieuSinhVien(DataGridViewComboBoxColumn columnName)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM SinhVien";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            columnName.DataSource = table;
            columnName.DisplayMember = "HoVaTen";
            columnName.ValueMember = "MaHocSinh";
            columnName.DataPropertyName = "MaHocSinh";
        }

        public void LayDuLieuSinhVien(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM SinhVien";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            comboBox.DataSource = table;
            comboBox.DisplayMember = "HoVaTen";
            comboBox.ValueMember = "MaSinhVien";
        }

        public void LayDuLieuLop(DataGridViewComboBoxColumn columnName)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();

            string sql = "SELECT * FROM LopHoc";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);

            columnName.DataSource = table;
            columnName.DisplayMember = "TenLop";
            columnName.ValueMember = "MaLop";
            columnName.DataPropertyName = "MaLop";
        }

        public void LayDuLieuLop(ComboBox comboBox)
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



        private void PhanLop_Load_1(object sender, EventArgs e)
        {
            LayDuLieuSinhVien(MaSinhVien);
            LayDuLieuSinhVien(cboSinhVien);

            LayDuLieuLop(MaLop);
            LayDuLieuLop(cboLop);

            LayDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaSinhVien"] = "SinhVien";
            dataRow["MaLop"] = "";
            dataRow["NamHoc"] = "2021 - 2022";
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
                KiemTra("MaLop") &&
                KiemTra("NamHoc"))
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

        private void button2_Click(object sender, EventArgs e)
        {
            cboSinhVien.Text = "";
            cboLop.Text = "";
            cboNamHoc.Text = "";

            cboSinhVien.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cboSinhVien.Text.Trim() == "")
                MessageBox.Show("Học sinh không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboLop.Text.Trim() == "")
                MessageBox.Show("Lớp không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboNamHoc.Text.Trim() == "")
                MessageBox.Show("Năm học không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = @"INSERT INTO PhanLop VALUES(@MaHocSinh, @MaLop, @NamHoc)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@MaHocSinh", SqlDbType.NVarChar, 5).Value = cboSinhVien.SelectedValue.ToString();
                cmd.Parameters.Add("@MaLop", SqlDbType.NVarChar, 5).Value = cboLop.SelectedValue.ToString();
                cmd.Parameters.Add("@NamHoc", SqlDbType.NVarChar, 12).Value = cboNamHoc.Text;
                dataTable.Update(cmd);

                // Lấy dữ liệu mới sau khi lưu
                LayDuLieu();
                bindingNavigator.BindingSource.MoveLast();
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}

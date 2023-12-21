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
    public partial class MonHoc : Form
    {
        MyDataTable dataTable = new MyDataTable();
        public MonHoc()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }
        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM MonHoc");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator1.BindingSource = binding;

            // Liên kết dữ liệu từ DataGridView lên các control
            txtMaMon.DataBindings.Clear();
            txtTenMon.DataBindings.Clear();
            txtHocKy.DataBindings.Clear();
            txtSoTC.DataBindings.Clear();
            cboKhoa.DataBindings.Clear();
            txtMaGV.DataBindings.Clear();


            txtMaMon.DataBindings.Add("Text", binding, "MaHocPhan");
            txtTenMon.DataBindings.Add("Text", binding, "TenMonHoc");
            txtHocKy.DataBindings.Add("Text", binding, "HocKy");
            txtSoTC.DataBindings.Add("Text", binding, "SoTC");
            cboKhoa.DataBindings.Add("SelectedValue", binding, "MaKhoa");
            txtMaGV.DataBindings.Add("Text", binding, "MaGV");
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

        private void MonHoc_Load(object sender, EventArgs e)
        {
            // Lấy dữ liệu vào ComboBox
            LayDuLieu(cboKhoa);

            // Lấy dữ liệu vào các ComboBox của cột MaKhoa
            LayDuLieu(MaKhoa);

            // Lấy dữ liệu vào DataGridView và bindingNavigator
            LayDuLieu();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaHocPhan"] = "";
            dataRow["TenMonHoc"] = "";
            dataRow["MaGV"] = "";
            dataRow["SoTC"] = "";
            dataRow["MaKhoa"] = "";
            dataRow["HocKy"] = "";
            dataTable.Rows.Add(dataRow);
            bindingNavigator1.BindingSource.MoveLast();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa dòng này không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator1.BindingSource.RemoveCurrent();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (KiemTra("MaHocPhan") &&
                KiemTra("TenMonHoc") &&
                KiemTra("SoTC") &&
                KiemTra("MaKhoa") &&
                KiemTra("MaGV") &&
                KiemTra("HocKy"))
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


        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemMoi1_Click(object sender, EventArgs e)
        {
            txtMaMon.Clear();
            txtTenMon.Clear();
            txtSoTC.Clear();
            txtHocKy.Clear();
            txtMaGV.Clear();
            cboKhoa.Text = "";

            txtMaMon.Focus();
        }

        private void btnLuuDuLieu1_Click(object sender, EventArgs e)
        {
            if (txtMaMon.Text.Trim() == "")
                MessageBox.Show("Mã môn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenMon.Text.Trim() == "")
                MessageBox.Show("Tên môn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboKhoa.Text.Trim() == "")
                MessageBox.Show("Mã khoa không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = @"INSERT INTO MonHoc VALUES(@MaHocPhan, @TenMonHoc, @MaKhoa,@MaGV, @SoTC, @HocKy)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@MaHocPhan", SqlDbType.NVarChar, 10).Value = txtMaMon.Text;
                cmd.Parameters.Add("@TenMonHoc", SqlDbType.NVarChar, 50).Value = txtTenMon.Text;
                cmd.Parameters.Add("@MaKhoa", SqlDbType.NVarChar, 10).Value = cboKhoa.SelectedValue.ToString();

                cmd.Parameters.Add("@MaGV", SqlDbType.NVarChar, 5).Value = txtMaGV.Text;
                
                cmd.Parameters.Add("@SoTC", SqlDbType.NVarChar, 5).Value = txtSoTC.Text;
                cmd.Parameters.Add("@HocKy", SqlDbType.NVarChar,10).Value = txtHocKy.Text;
                dataTable.Update(cmd);

                LayDuLieu();
                bindingNavigator1.BindingSource.MoveLast();
            }
        }

    }
}


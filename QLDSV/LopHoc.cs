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
    public partial class LopHoc : Form
    {
        MyDataTable dataTable = new MyDataTable();

        public LopHoc()
        {
            InitializeComponent();
            dataTable.OpenConnection();
         
        }

        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM LopHoc");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator1.BindingSource = binding;
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

        private void LopHoc_Load(object sender, EventArgs e)
        {


            // Lấy dữ liệu vào các ComboBox của cột 
            LayDuLieu(MaKhoa);

            LayDuLieu();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            DataRow dataRow = dataTable.NewRow();
            dataRow["MaLop"] = "";
            dataRow["TenLop"] = "";
            dataTable.Rows.Add(dataRow);
            bindingNavigator1.BindingSource.MoveLast();
        }

        private void bthXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa dòng này không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator1.BindingSource.RemoveCurrent();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTra("MaLop") && KiemTra("TenLop"))
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
    }
}

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
    public partial class QueQuan : Form
    {
        MyDataTable dataTable = new MyDataTable();

        public QueQuan()
        {
            InitializeComponent();
            dataTable.OpenConnection();
       
        }

        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM QueQuan");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator1.BindingSource = binding;
        }

        private void QueQuan_Load(object sender, EventArgs e)
        {
            LayDuLieu();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaQueQuan"] = "";
            dataRow["TenQueQuan"] = "";
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
            if (KiemTra("MaQueQuan") && KiemTra("TenQueQuan"))
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
    }
}

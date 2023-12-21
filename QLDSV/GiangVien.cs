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
    public partial class GiangVien : Form
    {
        MyDataTable dataTable = new MyDataTable();
       
        public GiangVien()
        {
            InitializeComponent();
            dataTable.OpenConnection();
           
        }
        public void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM GiangVien");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;

            dataGridView.DataSource = binding;
            bindingNavigator1.BindingSource = binding;

            // Liên kết dữ liệu từ DataGridView lên các control
            txtMaGV.DataBindings.Clear();
            txtHoVaTen.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            chkGioiTinhNu.DataBindings.Clear();


            txtMaGV.DataBindings.Add("Text", binding, "MaGV");
            txtHoVaTen.DataBindings.Add("Text", binding, "HoVaTen");
            txtEmail.DataBindings.Add("Text", binding, "Email");
            chkGioiTinhNu.DataBindings.Add("Checked", binding, "GioiTinh");
        }

        private void GiangVien_Load(object sender, EventArgs e)
        {
            LayDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["MaGV"] = "SV";
            dataRow["HoVaTen"] = "";
            dataRow["GioiTinh"] = 0;
          
            dataRow["Email"] = "";
           
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
            if (KiemTra("MaGV") &&
               KiemTra("HoVaTen") &&
               KiemTra("GioiTinh") &&
               
               KiemTra("Email"))
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

        private void btnThemMoi1_Click(object sender, EventArgs e)
        { 
            txtMaGV.Clear();
            txtHoVaTen.Clear();
            
            chkGioiTinhNu.Checked = false;
            txtEmail.Clear();

            txtMaGV.Focus();

        }

        private void btnLuuDuLieu1_Click(object sender, EventArgs e)

        {
            

            

            if (txtMaGV.Text.Trim() == "")
                MessageBox.Show("Mã GV không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
            else if (txtHoVaTen.Text.Trim() == "")
                MessageBox.Show("Họ và tên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (txtEmail.Text.Trim() == "")
                MessageBox.Show("Email không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = @"INSERT INTO GiangVien VALUES(@MaGV, @HoVaTen, @GioiTinh, @Email)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@MaGV", SqlDbType.NVarChar, 5).Value = txtMaGV.Text;
                cmd.Parameters.Add("@HoVaTen", SqlDbType.NVarChar, 50).Value = txtHoVaTen.Text;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.TinyInt).Value = chkGioiTinhNu.Checked ? 1 : 0;


                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 10).Value = txtEmail.Text;
                dataTable.Update(cmd);

                LayDuLieu();
                bindingNavigator1.BindingSource.MoveLast();
            }
        }
    }
}

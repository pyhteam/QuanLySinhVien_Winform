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
using TP = BCrypt.Net.BCrypt;

namespace QLDSV
{
    public partial class Main : Form
    {

        public Main()
        {
            Flash flash = new Flash();
            flash.ShowDialog();

            InitializeComponent();

        }

        #region Biến toàn cục
        QueQuan queQuan = null;
        LopHoc lopHoc = null;
        SinhVien sinhvien = null;

        TaiKhoan taiKhoan = null;
        DangNhap dangNhap = null;
        string hoVaTen = "";
        Khoa khoa = null;
        MonHoc monHoc = null;
        GiangVien Giangvien = null;
        CapNhat capNhat = null;
        #endregion

        #region Hệ thống
        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }
            ChuaDangNhap();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Phân quyền
        public void ChuaDangNhap()
        {
           btnDangNhap.Enabled = true;

            btnDangXuat.Enabled = false;
            btnQueQuan.Enabled = false;
            btnLopHoc.Enabled = false;
            btnSinhVien.Enabled = false;

            btnTaiKhoan.Enabled = false;
            mnuDangXuat.Enabled = false;
            mnuQueQuan.Enabled = false;
            mnuLopHoc.Enabled = false;
            mnuSinhVien.Enabled = false;

            mnuTaiKhoan.Enabled = false;


            lblTrangThai.Text = "Chưa đăng nhập";
        }

        public void QuanTriVien()
        {
            btnDangNhap.Enabled = false;

            btnDangXuat.Enabled = true;
            btnQueQuan.Enabled = false;
            btnLopHoc.Enabled = false;
            btnSinhVien.Enabled = false;

            btnTaiKhoan.Enabled = true; // Hiện Tài khoản

            mnuDangXuat.Enabled = true;
            mnuQueQuan.Enabled = false;
            mnuLopHoc.Enabled = false;
            mnuSinhVien.Enabled = false;

            mnuTaiKhoan.Enabled = true; // Hiện Tài khoản


            lblTrangThai.Text = "Quản trị viên: " + hoVaTen;
        }

        public void NhanVien()
        {
            btnDangNhap.Enabled = false;

            btnDangXuat.Enabled = true;
            btnQueQuan.Enabled = true;
            btnLopHoc.Enabled = true;
            btnSinhVien.Enabled = true;

            btnTaiKhoan.Enabled = false; // Ẩn Tài khoản

            mnuDangXuat.Enabled = true;
            mnuQueQuan.Enabled = true;
            mnuLopHoc.Enabled = true;
            mnuSinhVien.Enabled = true;

            mnuTaiKhoan.Enabled = false; // Ẩn Tài khoản



            lblTrangThai.Text = "Nhân viên: " + hoVaTen;
        }
        

        private void DangNhap()
        {
        LamLai:
            if (dangNhap == null || dangNhap.IsDisposed)
                dangNhap = new DangNhap();
            if (dangNhap.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(dangNhap.txtTenDangNhap.Text))
                {
                    MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto LamLai;
                }
                else if (string.IsNullOrWhiteSpace(dangNhap.txtMatKhau.Text))
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto LamLai;
                }
                else
                {
                    MyDataTable dataTable = new MyDataTable();
                    dataTable.OpenConnection();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM TaiKhoan WHERE TenDangNhap = @TDN");
                    cmd.Parameters.Add("@TDN", SqlDbType.NVarChar, 20).Value = dangNhap.txtTenDangNhap.Text;
                    dataTable.Fill(cmd);

                    if (dataTable.Rows.Count > 0)
                    {
                        hoVaTen = dataTable.Rows[0]["HoVaTen"].ToString();
                        string quyenHan = dataTable.Rows[0]["QuyenHan"].ToString();
                        string matKhauMaHoa = dataTable.Rows[0]["MatKhau"].ToString();
                        if (TP.Verify(dangNhap.txtMatKhau.Text, matKhauMaHoa))
                        {
                            if (quyenHan == "admin")
                                QuanTriVien();
                            else if (quyenHan == "user")
                                NhanVien();
                            else
                                ChuaDangNhap();
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            goto LamLai;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        goto LamLai;
                    }
                }
            }
        }
        #endregion

        #region Quản lý

        private void mnuQueQuan_Click(object sender, EventArgs e)
        {
            if (queQuan == null || queQuan.IsDisposed)
            {
                queQuan = new QueQuan();
                queQuan.MdiParent = this;
                queQuan.Show();
            }
            else
                queQuan.Activate();
        }

        private void mnuLopHoc_Click(object sender, EventArgs e)
        {
            if (lopHoc == null || lopHoc.IsDisposed)
            {
                lopHoc = new LopHoc();
                lopHoc.MdiParent = this;
                lopHoc.Show();
            }
            else
                lopHoc.Activate();
        }

        private void mnuSinhVien_Click(object sender, EventArgs e)
        {
            if (sinhvien == null || sinhvien.IsDisposed)
            {
                sinhvien = new SinhVien();
                sinhvien.MdiParent = this;
                sinhvien.Show();
            }
            else
                sinhvien.Activate();
        }

        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            if (taiKhoan == null || taiKhoan.IsDisposed)
            {
                taiKhoan = new TaiKhoan();
                taiKhoan.MdiParent = this;
                taiKhoan.Show();
            }
            else
                taiKhoan.Activate();
        }

        private void mnuCapNhat_Click(object sender, EventArgs e)
        {
            if (capNhat == null || capNhat.IsDisposed)
            {
                capNhat = new CapNhat();
                capNhat.MdiParent = this;
                capNhat.Show();
            }
            else
                khoa.Activate();
        }

        private void mnuKhoa_Click(object sender, EventArgs e)
        {
            if (khoa == null || khoa.IsDisposed)
            {
                khoa = new Khoa();
                khoa.MdiParent = this;
                khoa.Show();
            }
            else
                khoa.Activate();
        }

        private void mnuMonHoc_Click(object sender, EventArgs e)
        {
            if (monHoc == null || monHoc.IsDisposed)
            {
                monHoc = new MonHoc();
                monHoc.MdiParent = this;
                monHoc.Show();
            }
            else
                monHoc.Activate();
        }
        #endregion

        private void mnuGiangVien_Click(object sender, EventArgs e)
        {
            if (Giangvien == null || Giangvien.IsDisposed)
            {
                Giangvien = new GiangVien();
                Giangvien.MdiParent = this;
                Giangvien.Show();
            }
            else
                Giangvien.Activate();
        }

        #region Toolbar

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            mnuDangNhap_Click(sender, e);
        }




        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            mnuDangXuat_Click(sender, e);
        }

        private void btnQueQuan_Click(object sender, EventArgs e)
        {
            mnuQueQuan_Click(sender, e);
        }

        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            mnuLopHoc_Click(sender, e);
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            mnuSinhVien_Click(sender, e);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            mnuTaiKhoan_Click(sender, e);
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            mnuThoat_Click(sender, e);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ChuaDangNhap();
            DangNhap();
        }

        private void mnuDangNhap_Click_1(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void mnuDangXuat_Click_1(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }
            ChuaDangNhap();
        }
    }
}
       
        

        
 
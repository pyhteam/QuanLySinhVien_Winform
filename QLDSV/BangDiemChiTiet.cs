using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV
{
    public partial class BangDiemChiTiet : Form
    {
        MyDataTable dataTable = new MyDataTable();
        public BangDiemChiTiet()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        
    }
}

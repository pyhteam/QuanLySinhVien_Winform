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
    public partial class Flash : Form
    {
        public Flash()
        {
            InitializeComponent();
        }

        private void Flash_Load(object sender, EventArgs e)
        {
            timer1.Interval = 3000;
            timer1.Start();
        }

        

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Equipment_Management
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void 装备信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            装备基本信息 to_zbjbxx = new 装备基本信息();
            to_zbjbxx.Show();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Load load = new Load();
            load.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void 统计查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 经费明细管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 部门基本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            部门基本信息 to_bmjbxx = new 部门基本信息();
            to_bmjbxx.Show();
        }

        private void 人员基本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            人员基本信息 to_ryjbxx = new 人员基本信息();
            to_ryjbxx.Show();
        }

        private void 资料类型管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            资料类型管理 to_zllx = new 资料类型管理();
            to_zllx.Show();
        }

        private void 资料信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            资料信息管理 to_zlxx = new 资料信息管理();
            to_zlxx.Show();
        }

        private void 资料借阅管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            资料借阅管理 to_zljy = new 资料借阅管理();
            to_zljy.Show();
        }

        private void 资料归档管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            资料归档管理 to_zlgd = new 资料归档管理();
            to_zlgd.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

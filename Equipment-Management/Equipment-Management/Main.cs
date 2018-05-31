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

        private void 入库信息管理添加_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.入库信息管理添加().Show();
        }

        private void 入库信息管理删除_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.入库信息管理删除().Show();
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            仓库信息管理.Visible = true;
            出库信息管理.Visible = false;
            入库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            仓库信息添加.Visible = true;
            仓库信息修改.Visible = false;
            仓库信息删除.Visible = false;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
            //menuStrip1.Visible = false;
        }

        private void 仓库信息管理添加_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.仓库信息管理添加().Show();
        }

        private void 仓库信息修改修改_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.仓库信息管理修改().Show();
        }

        private void 仓库信息管理删除_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.仓库信息管理删除().Show();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            仓库信息管理.Visible = true;
            出库信息管理.Visible = false;
            入库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            仓库信息添加.Visible = false;
            仓库信息修改.Visible = true;
            仓库信息删除.Visible = false;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            仓库信息管理.Visible = true;
            出库信息管理.Visible = false;
            入库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            仓库信息添加.Visible = false;
            仓库信息修改.Visible = false;
            仓库信息删除.Visible = true;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 入库信息管理修改_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.入库信息管理修改().Show();
        }

        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            入库信息管理.Visible = true;
            出库信息管理.Visible = false;
            仓库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            入库信息添加.Visible = true;
            入库信息删除.Visible = false;
            入库信息修改.Visible = false;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            入库信息管理.Visible = true;
            出库信息管理.Visible = false;
            仓库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            入库信息添加.Visible = false;
            入库信息删除.Visible = false;
            入库信息修改.Visible = true;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            入库信息管理.Visible = true;
            出库信息管理.Visible = false;
            仓库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            入库信息添加.Visible = false;
            入库信息删除.Visible = true;
            入库信息修改.Visible = false;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 添加ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            出库信息管理.Visible = true;
            仓库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            入库信息管理.Visible = false;
            出库信息添加.Visible = true;
            出库信息删除.Visible = false;
            出库信息修改.Visible = false;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 修改ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            出库信息管理.Visible = true;
            仓库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            入库信息管理.Visible = false;
            出库信息添加.Visible = false;
            出库信息删除.Visible = false;
            出库信息修改.Visible = true;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            出库信息管理.Visible = true;
            仓库信息管理.Visible = false;
            装备库存盘点.Visible = false;
            入库信息管理.Visible = false;
            出库信息添加.Visible = false;
            出库信息删除.Visible = true;
            出库信息修改.Visible = false;
            装备经费管理.Visible = false;
            统计与查询.Visible = false;
            WelcomePicture.Visible = false;
        }

        private void 出库信息管理修改_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.出库信息管理修改().Show();
        }

        private void 出库信息管理删除_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.出库信息管理删除().Show();
        }

        private void 出库信息管理添加_Click(object sender, EventArgs e)
        {
            new 装备库存管理框.出库信息管理添加().Show();
        }
    }
}

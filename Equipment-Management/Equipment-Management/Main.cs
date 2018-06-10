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
            WelcomePicture.Visible = true;
            统计与查询.Visible = false;
            装备经费管理.Visible = false;
            装备库存管理.Visible = false;
            系统管理.Visible = false;
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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           // this.text
        }

        private void 出入库统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 将其他层的panel隐藏
            统计与查询.Visible = true;
            装备经费管理.Visible = false;
            装备库存管理.Visible = false;
            系统管理.Visible = false;

            // 将同层的panel隐藏
            出入库统计表.Visible = true;
            部门人员查询.Visible = false;
            装备经费汇总.Visible = false;
            装备库存流水统计表.Visible = false;
        }

        private void 部门人员查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 将其他层的panel隐藏
            统计与查询.Visible = true;
            装备经费管理.Visible = false;
            装备库存管理.Visible = false;
            系统管理.Visible = false;

            // 将同层的panel隐藏
            出入库统计表.Visible = false;
            部门人员查询.Visible = true;
            装备经费汇总.Visible = false;
            装备库存流水统计表.Visible = false;
        }

        private void 装备经费汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 将其他层的panel隐藏
            统计与查询.Visible = true;
            装备经费管理.Visible = false;
            装备库存管理.Visible = false;
            系统管理.Visible = false;

            // 将同层的panel隐藏
            出入库统计表.Visible = false;
            部门人员查询.Visible = false;
            装备经费汇总.Visible = true;
            装备库存流水统计表.Visible = false;
        }

        private void 装备库存流水统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 将其他层的panel隐藏
            统计与查询.Visible = true;
            装备经费管理.Visible = false;
            装备库存管理.Visible = false;
            系统管理.Visible = false;

            // 将同层的panel隐藏
            出入库统计表.Visible = false;
            部门人员查询.Visible = false;
            装备经费汇总.Visible = false;
            装备库存流水统计表.Visible = true;
        }

        private void 出入库信息日期选择按钮_Click(object sender, EventArgs e)
        {

        }

        private void 出入库信息查询按钮_Click(object sender, EventArgs e)
        {
            int flag = 0;       // 判定选择按钮
            if (出库信息查询选择.Checked == true)
            {
                flag = 1;
            }
            else if (入库信息查询选择.Checked == true)
                flag = 2;
            else if (出入库信息查询选择.Checked == true)
                flag = 0;

            if (flag == 0)  // 判断查询类型是否为空
            {
                MessageBox.Show("请选择您所查询的类型！");
                return;
            }
                

            string str = 出入库查询内容输入.Text;        //  读入查询信息
            if (str == "")          // 判定查询内容是否为空
            {
                MessageBox.Show("请输入查询内容！");
                return;
            }

            if(flag == 1)       // 选择查询内容类型
            {

            }
            else if(flag == 2)
            {

            }
            else if(flag == 3)
            {

            }


        }
    }
}

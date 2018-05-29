using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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

        //加载时运行的代码
        private void Main_Load(object sender, EventArgs e)
        {
            myPanel.lastpanels.Add(WelcomePicture);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
            Application.Exit();
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

        //点击查看账户
        private void 查看账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 系统管理, 用户管理, 查看账户});
            try
            {
                DBClass.conn.Open();
                SqlCommand cmd = new SqlCommand("select * from ArmsUsers", DBClass.conn);
                SqlDataReader dr = cmd.ExecuteReader();
                listBoxUserInfo.Items.Add("用户名".PadRight(10) + "用户类型".PadRight(10));
                while (dr.Read())
                {
                    listBoxUserInfo.Items.Add(dr["Usersname"].ToString().PadRight(10)
                        + dr["User_type"].ToString().PadRight(10));
                }
                dr.Close();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //点击添加账户
        private void 添加账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 系统管理, 用户管理, 添加账户 });
            comboBoxUserType.Items.AddRange(new List<string>() { "管理员" }.ToArray());
            LogInUser.AddFlag = false;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //账户添加确认
        private void buttonUserAddConfirm_Click(object sender, EventArgs e)
        {
            //检查是否能注册
            if (!LogInUser.AddFlag)
            {
                MessageBox.Show("信息未填写完整或有错误");
                return;
            }
            try
            {
                DBClass.conn.Open();
                string strcmd = String.Format("insert ArmsUsers(Usersname, Userspwd, User_type)" +
                    "valus('{0}','{1}','{2}')", textBoxUserName, textBoxPwd, comboBoxUserType);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);

                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //判断用户名是否重复
        private void textBoxUserName_Leave(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "")
            {
                return;
            }
            try
            {
                DBClass.conn.Open();
                string strCmd = "select UsersName from ArmsUsers where UsersName = '" + textBoxUserName.Text + "'";
                string strTemp = "";
                SqlCommand cmd = new SqlCommand(strCmd, DBClass.conn);
                //结果为空时，strTemp也为空
                if (cmd.ExecuteScalar() != null)
                {
                    strTemp = cmd.ExecuteScalar().ToString().Trim();//用trim去掉后面的空格
                }
                if (textBoxUserName.Text == strTemp)
                {
                    MessageBox.Show("用户名已存在");
                }
                else
                {
                    LogInUser.AddFlag = true;//用户名不存在时，标记为能注册
                }
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "\n打开数据库失败");
            }
        }

        //判断两次密码输入是否一致
        private void textBoxConfirmPwd_Leave(object sender, EventArgs e)
        {
            if (textBoxPwd.Text != textBoxConfirmPwd.Text)
            {
                MessageBox.Show("两次密码输入不正确");
            }
            else
            {
                LogInUser.AddFlag = true;//密码正确时，标记为可以注册
            }
        }

        private void 重置密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        //窗口加载时运行的代码
        private void Main_Load(object sender, EventArgs e)
        {
            //将欢迎panel加入lastpanels中（欢迎panel默认第一个显示，其余均隐藏）
            myPanel.lastpanels.Add(WelcomePicture);
            //判断登录的用户的权限，若权限不够则禁用功能
            /*LogInUser.userType = "管理员";
            if(LogInUser.userType == "管理员")
            {
                添加账户ToolStripMenuItem.Enabled = false;
                重置密码ToolStripMenuItem.Enabled = false;
                删除账户ToolStripMenuItem.Enabled = false;
            }*/
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { WelcomePicture });
            this.Hide();
            Load load = new Load();
            load.Show();
        }

        //菜单点击退出
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

        //菜单点击查看账户
        private void 查看账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //显示相应的panel
            myPanel.Show(new List<Panel>() { 系统管理, 用户管理, 查看账户 });
            try
            {
                //DBClass.conn.Open();
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

        //菜单点击添加账户
        private void 添加账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 系统管理, 用户管理, 添加账户 });
            comboBoxUserType.Items.AddRange(new List<string>() { "管理员" }.ToArray());
            LogInUser.addFlag = false;
        }

        //关闭窗口时运行的代码
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //账户添加确认
        private void buttonUserAddConfirm_Click(object sender, EventArgs e)
        {
            //检查是否能注册
            if (!LogInUser.addFlag)
            {
                MessageBox.Show("信息未填写完整或有错误");
                return;
            }
            try
            {
                //DBClass.conn.Open();
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
                //DBClass.conn.Open();
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
                    LogInUser.addFlag = true;//用户名不存在时，标记为能注册
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
                LogInUser.addFlag = true;//密码正确时，标记为可以注册
            }
        }

        //菜单点击重置密码
        private void 重置密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 系统管理, 用户管理, 重置密码 });
            try
            {
                //DBClass.conn.Open();
                SqlCommand cmd = new SqlCommand("select Usersname from ArmsUsers", DBClass.conn);
                SqlDataReader dr = cmd.ExecuteReader();
                listBoxUserInfo.Items.Add("用户名".PadRight(10));
                while (dr.Read())
                {
                    listBoxUserInfo.Items.Add(dr["Usersname"].ToString().PadRight(10));
                }
                dr.Close();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //重置密码确认
        private void buttonPwdReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要重置吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //DBClass.conn.Open();
                    string strcmd = String.Format("updata ArmsUsers set [Userspwd] = '{0}'" +
                        " where [Usersname] = '{1}'", String.Empty, listBoxPwdReset.SelectedItem.ToString());
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }
        }

        //菜单点击删除账户
        private void 删除账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //DBClass.conn.Open();
                    string strcmd = String.Format("delete from ArmsUsers " +
                        " where [Usersname] = '{0}'", listBoxPwdReset.SelectedItem.ToString());
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }
        }

        //菜单点击修改密码
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 系统管理, 用户管理, 修改密码 });
        }

        //修改密码确认
        private void buttonPwdModifyConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //DBClass.conn.Open();
                string strcmd = String.Format("updata ArmsUsers set [Userspwd] = '{0}'" +
                        " where [Usersname] = '{1}'", textBoxPwdModifyConfirmNewPwd.Text, LogInUser.userName);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //日志管理刷新
        private void LogManageRefresh()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from SysLog", DBClass.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SysLog");
            dataGridViewLogManage.DataSource = ds.Tables["SysLog"];
            da = new SqlDataAdapter("select [LogType] from SysLog", DBClass.conn);
            da.Fill(ds, "LogType");
            comboBoxLogManageSelectGenre.DataSource = ds.Tables["LogType"];
            da = new SqlDataAdapter("select [LogName] from SysLog", DBClass.conn);
            da.Fill(ds, "LogName");
            comboBoxLogManageSelectUser.DataSource = ds.Tables["LogName"];
        }

        //日志管理菜单点击日志管理
        private void 日志管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 系统管理, 日志管理 });
            comboBoxLogManageSelectGenre.Items.Add("空");
            comboBoxLogManageSelectGenre.SelectedItem = "空";
            comboBoxLogManageSelectUser.Items.Add("空");
            comboBoxLogManageSelectUser.SelectedItem = "空";
            try
            {
                //DBClass.conn.Open();
                LogManageRefresh();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //日志管理选择类别
        private void comboBoxLogManageSelectGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            strList.items.Clear();
            strList.items.AddRange(new List<string>() { comboBoxLogManageSelectGenre.SelectedItem.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedItem.ToString()});
            try
            {
                //DBClass.conn.Open();
                string strda = "select * from SysLog where 1 = 1";
                if (strList.items[0] != "空")
                {
                    strda += " and [LogType] = " + strList.items[0];
                }
                if (strList.timeFlag)
                {
                    strda += " and [LogDate] = " + strList.items[1];
                }
                if (strList.items[2] != "空")
                {
                    strda += " and [UserName] = " + strList.items[2];
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SysLog");
                dataGridViewLogManage.DataSource = ds.Tables["SysLog"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //日志管理选择日期
        private void dateTimePickerLogManage_CloseUp(object sender, EventArgs e)
        {
            strList.timeFlag = true;
            strList.items.Clear();
            strList.items.AddRange(new List<string>() { comboBoxLogManageSelectGenre.SelectedItem.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedItem.ToString()});
            try
            {
                //DBClass.conn.Open();
                string strda = "select * from SysLog where 1 = 1";
                if (strList.items[0] != "空")
                {
                    strda += " and [LogType] = " + strList.items[0];
                }
                if (strList.timeFlag)
                {
                    strda += " and [LogDate] = " + strList.items[1];
                }
                if (strList.items[2] != "空")
                {
                    strda += " and [UserName] = " + strList.items[2];
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SysLog");
                dataGridViewLogManage.DataSource = ds.Tables["SysLog"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //日志管理选择操作用户
        private void comboBoxLogManageSelectUser_SelectionChangeCommitted(object sender, EventArgs e)
        {
            strList.items.Clear();
            strList.items.AddRange(new List<string>() { comboBoxLogManageSelectGenre.SelectedItem.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedItem.ToString()});
            try
            {
                //DBClass.conn.Open();
                string strda = "select * from SysLog where 1 = 1";
                if (strList.items[0] != "空")
                {
                    strda += " and [LogType] = " + strList.items[0];
                }
                if (strList.timeFlag)
                {
                    strda += " and [LogDate] = " + strList.items[1];
                }
                if (strList.items[2] != "空")
                {
                    strda += " and [UserName] = " + strList.items[2];
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SysLog");
                dataGridViewLogManage.DataSource = ds.Tables["SysLog"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //清除日期
        private void buttonTimeFlagClear_Click(object sender, EventArgs e)
        {
            strList.timeFlag = false;
        }

        //日志管理控件隐藏时
        private void 日志管理_VisibleChanged(object sender, EventArgs e)
        {
            strList.timeFlag = false;
        }

        //日志管理点击逐个删除
        private void buttonLogManageDeleteOneByOne_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridViewLogManage.CurrentRow == null)
                {
                    return;
                }
                try
                {
                    //DBClass.conn.Open();
                    string strcmd = "delete from SysLog where 1 = 1 and [LogId] = " +
                        dataGridViewLogManage.CurrentRow.Cells[0].ToString();
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    LogManageRefresh();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }
        }

        //日志管理点击全部删除
        private void buttonLogManageDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //DBClass.conn.Open();
                    string strcmd = "delete from SysLog where 1 = 1";
                    if (strList.items[0] != "空")
                    {
                        strcmd += " and [LogType] = " + strList.items[0];
                    }
                    if (strList.timeFlag)
                    {
                        strcmd += " and [LogDate] = " + strList.items[1];
                    }
                    if (strList.items[2] != "空")
                    {
                        strcmd += " and [UserName] = " + strList.items[2];
                    }
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    LogManageRefresh();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }  
        }

        //日志管理点击全天删除
        private void buttonLogManageDeleteTheDay_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!strList.timeFlag)
                {
                    return;
                }
                try
                {
                    //DBClass.conn.Open();
                    string strcmd = "delete from SysLog where 1 = 1";
                    strcmd += " and [LogDate] = " + strList.items[1];
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    LogManageRefresh();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }    
        }

        //日志管理点击退出
        private void buttonLogManageExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                myPanel.Show(new List<Panel>() { WelcomePicture });
            }
        }

        //菜单点击查看维修记录
        private void 查看维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPanel.Show(new List<Panel>() { 装备维修管理, 查看维修记录 });
            comboBoxRepairManageSelectArms.Items.Add("空");
            comboBoxRepairManageSelectArms.SelectedItem = "空";
            comboBoxRepairManageSelectRepairMan.Items.Add("空");
            comboBoxRepairManageSelectRepairMan.SelectedItem = "空";
            try
            {
                //DBClass.conn.Open();
                RepairManageRefresh();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //装备维修管理刷新
        private void RepairManageRefresh()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from ArmsRepair", DBClass.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "RepairLog");
            dataGridViewRepairManage.DataSource = ds.Tables["ArmsRepair"];
            da = new SqlDataAdapter("select [Zbname] from ArmsInfo ai, ArmsRepair ar" +
                " where ai.[Zbid] = ar.[Zbid]", DBClass.conn);
            da.Fill(ds, "ArmsName");
            comboBoxRepairManageSelectArms.DataSource = ds.Tables["ArmsName"];
            da = new SqlDataAdapter("select [Ryname] from ArmsRepair", DBClass.conn);
            da.Fill(ds, "RepairMan");
            comboBoxRepairManageSelectRepairMan.DataSource = ds.Tables["RepairMan"];
        }

        //装备维修管理选择装备
        private void comboBoxSelectArms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            strList.items.Clear();
            strList.items.AddRange(new List<string>() { comboBoxRepairManageSelectArms.SelectedItem.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedItem.ToString()});
            try
            {
                //DBClass.conn.Open();
                SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = " + strList.items[0], DBClass.conn);
                strList.items[0] = cmd.ExecuteScalar().ToString();
                string strda = "select * from ArmsRepair where 1 = 1";
                if (strList.items[0] != "空")
                {
                    strda += " and [Zbid] = " + strList.items[0];
                }
                if (strList.timeFlag)
                {
                    strda += " and [RepairDate] = " + strList.items[1];
                }
                if (strList.items[2] != "空")
                {
                    strda += " and [Ryname] = " + strList.items[2];
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "RepairLog");
                dataGridViewRepairManage.DataSource = ds.Tables["RepairLog"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //装备维修管理选择日期
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            strList.timeFlag = true;
            strList.items.Clear();
            strList.items.AddRange(new List<string>() { comboBoxRepairManageSelectArms.SelectedItem.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedItem.ToString()});
            try
            {
                //DBClass.conn.Open();
                SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = " + strList.items[0], DBClass.conn);
                strList.items[0] = cmd.ExecuteScalar().ToString();
                string strda = "select * from SysLog where 1 = 1";
                if (strList.items[0] != "空")
                {
                    strda += " and [Zbid] = " + strList.items[0];
                }
                if (strList.timeFlag)
                {
                    strda += " and [RepairDate] = " + strList.items[1];
                }
                if (strList.items[2] != "空")
                {
                    strda += " and [Ryname] = " + strList.items[2];
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "RepairLog");
                dataGridViewRepairManage.DataSource = ds.Tables["RepairLog"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //装备维修管理清除日期
        private void buttonRepairManageTimeFlagClear_Click(object sender, EventArgs e)
        {
            strList.timeFlag = false;
        }

        //装备维修管理选择负责人
        private void comboBoxSelectRepairMan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            strList.items.Clear();
            strList.items.AddRange(new List<string>() { comboBoxRepairManageSelectArms.SelectedItem.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedItem.ToString()});
            try
            {
                //DBClass.conn.Open();
                SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = " + strList.items[0], DBClass.conn);
                strList.items[0] = cmd.ExecuteScalar().ToString();
                string strda = "select * from SysLog where 1 = 1";
                if (strList.items[0] != "空")
                {
                    strda += " and [Zbid] = " + strList.items[0];
                }
                if (strList.timeFlag)
                {
                    strda += " and [RepairDate] = " + strList.items[1];
                }
                if (strList.items[2] != "空")
                {
                    strda += " and [Ryname] = " + strList.items[2];
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "RepairLog");
                dataGridViewRepairManage.DataSource = ds.Tables["RepairLog"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //查看维修记录隐藏时
        private void 查看维修记录_VisibleChanged(object sender, EventArgs e)
        {
            strList.timeFlag = false;
        }

        //菜单点击删除维修记录
        private void 删除维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridViewRepairManage.CurrentRow == null)
                {
                    return;
                }
                try
                {
                    //DBClass.conn.Open();
                    string strcmd = "delete from SysLog where 1 = 1 and [LogId] = " +
                        dataGridViewRepairManage.CurrentRow.Cells[0].ToString();
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    RepairManageRefresh();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }
        }

        //菜单点击添加维修记录
        private void 添加维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //菜单点击修改维修记录
        private void 修改维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //菜单点击维修完成
        private void 维修完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

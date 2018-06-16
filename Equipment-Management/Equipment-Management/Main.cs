﻿using System;
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

        //窗口加载时运行的代码
        private void Main_Load(object sender, EventArgs e)
        {
            //将欢迎panel加入lastpanels中（欢迎panel默认第一个显示，其余均隐藏）
            MyPanel.lastpanels.Add(WelcomePicture);
            //判断登录的用户的权限，若权限不够则禁用功能
            if (LogInUser.userType != "管理员")
            {
                添加账户ToolStripMenuItem.Enabled = false;
                重置密码ToolStripMenuItem.Enabled = false;
                删除账户ToolStripMenuItem.Enabled = false;
                日志管理ToolStripMenuItem.Enabled = false;
                添加维修记录ToolStripMenuItem.Enabled = false;
                添加调拨记录ToolStripMenuItem.Enabled = false;
                删除维修记录ToolStripMenuItem.Enabled = false;
                删除调拨记录ToolStripMenuItem.Enabled = false;
                修改维修记录ToolStripMenuItem.Enabled = false;
                修改调拨记录ToolStripMenuItem.Enabled = false;
                维修完成ToolStripMenuItem.Enabled = false;
            }
        }

        //菜单点击注销
        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { WelcomePicture });//调用mypanel的show方法显示欢迎panel
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)//检验数据库是否已连接
                {
                    DBClass.conn.Open();//连接数据库
                }
                //将注销登录事件记录到syslog表内
                string strcmd = String.Format("insert SysLog(LogId, LogDate, LogTime," +
                        " LogType, Title, Body, UserName)values('{0}','{1}','{2}'" +
                        ",'{3}','{4}','{5}','{6}')", StreamCode.GetCode("LogId", "SysLog"),
                        DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),
                        "注销登录", "用户注销登录成功", "用户名: " + LogInUser.userName, LogInUser.userName);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                DBClass.conn.Close();
                //显示登录窗口
                this.Hide();
                new FormLogIn().Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //菜单点击退出
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //将注销登录事件记录到syslog表内
                string strcmd = String.Format("insert SysLog(LogId, LogDate, LogTime," +
                        " LogType, Title, Body, UserName)values('{0}','{1}','{2}'" +
                        ",'{3}','{4}','{5}','{6}')", StreamCode.GetCode("LogId", "SysLog"),
                        DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),
                        "注销登录", "用户注销登录成功", "用户名: " + LogInUser.userName, LogInUser.userName);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
            //退出程序
            Application.Exit();
        }

        //菜单点击查看账户
        private void 查看账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //显示相应的panel
            MyPanel.Show(new List<Panel>() { 系统管理, 用户管理, 查看账户 });
            listBoxUserInfo.Items.Clear();//先将items清空
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //用datareader读数据库并写入listboxuserinfo
                SqlCommand cmd = new SqlCommand("select * from ArmsUsers", DBClass.conn);
                SqlDataReader dr = cmd.ExecuteReader();
                listBoxUserInfo.Items.Add("用户名".PadRight(10) + "用户类型".PadRight(10));
                while (dr.Read())
                {
                    listBoxUserInfo.Items.Add(dr["Usersname"].ToString().PadRight(13)
                        + dr["User_type"].ToString().PadRight(14));
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
            MyPanel.Show(new List<Panel>() { 系统管理, 用户管理, 添加账户 });
            //添加两种用户类型到comboboxusertype
            comboBoxUserType.Items.Clear();
            comboBoxUserType.Items.AddRange(new List<string>() { "管理员", "普通用户" }.ToArray());
            LogInUser.addFlag = false;
        }

        //关闭窗口时运行的代码
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //添加注销登录的记录
                string strcmd = String.Format("insert SysLog(LogId, LogDate, LogTime," +
                        " LogType, Title, Body, UserName)values('{0}','{1}','{2}'" +
                        ",'{3}','{4}','{5}','{6}')", StreamCode.GetCode("LogId", "SysLog"),
                        DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),
                        "注销登录", "用户注销登录成功", "用户名: " + LogInUser.userName, LogInUser.userName);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
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
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //添加用户
                string strcmd = String.Format("insert ArmsUsers(Usersname, Userspwd, User_type)" +
                    "values('{0}','{1}','{2}')", textBoxUserName.Text, textBoxPwd.Text, comboBoxUserType.Text);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("用户已添加", "提示");
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //添加用户取消按钮
        private void buttonUserAddCancel_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { WelcomePicture });
        }

        //判断用户名是否重复
        private void textBoxUserName_Leave(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "")
            {
                LogInUser.addFlag = false;
                return;
            }
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strCmd = "select UsersName from ArmsUsers where UsersName = '" 
                    + textBoxUserName.Text + "'";
                string strTemp = "";
                SqlCommand cmd = new SqlCommand(strCmd, DBClass.conn);
                //结果为空时，strTemp也为空
                if (cmd.ExecuteScalar().ToString() != "")
                {
                    strTemp = cmd.ExecuteScalar().ToString().Trim();//用trim去掉后面的空格
                }
                //判断用户名是否一致
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
            if (textBoxPwd.Text == "" || textBoxConfirmPwd.Text == "")
            {
                LogInUser.addFlag = false;
                return;
            }
            if (textBoxPwd.Text != textBoxConfirmPwd.Text)
            {
                MessageBox.Show("两次密码输入不一致，请重新输入", "提示");
            }
            else
            {
                LogInUser.addFlag = true;//密码正确时，标记为可以注册
            }
        }

        //菜单点击重置密码
        private void 重置密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { 系统管理, 用户管理, 重置密码 });
            listBoxPwdReset.Items.Clear();
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //将所有用户列出
                SqlCommand cmd = new SqlCommand("select [Usersname] from ArmsUsers", DBClass.conn);
                SqlDataReader dr = cmd.ExecuteReader();
                listBoxPwdReset.Items.Add("用户名".PadRight(10));
                while (dr.Read())
                {
                    listBoxPwdReset.Items.Add(dr["Usersname"].ToString().PadRight(10));
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
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    //将选中的用户的密码重置为123456
                    string strcmd = String.Format("update ArmsUsers set [Userspwd] = '{0}'" +
                        " where [Usersname] = '{1}'", "123456", listBoxPwdReset.SelectedItem.ToString());
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("密码已重置为123456", "提示");
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
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    string strcmd = String.Format("delete from ArmsUsers " +
                        " where [Usersname] = '{0}'", 
                        listBoxUserInfo.SelectedItem.ToString().Substring(0,13).Trim());
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    //删除后直接刷新
                    listBoxUserInfo.Items.Clear();
                    SqlCommand cmd1 = new SqlCommand("select * from ArmsUsers", DBClass.conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    listBoxUserInfo.Items.Add("用户名".PadRight(10) + "用户类型".PadRight(10));
                    while (dr.Read())
                    {
                        listBoxUserInfo.Items.Add(dr["Usersname"].ToString().PadRight(13)
                            + dr["User_type"].ToString().PadRight(14));
                    }
                    dr.Close();
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
            MyPanel.Show(new List<Panel>() { 系统管理, 修改密码 });
        }

        //修改密码确认
        private void buttonPwdModifyConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //修改密码
                string strcmd = String.Format("update ArmsUsers set [Userspwd] = '{0}'" +
                        " where [Usersname] = '{1}'", 
                        textBoxPwdModifyConfirmNewPwd.Text, LogInUser.userName);
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("密码已修改", "提示");
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //修改密码点击取消
        private void buttonPwdModifyCancel_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { WelcomePicture });
        }

        //日志管理刷新
        private void LogManageRefresh()
        {
            //读数据到datagridview里
            SqlDataAdapter da = new SqlDataAdapter("select * from SysLog", DBClass.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SysLog");
            dataGridViewLogManage.DataSource = ds.Tables["SysLog"];
            //读数据到combobox里
            da = new SqlDataAdapter("select distinct [LogType] from SysLog", DBClass.conn);
            da.Fill(ds, "LogType");
            //向ds的logtype表内添加“空”行
            DataRow row1 = ds.Tables["LogType"].NewRow();
            row1["LogType"] = "空";
            ds.Tables["LogType"].Rows.InsertAt(row1, 0);
            comboBoxLogManageSelectGenre.DisplayMember = "LogType";
            comboBoxLogManageSelectGenre.ValueMember = "LogType";
            comboBoxLogManageSelectGenre.DataSource = ds.Tables["LogType"];
            //读数据到combobox里
            da = new SqlDataAdapter("select distinct [UserName] from SysLog", DBClass.conn);
            da.Fill(ds, "UserName");
            DataRow row2 = ds.Tables["UserName"].NewRow();
            row2["UserName"] = "空";
            ds.Tables["UserName"].Rows.InsertAt(row2, 0);
            comboBoxLogManageSelectUser.DisplayMember = "UserName";
            comboBoxLogManageSelectUser.ValueMember = "UserName";
            comboBoxLogManageSelectUser.DataSource = ds.Tables["UserName"];
        }

        //日志管理菜单点击日志管理
        private void 日志管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { 系统管理, 日志管理 });
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //调用刷新函数
                LogManageRefresh();
                comboBoxLogManageSelectGenre.SelectedValue = "空";
                comboBoxLogManageSelectUser.SelectedValue = "空";
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
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxLogManageSelectGenre.SelectedValue.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strda = "select * from SysLog where 1 = 1";
                //若数据不为"空"，则添加到select语句中
                if (StrList.items[0] != "空")
                {
                    strda += " and [LogType] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and [LogDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and [UserName] = '" + StrList.items[2] + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SysLog");
                dataGridViewLogManage.DataSource = ds.Tables["SysLog"];//绑定数据源
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
            //将timeflag设置为true
            StrList.timeFlag = true;
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxLogManageSelectGenre.SelectedValue.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strda = "select * from SysLog where 1 = 1";
                if (StrList.items[0] != "空")
                {
                    strda += " and [LogType] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and [LogDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and [UserName] = '" + StrList.items[2] + "'";
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
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxLogManageSelectGenre.SelectedValue.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strda = "select * from SysLog where 1 = 1";
                if (StrList.items[0] != "空")
                {
                    strda += " and [LogType] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and [LogDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and [UserName] = '" + StrList.items[2] + "'";
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
            StrList.timeFlag = false;
            //清除日期后就将日期还原为今天
            dateTimePickerLogManage.Value = DateTime.Now;
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxLogManageSelectGenre.SelectedValue.ToString(),
                dateTimePickerLogManage.Value.ToShortDateString(),
                comboBoxLogManageSelectUser.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strda = "select * from SysLog where 1 = 1";
                if (StrList.items[0] != "空")
                {
                    strda += " and [LogType] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and [LogDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and [UserName] = '" + StrList.items[2] + "'";
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

        //日志管理控件隐藏时
        private void 日志管理_VisibleChanged(object sender, EventArgs e)
        {
            StrList.timeFlag = false;
        }

        //日志管理点击逐个删除
        private void buttonLogManageDeleteOneByOne_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //未选中行
                if (dataGridViewLogManage.CurrentRow == null)
                {
                    MessageBox.Show("未选中行", "提示");
                    return;
                }
                try
                {
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    string strcmd = "delete from SysLog where 1 = 1 and [LogId] = '" +
                        dataGridViewLogManage.CurrentRow.Cells[0].Value.ToString() + "'";
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
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    string strcmd = "delete from SysLog where 1 = 1";
                    if (StrList.items[0] != "空")
                    {
                        strcmd += " and [LogType] = '" + StrList.items[0] + "'";
                    }
                    if (StrList.timeFlag)
                    {
                        strcmd += " and [LogDate] = '" + StrList.items[1] + "'";
                    }
                    if (StrList.items[2] != "空")
                    {
                        strcmd += " and [UserName] = '" + StrList.items[2] + "'";
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
                try
                {
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    //不管选没选中日期，都执行删除
                    string strcmd = "delete from SysLog where [LogDate] = '" +
                        dateTimePickerLogManage.Value.ToShortDateString() + "'";
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
                MyPanel.Show(new List<Panel>() { WelcomePicture });
            }
        }

        //菜单点击查看维修记录
        private void 查看维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备维修管理, 查看维修记录 });
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                RepairManageRefresh();
                comboBoxRepairManageSelectArms.Text = "空";
                comboBoxRepairManageSelectRepairMan.Text = "空";
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
            SqlDataAdapter da = new SqlDataAdapter("select ar.[RepId], ai.[Zbname], " +
                "ar.[RepairDate], ar.[Unit], ar.[Reason], ar.[Status], ar.[Cost], " +
                "ar.[Result], ar.[Ryname], ar.[Ryname1], ar.[PostDate] " +
                "from ArmsRepair ar, ArmsInfo ai " +
                "where ar.[Zbid] = ai.[Zbid]", DBClass.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ArmsRepair");
            dataGridViewRepairManage.DataSource = ds.Tables["ArmsRepair"];
            da = new SqlDataAdapter("select distinct ai.[Zbname] from ArmsInfo ai, ArmsRepair ar" +
                " where ai.[Zbid] = ar.[Zbid]", DBClass.conn);
            da.Fill(ds, "ArmsName");
            DataRow row1 = ds.Tables["ArmsName"].NewRow();
            row1["Zbname"] = "空";
            ds.Tables["ArmsName"].Rows.InsertAt(row1, 0);
            comboBoxRepairManageSelectArms.DisplayMember = "Zbname";
            comboBoxRepairManageSelectArms.ValueMember = "Zbname";
            comboBoxRepairManageSelectArms.DataSource = ds.Tables["ArmsName"];
            da = new SqlDataAdapter("select distinct [Ryname] from ArmsRepair", DBClass.conn);
            da.Fill(ds, "RepairMan");
            DataRow row2 = ds.Tables["RepairMan"].NewRow();
            row2["Ryname"] = "空";
            ds.Tables["RepairMan"].Rows.InsertAt(row2, 0);
            comboBoxRepairManageSelectRepairMan.DisplayMember = "Ryname";
            comboBoxRepairManageSelectRepairMan.ValueMember = "Ryname";
            comboBoxRepairManageSelectRepairMan.DataSource = ds.Tables["RepairMan"];
        }

        //装备维修管理选择装备
        private void comboBoxRepairManageSelectArms_SelectionChangeCommitted(object sender, EventArgs e)
        {

            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxRepairManageSelectArms.SelectedValue.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select ar.[RepId], ai.[Zbname], " +
                "ar.[RepairDate], ar.[Unit], ar.[Reason], ar.[Status], ar.[Cost], " +
                "ar.[Result], ar.[Ryname], ar.[Ryname1], ar.[PostDate] " +
                "from ArmsRepair ar, ArmsInfo ai where ar.[Zbid] = ai.[Zbid] ";
                if (StrList.items[0] != "空")
                {
                    strda += " and ar.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and ar.[RepairDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and ar.[Ryname] = '" + StrList.items[2] + "'";
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
        private void dateTimePickerRepairManageSelectDate_CloseUp(object sender, EventArgs e)
        {
            StrList.timeFlag = true;
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxRepairManageSelectArms.SelectedValue.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select ar.[RepId], ai.[Zbname], " +
                "ar.[RepairDate], ar.[Unit], ar.[Reason], ar.[Status], ar.[Cost], " +
                "ar.[Result], ar.[Ryname], ar.[Ryname1], ar.[PostDate] " +
                "from ArmsRepair ar, ArmsInfo ai where ar.[Zbid] = ai.[Zbid] ";
                if (StrList.items[0] != "空")
                {
                    strda += " and ar.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and ar.[RepairDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and ar.[Ryname] = '" + StrList.items[2] + "'";
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
            StrList.timeFlag = false;
            dateTimePickerRepairManage.Value = DateTime.Now;
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxRepairManageSelectArms.SelectedValue.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select ar.[RepId], ai.[Zbname], " +
                "ar.[RepairDate], ar.[Unit], ar.[Reason], ar.[Status], ar.[Cost], " +
                "ar.[Result], ar.[Ryname], ar.[Ryname1], ar.[PostDate] " +
                "from ArmsRepair ar, ArmsInfo ai where ar.[Zbid] = ai.[Zbid] ";
                if (StrList.items[0] != "空")
                {
                    strda += " and ar.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and ar.[RepairDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and ar.[Ryname] = '" + StrList.items[2] + "'";
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

        //装备维修管理选择负责人
        private void comboBoxRepairManageSelectRepairMan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxRepairManageSelectArms.SelectedValue.ToString(),
                dateTimePickerRepairManage.Value.ToShortDateString(),
                comboBoxRepairManageSelectRepairMan.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select ar.[RepId], ai.[Zbname], " +
                "ar.[RepairDate], ar.[Unit], ar.[Reason], ar.[Status], ar.[Cost], " +
                "ar.[Result], ar.[Ryname], ar.[Ryname1], ar.[PostDate] " +
                "from ArmsRepair ar, ArmsInfo ai where ar.[Zbid] = ai.[Zbid] ";
                if (StrList.items[0] != "空")
                {
                    strda += " and ar.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and ar.[RepairDate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and ar.[Ryname] = '" + StrList.items[2] + "'";
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
            StrList.timeFlag = false;
        }

        //菜单点击删除维修记录
        private void 删除维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairManage.CurrentRow == null)
            {
                MessageBox.Show("未选中要删除的行", "提示");
                return;
            }
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    string strcmd = "delete from ArmsRepair where [RepId] = '" +
                        dataGridViewRepairManage.CurrentRow.Cells[0].Value.ToString() + "'";
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
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备维修管理, 添加修改维修记录 });
            //将addmodifyflag设置为true，即添加
            AddModify.AddModifyFlag = true;
        }

        //菜单点击修改维修记录
        private void 修改维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairManage.CurrentRow == null)
            {
                MessageBox.Show("未选中要修改的行", "提示");
                return;
            }
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备维修管理, 添加修改维修记录 });
            AddModify.AddModifyFlag = false;
            //将所有的控件的选中项更改为相应的值
            comboBoxRepairManageAddModifySelectArms.SelectedValue =
                dataGridViewRepairManage.CurrentRow.Cells[1].Value.ToString();
            if (dataGridViewRepairManage.CurrentRow.Cells[2].Value.ToString() != "")
            {
                dateTimePickerRepairManageAddModifySelectDate.Value =
                    DateTime.Parse(dataGridViewRepairManage.CurrentRow.Cells[2].Value.ToString());
            }
            comboBoxRepairManageAddModifySelectUnit.SelectedValue =
                dataGridViewRepairManage.CurrentRow.Cells[3].Value.ToString();
            textBoxRepairManageAddModifyErrorReason.Text =
                dataGridViewRepairManage.CurrentRow.Cells[4].Value.ToString();
            comboBoxRepairManageAddModifySelectStatus.SelectedItem =
                dataGridViewRepairManage.CurrentRow.Cells[5].Value.ToString();
            textBoxRepairManageAddModifyCost.Text =
                dataGridViewRepairManage.CurrentRow.Cells[6].Value.ToString();
            textBoxRepairManageAddModifyResult.Text =
                dataGridViewRepairManage.CurrentRow.Cells[7].Value.ToString();
            comboBoxRepairManageAddModifySelectRepairMan.SelectedValue =
                dataGridViewRepairManage.CurrentRow.Cells[8].Value.ToString();
            comboBoxRepairManageAddModifySelectConfirmMan.SelectedValue =
                dataGridViewRepairManage.CurrentRow.Cells[9].Value.ToString();
        }

        //添加修改维修记录选择日期
        private void dateTimePickerRepairManageAddModifySelectDate_CloseUp(object sender, EventArgs e)
        {
            StrList.timeFlag = true;
        }

        //添加修改维修记录点击确定
        private void buttonlRepairManageAddModifyConfirm_Click(object sender, EventArgs e)
        {
            string zbid = "";
            string repairDate = "";
            string unit = "";
            string reason = "";
            string status = "";
            decimal cost = 0;
            string result = "";
            string repairMan = "";
            string confirmMan = "";
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                //添加
                if (AddModify.AddModifyFlag)
                {
                    //读入数据，方便添加
                    if(comboBoxRepairManageAddModifySelectArms.Text != "空")
                    {
                        string strcmd1 = String.Format("select [Zbid] from ArmsInfo where [Zbname] = '{0}'",
                             comboBoxRepairManageAddModifySelectArms.Text);
                        SqlCommand cmd1 = new SqlCommand(strcmd1, DBClass.conn);
                        zbid = cmd1.ExecuteScalar().ToString();
                    }
                    if(StrList.timeFlag)
                    {
                        repairDate = dateTimePickerRepairManageAddModifySelectDate.Value.ToShortDateString();
                    }
                    if (comboBoxRepairManageAddModifySelectUnit.Text != "空")
                    {
                        unit = comboBoxRepairManageAddModifySelectUnit.Text;
                    }
                    if(textBoxRepairManageAddModifyErrorReason.Text != "")
                    {
                        reason = textBoxRepairManageAddModifyErrorReason.Text;
                    }
                    if (comboBoxRepairManageAddModifySelectStatus.Text != "空")
                    {
                        status = comboBoxRepairManageAddModifySelectStatus.Text;
                    }
                    if (textBoxRepairManageAddModifyCost.Text != "")
                    {
                        cost = decimal.Parse(textBoxRepairManageAddModifyCost.Text);
                    }
                    if (textBoxRepairManageAddModifyResult.Text != "")
                    {
                        result = textBoxRepairManageAddModifyResult.Text;
                    }
                    if (comboBoxRepairManageAddModifySelectRepairMan.Text != "空")
                    {
                        repairMan = comboBoxRepairManageAddModifySelectRepairMan.Text;
                    }
                    if (comboBoxRepairManageAddModifySelectConfirmMan.Text != "空")
                    {
                        confirmMan = comboBoxRepairManageAddModifySelectConfirmMan.Text;
                    }
                    string strcmd2 = String.Format("insert ArmsRepair(RepId,Zbid,RepairDate,Unit," +
                                "Reason,Status,Cost,Result,Ryname,Ryname1,PostDate)values" +
                                "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                                StreamCode.GetCode("RepId", "ArmsRepair"), zbid,
                                repairDate,unit,reason,status,cost,result,repairMan,confirmMan,
                                DateTime.Now.ToShortDateString());
                    SqlCommand cmd2 = new SqlCommand(strcmd2, DBClass.conn);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("添加完成", "提示");
                }
                //修改
                else
                {
                    if (comboBoxRepairManageAddModifySelectArms.Text != "空")
                    {
                        string strcmd1 = String.Format("select [Zbid] from ArmsInfo where [Zbname] = '{0}'",
                             comboBoxRepairManageAddModifySelectArms.Text);
                        SqlCommand cmd1 = new SqlCommand(strcmd1, DBClass.conn);
                        zbid = cmd1.ExecuteScalar().ToString();
                    }
                    if (StrList.timeFlag)
                    {
                        repairDate = dateTimePickerRepairManageAddModifySelectDate.Value.ToShortDateString();
                    }
                    if (comboBoxRepairManageAddModifySelectUnit.Text != "空")
                    {
                        unit = comboBoxRepairManageAddModifySelectUnit.Text;
                    }
                    if (textBoxRepairManageAddModifyErrorReason.Text != "")
                    {
                        reason = textBoxRepairManageAddModifyErrorReason.Text;
                    }
                    if (comboBoxRepairManageAddModifySelectStatus.Text != "空")
                    {
                        status = comboBoxRepairManageAddModifySelectStatus.Text;
                    }
                    if (textBoxRepairManageAddModifyCost.Text != "")
                    {
                        cost = decimal.Parse(textBoxRepairManageAddModifyCost.Text);
                    }
                    if (textBoxRepairManageAddModifyResult.Text != "")
                    {
                        result = textBoxRepairManageAddModifyResult.Text;
                    }
                    if (comboBoxRepairManageAddModifySelectRepairMan.Text != "空")
                    {
                        repairMan = comboBoxRepairManageAddModifySelectRepairMan.Text;
                    }
                    if (comboBoxRepairManageAddModifySelectConfirmMan.Text != "空")
                    {
                        confirmMan = comboBoxRepairManageAddModifySelectConfirmMan.Text;
                    }
                    string strcmd2 = String.Format("update ArmsRepair set [Zbid] = '{0}', " +
                        "[RepairDate] = '{1}', [Unit] = '{2}', [Reason] = '{3}', [Status] = '{4}', " +
                        "[Cost] = '{5}', [Result] = '{6}', [Ryname] = '{7}', [Ryname1] = '{8}' " +
                        "where [RepId] = '{9}'",
                                zbid,repairDate,unit,reason,status,cost,result,repairMan,confirmMan,
                                dataGridViewRepairManage.CurrentRow.Cells[0].Value.ToString());
                    SqlCommand cmd2 = new SqlCommand(strcmd2, DBClass.conn);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("修改成功", "提示");
                }
                RepairManageRefresh();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //添加修改维修记录点击清除日期
        private void buttonlRepairManageAddModifyTimeFlagClear_Click(object sender, EventArgs e)
        {
            StrList.timeFlag = false;
            if (AddModify.AddModifyFlag)
            {
                dateTimePickerRepairManageAddModifySelectDate.Value = DateTime.Now;
            }
            //若为修改则重置为相应的值
            else
            {
                try
                {
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    string strcmd = "select [RepairDate] from ArmsReepair where [RepId] = '" +
                        dataGridViewRepairManage.CurrentRow.Cells[0].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    dateTimePickerRepairManageAddModifySelectDate.Value =
                        DateTime.Parse(cmd.ExecuteScalar().ToString());
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }
        }

        //添加修改维修记录点击返回上一级
        private void buttonlRepairManageAddModifyReturn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要返回吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备维修管理, 查看维修记录 });
            }
        }

        //更改添加修改维修记录控件可见性时加载
        private void 添加修改维修记录_VisibleChanged(object sender, EventArgs e)
        {
            string strda = "select distinct [Zbname] from ArmsInfo";
            SqlDataAdapter da1 = new SqlDataAdapter(strda, DBClass.conn);
            DataSet ds = new DataSet();
            da1.Fill(ds, "Zbname");
            DataRow row1 = ds.Tables["Zbname"].NewRow();
            row1["Zbname"] = "空";
            ds.Tables["Zbname"].Rows.InsertAt(row1, 0);
            comboBoxRepairManageAddModifySelectArms.DisplayMember = "Zbname";
            comboBoxRepairManageAddModifySelectArms.ValueMember = "Zbname";
            comboBoxRepairManageAddModifySelectArms.DataSource = ds.Tables["Zbname"];
            comboBoxRepairManageAddModifySelectArms.SelectedItem = "空";

            StrList.timeFlag = false;
            dateTimePickerRepairManageAddModifySelectDate.Value = DateTime.Now;

            strda = "select distinct [DepName] from Departments";
            SqlDataAdapter da2 = new SqlDataAdapter(strda, DBClass.conn);
            da2.Fill(ds, "Unit");
            DataRow row2 = ds.Tables["Unit"].NewRow();
            row2["DepName"] = "空";
            ds.Tables["Unit"].Rows.InsertAt(row2, 0);
            comboBoxRepairManageAddModifySelectUnit.DisplayMember = "DepName";
            comboBoxRepairManageAddModifySelectUnit.ValueMember = "DepName";
            comboBoxRepairManageAddModifySelectUnit.DataSource = ds.Tables["Unit"];
            comboBoxRepairManageAddModifySelectUnit.SelectedItem = "空";

            comboBoxRepairManageAddModifySelectStatus.Items.AddRange(
                new List<string>() { "空", "已送修", "维修完成" }.ToArray());

            //要将选人员的控件绑定到不同的表上，否则选一个，另一个也会变
            strda = "select distinct [Ryname] from ArmsPerson";
            SqlDataAdapter da3 = new SqlDataAdapter(strda, DBClass.conn);
            da3.Fill(ds, "Ryname");
            da3.Fill(ds, "Ryname1");
            DataRow row3 = ds.Tables["Ryname"].NewRow();
            row3["Ryname"] = "空";
            ds.Tables["Ryname"].Rows.InsertAt(row3, 0);
            DataRow row4 = ds.Tables["Ryname1"].NewRow();
            row4["Ryname"] = "空";
            ds.Tables["Ryname1"].Rows.InsertAt(row4, 0);
            comboBoxRepairManageAddModifySelectRepairMan.DisplayMember = "Ryname";
            comboBoxRepairManageAddModifySelectRepairMan.ValueMember = "Ryname";
            comboBoxRepairManageAddModifySelectConfirmMan.DisplayMember = "Ryname";
            comboBoxRepairManageAddModifySelectConfirmMan.ValueMember = "Ryname";
            comboBoxRepairManageAddModifySelectRepairMan.DataSource = ds.Tables["Ryname"];
            comboBoxRepairManageAddModifySelectConfirmMan.DataSource = ds.Tables["Ryname1"];
            comboBoxRepairManageAddModifySelectRepairMan.SelectedItem = "空";
            comboBoxRepairManageAddModifySelectConfirmMan.SelectedItem = "空";
        }

        //菜单点击装备维修完成
        private void 维修完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairManage.CurrentRow == null)
            {
                MessageBox.Show("未选中要设置为维修完成的行", "提示");
                return;
            }
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strcmd = "select [Status] from ArmsRepair where 1 = 1 and [RepId] = " +
                    dataGridViewRepairManage.CurrentRow.Cells[0].Value.ToString();
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                if (cmd.ExecuteScalar().ToString() == "维修完成")
                {
                    MessageBox.Show("维修已完成无需再设置!", "提示");
                    return;
                }
                MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备维修管理, 更新维修状态 });
                textBoxRepairManageStatusUpdateCost.Text =
                    dataGridViewRepairManage.CurrentRow.Cells[6].Value.ToString();
                textBoxRepairManageStatusUpdateResult.Text =
                    dataGridViewRepairManage.CurrentRow.Cells[7].Value.ToString();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //更新维修状态点击确认
        private void buttonRapirManageStatusUpdateConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strcmd = String.Format("update ArmsRepair set [Status] = '{0}', [Cost] = '{1}'," +
                    " [Result] = '{2}' where [RepId] = '{3}'", "维修完成", 
                    textBoxRepairManageStatusUpdateCost.Text,
                    textBoxRepairManageStatusUpdateResult.Text, 
                    dataGridViewRepairManage.CurrentRow.Cells[0].Value.ToString());
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("设置完成", "提示");
                RepairManageRefresh();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //更新维修状态点击取消
        private void buttonRapirManageStatusUpdateCancel_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备维修管理, 查看维修记录 });
        }

        //装备调拨管理刷新
        private void AllocationManageRefresh()
        {
            SqlDataAdapter da = new SqlDataAdapter("select aa.[Id], ai.[Zbname], " +
                "aa.[ANum], aa.[Zbprice], aa.[OutDep], aa.[InDep], aa.[AType], " +
                "aa.[Person], aa.[UsefulDate], aa.[Ryname1], aa.[Ryname], " +
                "aa.[ADate], aa.[Memo] from ArmsAllo aa, ArmsInfo ai " +
                "where aa.[Zbid] = ai.[Zbid]", DBClass.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ArmsAllo");
            dataGridViewAllocationManage.DataSource = ds.Tables["ArmsAllo"];
            da = new SqlDataAdapter("select distinct ai.[Zbname] from ArmsInfo ai, ArmsAllo aa" +
                " where ai.[Zbid] = aa.[Zbid]", DBClass.conn);
            da.Fill(ds, "ArmsName");
            DataRow row1 = ds.Tables["ArmsName"].NewRow();
            row1["Zbname"] = "空";
            ds.Tables["ArmsName"].Rows.InsertAt(row1, 0);
            comboBoxAllocationManageSelectArms.DisplayMember = "Zbname";
            comboBoxAllocationManageSelectArms.ValueMember = "Zbname";
            comboBoxAllocationManageSelectArms.DataSource = ds.Tables["ArmsName"];
            da = new SqlDataAdapter("select distinct [OutDep] from ArmsAllo", DBClass.conn);
            da.Fill(ds, "OutDep");
            DataRow row2 = ds.Tables["OutDep"].NewRow();
            row2["OutDep"] = "空";
            ds.Tables["OutDep"].Rows.InsertAt(row2, 0);
            comboBoxAllocationManageSelectOutDep.DisplayMember = "OutDep";
            comboBoxAllocationManageSelectOutDep.ValueMember = "OutDep";
            comboBoxAllocationManageSelectOutDep.DataSource = ds.Tables["OutDep"];
            da = new SqlDataAdapter("select distinct [InDep] from ArmsAllo", DBClass.conn);
            da.Fill(ds, "InDep");
            DataRow row3 = ds.Tables["InDep"].NewRow();
            row3["InDep"] = "空";
            ds.Tables["InDep"].Rows.InsertAt(row3, 0);
            comboBoxAllocationManageSelectInDep.DisplayMember = "InDep";
            comboBoxAllocationManageSelectInDep.ValueMember = "InDep";
            comboBoxAllocationManageSelectInDep.DataSource = ds.Tables["InDep"];
        }

        //菜单点击查看调拨记录
        private void 查看调拨记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备调拨管理, 查看调拨记录 });
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                AllocationManageRefresh();
                comboBoxAllocationManageSelectArms.SelectedValue= "空";
                comboBoxAllocationManageSelectOutDep.SelectedValue = "空";
                comboBoxAllocationManageSelectInDep.SelectedValue = "空";
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //查看装备调拨记录选择装备名称
        private void comboBoxAllocationManageSelectArms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxAllocationManageSelectArms.SelectedValue.ToString(),
                dateTimePickerAllocationManage.Value.ToShortDateString(),
                comboBoxAllocationManageSelectInDep.SelectedValue.ToString(),
                comboBoxAllocationManageSelectOutDep.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select aa.[Id], ai.[Zbname], " +
                "aa.[ANum], aa.[Zbprice], aa.[OutDep], aa.[InDep], aa.[AType], " +
                "aa.[Person], aa.[UsefulDate], aa.[Ryname1], aa.[Ryname], " +
                "aa.[ADate], aa.[Memo] from ArmsAllo aa, ArmsInfo ai where aa.[Zbid] = ai.[Zbid]";
                if (StrList.items[0] != "空")
                {
                    strda += " and aa.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and aa.[ADate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and aa.[InDep] = '" + StrList.items[2] + "'";
                }
                if (StrList.items[3] != "空")
                {
                    strda += " and aa.[OutDep] = '" + StrList.items[3] + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ArmsAllo");
                dataGridViewAllocationManage.DataSource = ds.Tables["ArmsAllo"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //查看装备调拨记录选择调拨日期
        private void dateTimePickerAllocationManage_CloseUp(object sender, EventArgs e)
        {
            StrList.timeFlag = true;
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxAllocationManageSelectArms.SelectedValue.ToString(),
                dateTimePickerAllocationManage.Value.ToShortDateString(),
                comboBoxAllocationManageSelectInDep.SelectedValue.ToString(),
                comboBoxAllocationManageSelectOutDep.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select aa.[Id], ai.[Zbname], " +
                "aa.[ANum], aa.[Zbprice], aa.[OutDep], aa.[InDep], aa.[AType], " +
                "aa.[Person], aa.[UsefulDate], aa.[Ryname1], aa.[Ryname], " +
                "aa.[ADate], aa.[Memo] from ArmsAllo aa, ArmsInfo ai where aa.[Zbid] = ai.[Zbid]";
                if (StrList.items[0] != "空")
                {
                    strda += " and aa.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and aa.[ADate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and aa.[InDep] = '" + StrList.items[2] + "'";
                }
                if (StrList.items[3] != "空")
                {
                    strda += " and aa.[OutDep] = '" + StrList.items[3] + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ArmsAllo");
                dataGridViewAllocationManage.DataSource = ds.Tables["ArmsAllo"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }


        //查看装备调拨记录清除日期
        private void buttonAllocationManageTimeFlagClear_Click(object sender, EventArgs e)
        {
            StrList.timeFlag = false;
            dateTimePickerAllocationManage.Value = DateTime.Now;
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxAllocationManageSelectArms.SelectedValue.ToString(),
                dateTimePickerAllocationManage.Value.ToShortDateString(),
                comboBoxAllocationManageSelectInDep.SelectedValue.ToString(),
                comboBoxAllocationManageSelectOutDep.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select aa.[Id], ai.[Zbname], " +
                "aa.[ANum], aa.[Zbprice], aa.[OutDep], aa.[InDep], aa.[AType], " +
                "aa.[Person], aa.[UsefulDate], aa.[Ryname1], aa.[Ryname], " +
                "aa.[ADate], aa.[Memo] from ArmsAllo aa, ArmsInfo ai where aa.[Zbid] = ai.[Zbid]";
                if (StrList.items[0] != "空")
                {
                    strda += " and aa.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and aa.[ADate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and aa.[InDep] = '" + StrList.items[2] + "'";
                }
                if (StrList.items[3] != "空")
                {
                    strda += " and aa.[OutDep] = '" + StrList.items[3] + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ArmsAllo");
                dataGridViewAllocationManage.DataSource = ds.Tables["ArmsAllo"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //查看装备调拨记录选择调出单位
        private void comboBoxAllocationManageSelectOutDep_SelectionChangeCommitted(object sender, EventArgs e)
        {
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxAllocationManageSelectArms.SelectedValue.ToString(),
                dateTimePickerAllocationManage.Value.ToShortDateString(),
                comboBoxAllocationManageSelectInDep.SelectedValue.ToString(),
                comboBoxAllocationManageSelectOutDep.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select aa.[Id], ai.[Zbname], " +
                "aa.[ANum], aa.[Zbprice], aa.[OutDep], aa.[InDep], aa.[AType], " +
                "aa.[Person], aa.[UsefulDate], aa.[Ryname1], aa.[Ryname], " +
                "aa.[ADate], aa.[Memo] from ArmsAllo aa, ArmsInfo ai where aa.[Zbid] = ai.[Zbid]";
                if (StrList.items[0] != "空")
                {
                    strda += " and aa.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and aa.[ADate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and aa.[InDep] = '" + StrList.items[2] + "'";
                }
                if (StrList.items[3] != "空")
                {
                    strda += " and aa.[OutDep] = '" + StrList.items[3] + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ArmsAllo");
                dataGridViewAllocationManage.DataSource = ds.Tables["ArmsAllo"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //查看装备调拨记录选择调入单位
        private void comboBoxAllocationManageSelectInDep_SelectionChangeCommitted(object sender, EventArgs e)
        {
            StrList.items.Clear();
            StrList.items.AddRange(new List<string>() {
                comboBoxAllocationManageSelectArms.SelectedValue.ToString(),
                dateTimePickerAllocationManage.Value.ToShortDateString(),
                comboBoxAllocationManageSelectInDep.SelectedValue.ToString(),
                comboBoxAllocationManageSelectOutDep.SelectedValue.ToString()});
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (StrList.items[0] != "空")
                {
                    SqlCommand cmd = new SqlCommand("select [Zbid] from ArmsInfo " +
                    "where [Zbname] = '" + StrList.items[0] + "'", DBClass.conn);
                    StrList.items[0] = cmd.ExecuteScalar().ToString();
                }
                string strda = "select aa.[Id], ai.[Zbname], " +
                "aa.[ANum], aa.[Zbprice], aa.[OutDep], aa.[InDep], aa.[AType], " +
                "aa.[Person], aa.[UsefulDate], aa.[Ryname1], aa.[Ryname], " +
                "aa.[ADate], aa.[Memo] from ArmsAllo aa, ArmsInfo ai where aa.[Zbid] = ai.[Zbid]";
                if (StrList.items[0] != "空")
                {
                    strda += " and aa.[Zbid] = '" + StrList.items[0] + "'";
                }
                if (StrList.timeFlag)
                {
                    strda += " and aa.[ADate] = '" + StrList.items[1] + "'";
                }
                if (StrList.items[2] != "空")
                {
                    strda += " and aa.[InDep] = '" + StrList.items[2] + "'";
                }
                if (StrList.items[3] != "空")
                {
                    strda += " and aa.[OutDep] = '" + StrList.items[3] + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(strda, DBClass.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ArmsAllo");
                dataGridViewAllocationManage.DataSource = ds.Tables["ArmsAllo"];
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //装备调拨管理添加修改调拨记录选择日期
        private void dateTimePickerAllocationManageAddModifySelectDate_CloseUp(object sender, EventArgs e)
        {
            StrList.timeFlag = true;
        }

        //装备调拨管理添加修改调拨记录点击清除日期
        private void buttonAllocationManageAddModifyTimeFlagClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                string strcmd = "select [ADate] from ArmsAllo where [Id] = '" +
                    dataGridViewAllocationManage.CurrentRow.Cells[0].Value.ToString() + "'";
                SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                dateTimePickerAllocationManageAddModifySelectDate.Value =
                    DateTime.Parse(cmd.ExecuteScalar().ToString());
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //装备调拨管理添加修改调拨记录确认修改
        private void buttonAllocationManageAddModifyConfirm_Click(object sender, EventArgs e)
        {
            string zbid = "";
            string aDate = "";
            string inDep = "";
            string outDep = "";
            string memo = "";
            string aType = "";
            decimal zbprice = 0;
            string aNum = "";
            string hostMan = "";
            string confirmMan = "";
            string pickMan = "";
            string userfulTime = "";
            try
            {
                if (DBClass.conn.State != ConnectionState.Open)
                {
                    DBClass.conn.Open();
                }
                if (AddModify.AddModifyFlag)
                {
                    if (comboBoxAllocationManageAddModifySelectArms.Text != "空")
                    {
                        string strcmd1 = String.Format("select [Zbid] from ArmsInfo where [Zbname] = '{0}'",
                             comboBoxAllocationManageAddModifySelectArms.Text);
                        SqlCommand cmd1 = new SqlCommand(strcmd1, DBClass.conn);
                        zbid = cmd1.ExecuteScalar().ToString();
                    }
                    if (dateTimePickerAllocationManageAddModifySelectDate.Value.ToShortDateString() != "")
                    {
                        aDate = dateTimePickerRepairManageAddModifySelectDate.Value.ToShortDateString();
                    }
                    if (comboBoxAllocationManageAddModifySelectInDep.Text != "空")
                    {
                        inDep = comboBoxAllocationManageAddModifySelectInDep.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectOutDep.Text != "空")
                    {
                        outDep = comboBoxAllocationManageAddModifySelectOutDep.Text;
                    }
                    if (textBoxAllocationManageAddModifyMemo.Text != "")
                    {
                        memo = textBoxAllocationManageAddModifyMemo.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectType.Text != "空")
                    {
                        aType = comboBoxAllocationManageAddModifySelectType.Text;
                    }
                    if (textBoxAllocationManageAddModifyNum.Text != "")
                    {
                        aNum = textBoxAllocationManageAddModifyNum.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectHostMan.Text != "空")
                    {
                        hostMan = comboBoxAllocationManageAddModifySelectHostMan.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectConfirmMan.Text != "空")
                    {
                        confirmMan = comboBoxAllocationManageAddModifySelectConfirmMan.Text;
                    }
                    if(comboBoxAllocationManageAddModifySelectPickMan.Text != "空")
                    {
                        pickMan = comboBoxAllocationManageAddModifySelectPickMan.Text;
                    }
                    if(textBoxAllocationManageAddModifyUsefulTime.Text != "")
                    {
                        userfulTime = textBoxAllocationManageAddModifyUsefulTime.Text;
                    }
                    if(textBoxAllocationManageAddModifyZbprice.Text != "")
                    {
                        zbprice = decimal.Parse(textBoxAllocationManageAddModifyZbprice.Text);
                    }
                    string strcmd2 = String.Format("insert ArmsAllo(Id,Zbid,ANum,Zbprice," +
                                "OutDep,InDep,AType,Person,UsefulDate,Ryname1,Ryname,ADate,Memo)values" +
                                "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'," +
                                "'{11}','{12}')",
                                StreamCode.GetCode("Id", "ArmsAllo"), zbid, aNum, zbprice, outDep
                                , inDep, aType, pickMan, userfulTime, confirmMan, hostMan, aDate, memo);
                    SqlCommand cmd3 = new SqlCommand(strcmd2, DBClass.conn);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("添加完成", "提示");
                }
                else
                {
                    if (comboBoxAllocationManageAddModifySelectArms.Text != "空")
                    {
                        string strcmd1 = String.Format("select [Zbid] from ArmsInfo where [Zbname] = '{0}'",
                             comboBoxAllocationManageAddModifySelectArms.Text);
                        SqlCommand cmd1 = new SqlCommand(strcmd1, DBClass.conn);
                        zbid = cmd1.ExecuteScalar().ToString();
                    }
                    if (StrList.timeFlag)
                    {
                        aDate = dateTimePickerAllocationManageAddModifySelectDate.Value.ToShortDateString();
                    }
                    if (comboBoxAllocationManageAddModifySelectInDep.Text != "空")
                    {
                        inDep = comboBoxAllocationManageAddModifySelectInDep.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectOutDep.Text != "空")
                    {
                        outDep = comboBoxAllocationManageAddModifySelectOutDep.Text;
                    }
                    if (textBoxAllocationManageAddModifyMemo.Text != "")
                    {
                        memo = textBoxAllocationManageAddModifyMemo.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectType.Text != "空")
                    {
                        aType = comboBoxAllocationManageAddModifySelectType.Text;
                    }
                    if (textBoxAllocationManageAddModifyNum.Text != "")
                    {
                        aNum = textBoxAllocationManageAddModifyNum.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectHostMan.Text != "空")
                    {
                        hostMan = comboBoxAllocationManageAddModifySelectHostMan.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectConfirmMan.Text != "空")
                    {
                        confirmMan = comboBoxAllocationManageAddModifySelectConfirmMan.Text;
                    }
                    if (comboBoxAllocationManageAddModifySelectPickMan.Text != "空")
                    {
                        pickMan = comboBoxAllocationManageAddModifySelectPickMan.Text;
                    }
                    if (textBoxAllocationManageAddModifyUsefulTime.Text != "")
                    {
                        userfulTime = textBoxAllocationManageAddModifyUsefulTime.Text;
                    }
                    if (textBoxAllocationManageAddModifyZbprice.Text != "")
                    {
                        zbprice = decimal.Parse(textBoxAllocationManageAddModifyZbprice.Text);
                    }
                    string strcmd2 = String.Format("update ArmsAllo set [Zbid] = '{0}', " +
                        "[ANum] = '{1}', [OutDep] = '{2}', [InDep] = '{3}', [AType] = '{4}', " +
                        "[Person] = '{5}', [UsefulDate] = '{6}', [Ryname1] = '{7}', [Ryname] = '{8}', " +
                        "[ADate] = '{9}', [Memo] = '{10}', [Zbprice] = '{11}'" +
                        "where [Id] = '{12}'",
                                zbid, aNum, inDep, outDep, aType, pickMan, userfulTime,
                                confirmMan, hostMan, aDate, memo, zbprice, 
                                dataGridViewAllocationManage.CurrentRow.Cells[0].Value.ToString());
                    SqlCommand cmd2 = new SqlCommand(strcmd2, DBClass.conn);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("修改完成", "提示");
                }
                AllocationManageRefresh();
                DBClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n打开数据库失败");
            }
        }

        //装备调拨管理添加修改调拨记录返回上一级
        private void buttonAllocationManageAddModifyReturn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要返回吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备调拨管理, 查看调拨记录 });
            }
        }

        //菜单点击添加调拨记录
        private void 添加调拨记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备调拨管理, 添加修改调拨记录 });
            AddModify.AddModifyFlag = true;
        }


        //菜单点击删除调拨记录
        private void 删除调拨记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllocationManage.CurrentRow == null)
            {
                MessageBox.Show("未选中要删除的行", "提示");
                return;
            }
            if (MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (DBClass.conn.State != ConnectionState.Open)
                    {
                        DBClass.conn.Open();
                    }
                    string strcmd = "delete from ArmsAllo where [Id] = '" +
                        dataGridViewAllocationManage.CurrentRow.Cells[0].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, DBClass.conn);
                    cmd.ExecuteNonQuery();
                    AllocationManageRefresh();
                    DBClass.conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n打开数据库失败");
                }
            }
        }

        //菜单点击修改调拨记录
        private void 修改调拨记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllocationManage.CurrentRow == null)
            {
                MessageBox.Show("未选中要修改的行", "提示");
                return;
            }
            MyPanel.Show(new List<Panel>() { 装备维修调拨管理, 装备调拨管理, 添加修改调拨记录 });
            AddModify.AddModifyFlag = false;
            comboBoxAllocationManageAddModifySelectArms.SelectedValue =
                dataGridViewAllocationManage.CurrentRow.Cells[1].Value.ToString();
            textBoxAllocationManageAddModifyNum.Text =
                dataGridViewAllocationManage.CurrentRow.Cells[2].Value.ToString();
            textBoxAllocationManageAddModifyZbprice.Text =
                dataGridViewAllocationManage.CurrentRow.Cells[3].Value.ToString();
            comboBoxAllocationManageAddModifySelectOutDep.SelectedValue =
                dataGridViewAllocationManage.CurrentRow.Cells[4].Value.ToString();
            comboBoxAllocationManageAddModifySelectInDep.SelectedValue =
                dataGridViewAllocationManage.CurrentRow.Cells[5].Value.ToString();
            comboBoxAllocationManageAddModifySelectType.SelectedItem =
                dataGridViewAllocationManage.CurrentRow.Cells[6].Value.ToString();
            comboBoxAllocationManageAddModifySelectPickMan.SelectedValue =
                dataGridViewAllocationManage.CurrentRow.Cells[7].Value.ToString();
            textBoxAllocationManageAddModifyUsefulTime.Text =
                dataGridViewAllocationManage.CurrentRow.Cells[8].Value.ToString();
            comboBoxAllocationManageAddModifySelectConfirmMan.SelectedValue =
                dataGridViewAllocationManage.CurrentRow.Cells[9].Value.ToString();
            comboBoxAllocationManageAddModifySelectHostMan.SelectedValue =
                dataGridViewAllocationManage.CurrentRow.Cells[10].Value.ToString();
            if (dataGridViewAllocationManage.CurrentRow.Cells[11].Value.ToString() != "")
            {
                dateTimePickerAllocationManageAddModifySelectDate.Value =
                    DateTime.Parse(dataGridViewAllocationManage.CurrentRow.Cells[11].Value.ToString());
            }
            textBoxAllocationManageAddModifyMemo.Text =
                dataGridViewAllocationManage.CurrentRow.Cells[12].Value.ToString();
        }

        //添加修改调拨记录控件的可见性改变时执行
        private void 添加修改调拨记录_VisibleChanged(object sender, EventArgs e)
        {
            string strda = "select distinct [Zbname] from ArmsInfo";
            SqlDataAdapter da1 = new SqlDataAdapter(strda, DBClass.conn);
            DataSet ds = new DataSet();
            da1.Fill(ds, "Zbname");
            DataRow row1 = ds.Tables["Zbname"].NewRow();
            row1["Zbname"] = "空";
            ds.Tables["Zbname"].Rows.InsertAt(row1, 0);
            comboBoxAllocationManageAddModifySelectArms.DisplayMember = "Zbname";
            comboBoxAllocationManageAddModifySelectArms.ValueMember = "Zbname";
            comboBoxAllocationManageAddModifySelectArms.DataSource = ds.Tables["Zbname"];
            comboBoxAllocationManageAddModifySelectArms.SelectedValue= "空";

            StrList.timeFlag = false;
            dateTimePickerRepairManageAddModifySelectDate.Value = DateTime.Now;

            strda = "select distinct [DepName] from Departments";
            SqlDataAdapter da2 = new SqlDataAdapter(strda, DBClass.conn);
            da2.Fill(ds, "Unit");
            DataRow row2 = ds.Tables["Unit"].NewRow();
            row2["DepName"] = "空";
            ds.Tables["Unit"].Rows.InsertAt(row2, 0);
            da2.Fill(ds, "Unit1");
            DataRow row3 = ds.Tables["Unit1"].NewRow();
            row3["DepName"] = "空";
            ds.Tables["Unit1"].Rows.InsertAt(row3, 0);
            comboBoxAllocationManageAddModifySelectInDep.DisplayMember = "DepName";
            comboBoxAllocationManageAddModifySelectInDep.ValueMember = "DepName";
            comboBoxAllocationManageAddModifySelectInDep.DataSource = ds.Tables["Unit"];
            comboBoxAllocationManageAddModifySelectInDep.SelectedValue = "空";
            comboBoxAllocationManageAddModifySelectOutDep.DisplayMember = "DepName";
            comboBoxAllocationManageAddModifySelectOutDep.ValueMember = "DepName";
            comboBoxAllocationManageAddModifySelectOutDep.DataSource = ds.Tables["Unit1"];
            comboBoxAllocationManageAddModifySelectOutDep.SelectedItem = "空";

            comboBoxAllocationManageAddModifySelectType.Items.Clear();
            comboBoxAllocationManageAddModifySelectType.Items.AddRange(
                new List<string>() { "空", "调拨", "价拨" }.ToArray());

            strda = "select distinct [Ryname] from ArmsPerson";
            SqlDataAdapter da3 = new SqlDataAdapter(strda, DBClass.conn);
            da3.Fill(ds, "Ryname1");
            DataRow row4 = ds.Tables["Ryname1"].NewRow();
            row4["Ryname"] = "空";
            ds.Tables["Ryname1"].Rows.InsertAt(row4, 0);
            da3.Fill(ds, "Ryname2");
            DataRow row5 = ds.Tables["Ryname2"].NewRow();
            row5["Ryname"] = "空";
            ds.Tables["Ryname2"].Rows.InsertAt(row5, 0);
            da3.Fill(ds, "Ryname3");
            DataRow row6 = ds.Tables["Ryname3"].NewRow();
            row6["Ryname"] = "空";
            ds.Tables["Ryname3"].Rows.InsertAt(row6, 0);
            comboBoxAllocationManageAddModifySelectPickMan.DisplayMember = "Ryname";
            comboBoxAllocationManageAddModifySelectPickMan.ValueMember = "Ryname";
            comboBoxAllocationManageAddModifySelectPickMan.DataSource = ds.Tables["Ryname1"];
            comboBoxAllocationManageAddModifySelectConfirmMan.DisplayMember = "Ryname";
            comboBoxAllocationManageAddModifySelectConfirmMan.ValueMember = "Ryname";
            comboBoxAllocationManageAddModifySelectConfirmMan.DataSource = ds.Tables["Ryname2"];
            comboBoxAllocationManageAddModifySelectHostMan.DisplayMember = "Ryname";
            comboBoxAllocationManageAddModifySelectHostMan.ValueMember = "Ryname";
            comboBoxAllocationManageAddModifySelectHostMan.DataSource = ds.Tables["Ryname3"];
            comboBoxAllocationManageAddModifySelectPickMan.SelectedValue = "空";
            comboBoxAllocationManageAddModifySelectConfirmMan.SelectedValue = "空";
            comboBoxAllocationManageAddModifySelectHostMan.SelectedValue = "空";

            textBoxAllocationManageAddModifyMemo.Text = "";
            textBoxAllocationManageAddModifyNum.Text = "";
            textBoxAllocationManageAddModifyUsefulTime.Text = "";
            textBoxAllocationManageAddModifyZbprice.Text = "";
        }
    }
}

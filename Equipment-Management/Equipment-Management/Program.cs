using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Equipment_Management
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Load());
        }
    }

    //记录登录的用户名及用户类型
    public static class LogInUser
    {
        public static string userName;
        public static string userType;
        public static bool addFlag;//添加账户时用于判断是否能添加
    }

    //连接数据库
    public static class DBClass
    {
        public static string strConn = "Server = 25.24.20.180; Initial Catalog = " +
            "Equipment Management Information System; uid = lilang; pwd = 201608";
        public static SqlConnection conn = new SqlConnection(strConn);
    }

    //panel控件的显示与隐藏
    public static class myPanel
    {
        public static List<Panel> lastpanels = new List<Panel>();
        //隐藏旧panel，显示新panel
        public static void Show(List<Panel> mypanels)
        {
            foreach(Panel items in lastpanels)
            {
                items.Visible = false;
            }
            lastpanels.Clear();
            lastpanels.AddRange(mypanels);
            foreach (Panel items in lastpanels)
            {
                items.Visible = true;
            }
        }
    }

    //存储用在cmd命令里的字符串
    public static class strList
    {
        public static List<string> items = new List<string>();
        public static bool timeFlag = false;
    }
}

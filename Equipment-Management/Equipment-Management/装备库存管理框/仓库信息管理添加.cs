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

namespace Equipment_Management.装备库存管理框
{
   /* public static class DBClass
    {
        public static String strConn = "Data Source=.;Initial Catalog = MSSQLSERVER;uid=zhangli;pwd=201608";
        public static SqlConnection conn = new SqlConnection(strConn);
    }*/

    public partial class 仓库信息管理添加 : Form
    {
        public 仓库信息管理添加()
        {
            InitializeComponent();
        }

        private void 仓库信息管理添加仓库编号_TextChanged(object sender, EventArgs e)
        {
           // DBClass.conn.Open();
        }
    }
}

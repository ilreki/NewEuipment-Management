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
    public partial class 装备基本信息 : Form
    {
        public 装备基本信息()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            编辑装备信息 do_zbxx = new 编辑装备信息();
            do_zbxx.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            编辑装备信息 do_zbxx = new 编辑装备信息();
            do_zbxx.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        

        }

        private void 装备基本信息_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“equipment_Management_Information_SystemDataSet.ArmsInfo”中。您可以根据需要移动或删除它。
            this.armsInfoTableAdapter.Fill(this.equipment_Management_Information_SystemDataSet.ArmsInfo);

        }
    }
}

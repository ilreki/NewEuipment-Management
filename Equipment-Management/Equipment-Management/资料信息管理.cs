﻿using System;
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
    public partial class 资料信息管理 : Form
    {
        public 资料信息管理()
        {
            InitializeComponent();
        }

        private void 资料信息管理_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“equipment_Management_Information_SystemDataSet4.ArmsData”中。您可以根据需要移动或删除它。
            this.armsDataTableAdapter.Fill(this.equipment_Management_Information_SystemDataSet4.ArmsData);

        }
    }
}

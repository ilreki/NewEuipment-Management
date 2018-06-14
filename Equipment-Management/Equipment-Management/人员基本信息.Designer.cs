namespace Equipment_Management
{
    partial class 人员基本信息
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ryidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rynameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nationalotyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.politicalPartyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cultureLevelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maritalConditionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.positionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.armsPersonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.equipment_Management_Information_SystemDataSet1 = new Equipment_Management.Equipment_Management_Information_SystemDataSet1();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.armsPersonTableAdapter = new Equipment_Management.Equipment_Management_Information_SystemDataSet1TableAdapters.ArmsPersonTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.armsPersonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipment_Management_Information_SystemDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(180, 358);
            this.treeView1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ryidDataGridViewTextBoxColumn,
            this.rynameDataGridViewTextBoxColumn,
            this.sexDataGridViewTextBoxColumn,
            this.nationalotyDataGridViewTextBoxColumn,
            this.politicalPartyDataGridViewTextBoxColumn,
            this.cultureLevelDataGridViewTextBoxColumn,
            this.maritalConditionDataGridViewTextBoxColumn,
            this.positionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.armsPersonBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(212, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(846, 358);
            this.dataGridView1.TabIndex = 1;
            // 
            // ryidDataGridViewTextBoxColumn
            // 
            this.ryidDataGridViewTextBoxColumn.DataPropertyName = "Ryid";
            this.ryidDataGridViewTextBoxColumn.HeaderText = "人员编号";
            this.ryidDataGridViewTextBoxColumn.Name = "ryidDataGridViewTextBoxColumn";
            // 
            // rynameDataGridViewTextBoxColumn
            // 
            this.rynameDataGridViewTextBoxColumn.DataPropertyName = "Ryname";
            this.rynameDataGridViewTextBoxColumn.HeaderText = "姓名";
            this.rynameDataGridViewTextBoxColumn.Name = "rynameDataGridViewTextBoxColumn";
            // 
            // sexDataGridViewTextBoxColumn
            // 
            this.sexDataGridViewTextBoxColumn.DataPropertyName = "Sex";
            this.sexDataGridViewTextBoxColumn.HeaderText = "性别";
            this.sexDataGridViewTextBoxColumn.Name = "sexDataGridViewTextBoxColumn";
            // 
            // nationalotyDataGridViewTextBoxColumn
            // 
            this.nationalotyDataGridViewTextBoxColumn.DataPropertyName = "Nationaloty";
            this.nationalotyDataGridViewTextBoxColumn.HeaderText = "民族";
            this.nationalotyDataGridViewTextBoxColumn.Name = "nationalotyDataGridViewTextBoxColumn";
            // 
            // politicalPartyDataGridViewTextBoxColumn
            // 
            this.politicalPartyDataGridViewTextBoxColumn.DataPropertyName = "Political_Party";
            this.politicalPartyDataGridViewTextBoxColumn.HeaderText = "政治面貌";
            this.politicalPartyDataGridViewTextBoxColumn.Name = "politicalPartyDataGridViewTextBoxColumn";
            // 
            // cultureLevelDataGridViewTextBoxColumn
            // 
            this.cultureLevelDataGridViewTextBoxColumn.DataPropertyName = "Culture_Level";
            this.cultureLevelDataGridViewTextBoxColumn.HeaderText = "文化程度";
            this.cultureLevelDataGridViewTextBoxColumn.Name = "cultureLevelDataGridViewTextBoxColumn";
            // 
            // maritalConditionDataGridViewTextBoxColumn
            // 
            this.maritalConditionDataGridViewTextBoxColumn.DataPropertyName = "Marital_Condition";
            this.maritalConditionDataGridViewTextBoxColumn.HeaderText = "婚姻状况";
            this.maritalConditionDataGridViewTextBoxColumn.Name = "maritalConditionDataGridViewTextBoxColumn";
            // 
            // positionDataGridViewTextBoxColumn
            // 
            this.positionDataGridViewTextBoxColumn.DataPropertyName = "Position";
            this.positionDataGridViewTextBoxColumn.HeaderText = "工作岗位";
            this.positionDataGridViewTextBoxColumn.Name = "positionDataGridViewTextBoxColumn";
            // 
            // armsPersonBindingSource
            // 
            this.armsPersonBindingSource.DataMember = "ArmsPerson";
            this.armsPersonBindingSource.DataSource = this.equipment_Management_Information_SystemDataSet1;
            // 
            // equipment_Management_Information_SystemDataSet1
            // 
            this.equipment_Management_Information_SystemDataSet1.DataSetName = "Equipment_Management_Information_SystemDataSet1";
            this.equipment_Management_Information_SystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 398);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(256, 398);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(380, 398);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "部门调转";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(522, 398);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "返回";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // armsPersonTableAdapter
            // 
            this.armsPersonTableAdapter.ClearBeforeFill = true;
            // 
            // 人员基本信息
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 443);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.treeView1);
            this.Name = "人员基本信息";
            this.Text = "人员基本信息";
            this.Load += new System.EventHandler(this.人员基本信息_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.armsPersonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipment_Management_Information_SystemDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private Equipment_Management_Information_SystemDataSet1 equipment_Management_Information_SystemDataSet1;
        private System.Windows.Forms.BindingSource armsPersonBindingSource;
        private Equipment_Management_Information_SystemDataSet1TableAdapters.ArmsPersonTableAdapter armsPersonTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ryidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rynameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nationalotyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn politicalPartyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cultureLevelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maritalConditionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn positionDataGridViewTextBoxColumn;
    }
}
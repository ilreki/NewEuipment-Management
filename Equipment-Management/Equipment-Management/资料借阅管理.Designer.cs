namespace Equipment_Management
{
    partial class 资料借阅管理
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.equipment_Management_Information_SystemDataSet3 = new Equipment_Management.Equipment_Management_Information_SystemDataSet3();
            this.dataLendBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataLendTableAdapter = new Equipment_Management.Equipment_Management_Information_SystemDataSet3TableAdapters.DataLendTableAdapter();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lendDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ryidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lendCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rynameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipment_Management_Information_SystemDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLendBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.dataNoDataGridViewTextBoxColumn,
            this.lendDateDataGridViewTextBoxColumn,
            this.ryidDataGridViewTextBoxColumn,
            this.lendCountDataGridViewTextBoxColumn,
            this.rynameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dataLendBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(643, 314);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(190, 357);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(358, 356);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "借阅";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(517, 357);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "归还";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(654, 356);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "归还确认";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // equipment_Management_Information_SystemDataSet3
            // 
            this.equipment_Management_Information_SystemDataSet3.DataSetName = "Equipment_Management_Information_SystemDataSet3";
            this.equipment_Management_Information_SystemDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataLendBindingSource
            // 
            this.dataLendBindingSource.DataMember = "DataLend";
            this.dataLendBindingSource.DataSource = this.equipment_Management_Information_SystemDataSet3;
            // 
            // dataLendTableAdapter
            // 
            this.dataLendTableAdapter.ClearBeforeFill = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "借阅号";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // dataNoDataGridViewTextBoxColumn
            // 
            this.dataNoDataGridViewTextBoxColumn.DataPropertyName = "DataNo";
            this.dataNoDataGridViewTextBoxColumn.HeaderText = "资料编号";
            this.dataNoDataGridViewTextBoxColumn.Name = "dataNoDataGridViewTextBoxColumn";
            // 
            // lendDateDataGridViewTextBoxColumn
            // 
            this.lendDateDataGridViewTextBoxColumn.DataPropertyName = "LendDate";
            this.lendDateDataGridViewTextBoxColumn.HeaderText = "借阅日期";
            this.lendDateDataGridViewTextBoxColumn.Name = "lendDateDataGridViewTextBoxColumn";
            // 
            // ryidDataGridViewTextBoxColumn
            // 
            this.ryidDataGridViewTextBoxColumn.DataPropertyName = "Ryid";
            this.ryidDataGridViewTextBoxColumn.HeaderText = "借阅人编号";
            this.ryidDataGridViewTextBoxColumn.Name = "ryidDataGridViewTextBoxColumn";
            // 
            // lendCountDataGridViewTextBoxColumn
            // 
            this.lendCountDataGridViewTextBoxColumn.DataPropertyName = "LendCount";
            this.lendCountDataGridViewTextBoxColumn.HeaderText = "借阅数量";
            this.lendCountDataGridViewTextBoxColumn.Name = "lendCountDataGridViewTextBoxColumn";
            // 
            // rynameDataGridViewTextBoxColumn
            // 
            this.rynameDataGridViewTextBoxColumn.DataPropertyName = "Ryname";
            this.rynameDataGridViewTextBoxColumn.HeaderText = "批准人";
            this.rynameDataGridViewTextBoxColumn.Name = "rynameDataGridViewTextBoxColumn";
            // 
            // 资料借阅管理
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 445);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "资料借阅管理";
            this.Text = "资料借阅管理";
            this.Load += new System.EventHandler(this.资料借阅管理_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipment_Management_Information_SystemDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLendBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private Equipment_Management_Information_SystemDataSet3 equipment_Management_Information_SystemDataSet3;
        private System.Windows.Forms.BindingSource dataLendBindingSource;
        private Equipment_Management_Information_SystemDataSet3TableAdapters.DataLendTableAdapter dataLendTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lendDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ryidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lendCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rynameDataGridViewTextBoxColumn;
    }
}
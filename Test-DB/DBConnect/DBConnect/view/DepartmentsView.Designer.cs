namespace DBConnect.view
{
    partial class DepartmentsView
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
            this.refreshButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.deptBox = new System.Windows.Forms.ComboBox();
            this.departmentsViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.departmentsViewBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentsViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentsViewBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(623, 320);
            this.dataGridView1.TabIndex = 0;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.refreshButton.Location = new System.Drawing.Point(536, 404);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            this.updateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.updateButton.Location = new System.Drawing.Point(16, 404);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(127, 23);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Изменить запись";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deleteButton.Location = new System.Drawing.Point(158, 404);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(108, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Удалить запись";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addButton.Location = new System.Drawing.Point(326, 404);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(152, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Добавить новую запись";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Имя";
            // 
            // nameBox
            // 
            this.nameBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.nameBox.Location = new System.Drawing.Point(12, 355);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(154, 20);
            this.nameBox.TabIndex = 6;
            // 
            // codeBox
            // 
            this.codeBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.codeBox.Location = new System.Drawing.Point(181, 355);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(100, 20);
            this.codeBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Код";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(395, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Родительский отдел";
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(178, 437);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 11;
            // 
            // deptBox
            // 
            this.deptBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deptBox.FormattingEnabled = true;
            this.deptBox.Location = new System.Drawing.Point(303, 355);
            this.deptBox.Name = "deptBox";
            this.deptBox.Size = new System.Drawing.Size(319, 21);
            this.deptBox.TabIndex = 12;
            // 
            // departmentsViewBindingSource
            // 
            this.departmentsViewBindingSource.DataSource = typeof(DBConnect.view.DepartmentsView);
            // 
            // departmentsViewBindingSource1
            // 
            this.departmentsViewBindingSource1.DataSource = typeof(DBConnect.view.DepartmentsView);
            // 
            // DepartmentsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.deptBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(250, 250);
            this.MinimumSize = new System.Drawing.Size(650, 500);
            this.Name = "DepartmentsView";
            this.Text = "DepartmentsView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DepartmentsView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentsViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentsViewBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox deptBox;
        private System.Windows.Forms.BindingSource departmentsViewBindingSource;
        private System.Windows.Forms.BindingSource departmentsViewBindingSource1;
    }
}
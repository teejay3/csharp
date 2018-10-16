namespace DBConnect.view
{
    partial class StuffView
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
            this.saveButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.idBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.patronymicLabel = new System.Windows.Forms.Label();
            this.surnameLabel = new System.Windows.Forms.Label();
            this.firstnameBox = new System.Windows.Forms.TextBox();
            this.firstnameLabel = new System.Windows.Forms.Label();
            this.patronymicBox = new System.Windows.Forms.TextBox();
            this.dobLabel = new System.Windows.Forms.Label();
            this.docseriesLabel = new System.Windows.Forms.Label();
            this.docseriesBox = new System.Windows.Forms.TextBox();
            this.docnumberLabel = new System.Windows.Forms.Label();
            this.docnumberBox = new System.Windows.Forms.TextBox();
            this.positionLabel = new System.Windows.Forms.Label();
            this.positionBox = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.surnameBox = new System.Windows.Forms.TextBox();
            this.showAllButton = new System.Windows.Forms.Button();
            this.deptLabel = new System.Windows.Forms.Label();
            this.deptComboBox = new System.Windows.Forms.ComboBox();
            this.dobBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.saveButton.Location = new System.Drawing.Point(28, 395);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(109, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Изменить запись";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 43);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(780, 281);
            this.dataGridView1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(88, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(501, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // idBox
            // 
            this.idBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.idBox.Location = new System.Drawing.Point(24, 356);
            this.idBox.Name = "idBox";
            this.idBox.ReadOnly = true;
            this.idBox.Size = new System.Drawing.Size(37, 20);
            this.idBox.TabIndex = 3;
            // 
            // idLabel
            // 
            this.idLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(28, 330);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 13);
            this.idLabel.TabIndex = 4;
            this.idLabel.Text = "ID";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Отдел:";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deleteButton.Location = new System.Drawing.Point(154, 395);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(106, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Удалить запись";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(151, 439);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 7;
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addButton.Location = new System.Drawing.Point(407, 395);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(149, 23);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Добавить новую запись";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // patronymicLabel
            // 
            this.patronymicLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.patronymicLabel.AutoSize = true;
            this.patronymicLabel.Location = new System.Drawing.Point(260, 330);
            this.patronymicLabel.Name = "patronymicLabel";
            this.patronymicLabel.Size = new System.Drawing.Size(54, 13);
            this.patronymicLabel.TabIndex = 10;
            this.patronymicLabel.Text = "Отчество";
            // 
            // surnameLabel
            // 
            this.surnameLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.surnameLabel.AutoSize = true;
            this.surnameLabel.Location = new System.Drawing.Point(93, 330);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(56, 13);
            this.surnameLabel.TabIndex = 12;
            this.surnameLabel.Text = "Фамилия";
            // 
            // firstnameBox
            // 
            this.firstnameBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.firstnameBox.Location = new System.Drawing.Point(154, 356);
            this.firstnameBox.Name = "firstnameBox";
            this.firstnameBox.Size = new System.Drawing.Size(95, 20);
            this.firstnameBox.TabIndex = 11;
            // 
            // firstnameLabel
            // 
            this.firstnameLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.firstnameLabel.AutoSize = true;
            this.firstnameLabel.Location = new System.Drawing.Point(179, 330);
            this.firstnameLabel.Name = "firstnameLabel";
            this.firstnameLabel.Size = new System.Drawing.Size(29, 13);
            this.firstnameLabel.TabIndex = 14;
            this.firstnameLabel.Text = "Имя";
            // 
            // patronymicBox
            // 
            this.patronymicBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.patronymicBox.Location = new System.Drawing.Point(253, 356);
            this.patronymicBox.Name = "patronymicBox";
            this.patronymicBox.Size = new System.Drawing.Size(70, 20);
            this.patronymicBox.TabIndex = 13;
            // 
            // dobLabel
            // 
            this.dobLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dobLabel.AutoSize = true;
            this.dobLabel.Location = new System.Drawing.Point(327, 330);
            this.dobLabel.Name = "dobLabel";
            this.dobLabel.Size = new System.Drawing.Size(86, 13);
            this.dobLabel.TabIndex = 16;
            this.dobLabel.Text = "Дата рождения";
            // 
            // docseriesLabel
            // 
            this.docseriesLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.docseriesLabel.AutoSize = true;
            this.docseriesLabel.Location = new System.Drawing.Point(420, 330);
            this.docseriesLabel.Name = "docseriesLabel";
            this.docseriesLabel.Size = new System.Drawing.Size(64, 13);
            this.docseriesLabel.TabIndex = 18;
            this.docseriesLabel.Text = "Док. серия";
            // 
            // docseriesBox
            // 
            this.docseriesBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.docseriesBox.Location = new System.Drawing.Point(424, 356);
            this.docseriesBox.Name = "docseriesBox";
            this.docseriesBox.Size = new System.Drawing.Size(52, 20);
            this.docseriesBox.TabIndex = 17;
            // 
            // docnumberLabel
            // 
            this.docnumberLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.docnumberLabel.AutoSize = true;
            this.docnumberLabel.Location = new System.Drawing.Point(490, 330);
            this.docnumberLabel.Name = "docnumberLabel";
            this.docnumberLabel.Size = new System.Drawing.Size(66, 13);
            this.docnumberLabel.TabIndex = 20;
            this.docnumberLabel.Text = "Док. номер";
            // 
            // docnumberBox
            // 
            this.docnumberBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.docnumberBox.Location = new System.Drawing.Point(492, 356);
            this.docnumberBox.Name = "docnumberBox";
            this.docnumberBox.Size = new System.Drawing.Size(62, 20);
            this.docnumberBox.TabIndex = 19;
            // 
            // positionLabel
            // 
            this.positionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(576, 330);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(65, 13);
            this.positionLabel.TabIndex = 23;
            this.positionLabel.Text = "Должность";
            // 
            // positionBox
            // 
            this.positionBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.positionBox.Location = new System.Drawing.Point(564, 355);
            this.positionBox.Name = "positionBox";
            this.positionBox.Size = new System.Drawing.Size(92, 20);
            this.positionBox.TabIndex = 22;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.refreshButton.Location = new System.Drawing.Point(711, 395);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 24;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // surnameBox
            // 
            this.surnameBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.surnameBox.Location = new System.Drawing.Point(68, 356);
            this.surnameBox.Name = "surnameBox";
            this.surnameBox.Size = new System.Drawing.Size(83, 20);
            this.surnameBox.TabIndex = 25;
            // 
            // showAllButton
            // 
            this.showAllButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.showAllButton.Location = new System.Drawing.Point(638, 10);
            this.showAllButton.Name = "showAllButton";
            this.showAllButton.Size = new System.Drawing.Size(122, 23);
            this.showAllButton.TabIndex = 26;
            this.showAllButton.Text = "Показать всех";
            this.showAllButton.UseVisualStyleBackColor = true;
            // 
            // deptLabel
            // 
            this.deptLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deptLabel.AutoSize = true;
            this.deptLabel.Location = new System.Drawing.Point(708, 330);
            this.deptLabel.Name = "deptLabel";
            this.deptLabel.Size = new System.Drawing.Size(38, 13);
            this.deptLabel.TabIndex = 28;
            this.deptLabel.Text = "Отдел";
            // 
            // deptComboBox
            // 
            this.deptComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deptComboBox.FormattingEnabled = true;
            this.deptComboBox.Location = new System.Drawing.Point(662, 354);
            this.deptComboBox.Name = "deptComboBox";
            this.deptComboBox.Size = new System.Drawing.Size(130, 21);
            this.deptComboBox.TabIndex = 29;
            // 
            // dobBox
            // 
            this.dobBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dobBox.Location = new System.Drawing.Point(329, 355);
            this.dobBox.Name = "dobBox";
            this.dobBox.Size = new System.Drawing.Size(80, 20);
            this.dobBox.TabIndex = 30;
            this.dobBox.Text = "1999-01-30";
            this.toolTip1.SetToolTip(this.dobBox, "Формат даты: YYYY-MM-DD \r\nНапример: 15 января 1999 года \r\nбудет выглядеть следующ" +
        "им \r\nобразом 1999-01-15");
            // 
            // StuffView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.dobBox);
            this.Controls.Add(this.deptComboBox);
            this.Controls.Add(this.deptLabel);
            this.Controls.Add(this.showAllButton);
            this.Controls.Add(this.surnameBox);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.positionBox);
            this.Controls.Add(this.docnumberLabel);
            this.Controls.Add(this.docnumberBox);
            this.Controls.Add(this.docseriesLabel);
            this.Controls.Add(this.docseriesBox);
            this.Controls.Add(this.dobLabel);
            this.Controls.Add(this.firstnameLabel);
            this.Controls.Add(this.patronymicBox);
            this.Controls.Add(this.surnameLabel);
            this.Controls.Add(this.firstnameBox);
            this.Controls.Add(this.patronymicLabel);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.saveButton);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "StuffView";
            this.Text = "StuffView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label patronymicLabel;
        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.TextBox firstnameBox;
        private System.Windows.Forms.Label firstnameLabel;
        private System.Windows.Forms.TextBox patronymicBox;
        private System.Windows.Forms.Label dobLabel;
        private System.Windows.Forms.Label docseriesLabel;
        private System.Windows.Forms.TextBox docseriesBox;
        private System.Windows.Forms.Label docnumberLabel;
        private System.Windows.Forms.TextBox docnumberBox;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.TextBox positionBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox surnameBox;
        private System.Windows.Forms.Button showAllButton;
        private System.Windows.Forms.Label deptLabel;
        private System.Windows.Forms.ComboBox deptComboBox;
        private System.Windows.Forms.TextBox dobBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
namespace DBConnect
{
    partial class MainForm
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
            this.settingsButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.departmentButton = new System.Windows.Forms.Button();
            this.structureButton = new System.Windows.Forms.Button();
            this.stuffEditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Location = new System.Drawing.Point(57, 249);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(141, 23);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.Text = "Настройки соединения";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(57, 290);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(141, 23);
            this.testButton.TabIndex = 1;
            this.testButton.Text = "Тест соединения";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // departmentButton
            // 
            this.departmentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.departmentButton.Location = new System.Drawing.Point(68, 95);
            this.departmentButton.Name = "departmentButton";
            this.departmentButton.Size = new System.Drawing.Size(130, 23);
            this.departmentButton.TabIndex = 4;
            this.departmentButton.Text = "Отделы";
            this.departmentButton.UseVisualStyleBackColor = true;
            // 
            // structureButton
            // 
            this.structureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.structureButton.Location = new System.Drawing.Point(68, 155);
            this.structureButton.Name = "structureButton";
            this.structureButton.Size = new System.Drawing.Size(130, 23);
            this.structureButton.TabIndex = 3;
            this.structureButton.Text = "Структура компании";
            this.structureButton.UseVisualStyleBackColor = true;
            // 
            // stuffEditButton
            // 
            this.stuffEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stuffEditButton.Location = new System.Drawing.Point(68, 28);
            this.stuffEditButton.Name = "stuffEditButton";
            this.stuffEditButton.Size = new System.Drawing.Size(130, 23);
            this.stuffEditButton.TabIndex = 2;
            this.stuffEditButton.Text = "Сотрудники";
            this.stuffEditButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.departmentButton);
            this.Controls.Add(this.structureButton);
            this.Controls.Add(this.stuffEditButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.settingsButton);
            this.MinimumSize = new System.Drawing.Size(300, 500);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button departmentButton;
        private System.Windows.Forms.Button structureButton;
        private System.Windows.Forms.Button stuffEditButton;
    }
}


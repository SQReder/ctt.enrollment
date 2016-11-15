namespace Enrollment.DesktopScanner
{
    internal partial class MainForm
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
            this.ScanButton = new System.Windows.Forms.Button();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.ShowOutputButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanButton
            // 
            this.ScanButton.Location = new System.Drawing.Point(7, 63);
            this.ScanButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(347, 69);
            this.ScanButton.TabIndex = 0;
            this.ScanButton.Text = "Сканировать";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Location = new System.Drawing.Point(7, 27);
            this.CodeTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.Size = new System.Drawing.Size(347, 26);
            this.CodeTextBox.TabIndex = 1;
            this.CodeTextBox.Text = "123456";
            this.CodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ShowOutputButton
            // 
            this.ShowOutputButton.Location = new System.Drawing.Point(19, 176);
            this.ShowOutputButton.Name = "ShowOutputButton";
            this.ShowOutputButton.Size = new System.Drawing.Size(347, 31);
            this.ShowOutputButton.TabIndex = 2;
            this.ShowOutputButton.Text = "Открыть папку со сканами";
            this.ShowOutputButton.UseVisualStyleBackColor = true;
            this.ShowOutputButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ScanButton);
            this.groupBox1.Controls.Add(this.CodeTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 140);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ведите код и нажмите сканировать";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 232);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ShowOutputButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Запись в объединения - сканер";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Button ShowOutputButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


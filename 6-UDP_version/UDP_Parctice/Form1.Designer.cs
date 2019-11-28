namespace UDP_Parctice
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LocalIPAddressTextBox = new System.Windows.Forms.TextBox();
            this.TypeOfProtocolComboBox = new System.Windows.Forms.ComboBox();
            this.LocalIPPortTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureDataBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FarIPPortTextBox = new System.Windows.Forms.TextBox();
            this.FarIPAddressTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ClearReciveDataButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LocalIPAddressTextBox
            // 
            this.LocalIPAddressTextBox.Location = new System.Drawing.Point(109, 55);
            this.LocalIPAddressTextBox.Name = "LocalIPAddressTextBox";
            this.LocalIPAddressTextBox.Size = new System.Drawing.Size(121, 21);
            this.LocalIPAddressTextBox.TabIndex = 0;
            this.LocalIPAddressTextBox.Text = "192.168.1.105";
            this.LocalIPAddressTextBox.TextChanged += new System.EventHandler(this.IPAddressTextBox_TextChanged);
            // 
            // TypeOfProtocolComboBox
            // 
            this.TypeOfProtocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeOfProtocolComboBox.FormattingEnabled = true;
            this.TypeOfProtocolComboBox.Items.AddRange(new object[] {
            "TCP Client",
            "TCP Server",
            "UDP"});
            this.TypeOfProtocolComboBox.Location = new System.Drawing.Point(109, 20);
            this.TypeOfProtocolComboBox.Name = "TypeOfProtocolComboBox";
            this.TypeOfProtocolComboBox.Size = new System.Drawing.Size(121, 20);
            this.TypeOfProtocolComboBox.TabIndex = 1;
            // 
            // LocalIPPortTextBox
            // 
            this.LocalIPPortTextBox.Location = new System.Drawing.Point(109, 93);
            this.LocalIPPortTextBox.Name = "LocalIPPortTextBox";
            this.LocalIPPortTextBox.Size = new System.Drawing.Size(121, 21);
            this.LocalIPPortTextBox.TabIndex = 2;
            this.LocalIPPortTextBox.Text = "5000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "协议类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "本地IP地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "本地端口号";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(271, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // PictureDataBox
            // 
            this.PictureDataBox.Location = new System.Drawing.Point(6, 20);
            this.PictureDataBox.Multiline = true;
            this.PictureDataBox.Name = "PictureDataBox";
            this.PictureDataBox.ReadOnly = true;
            this.PictureDataBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PictureDataBox.Size = new System.Drawing.Size(308, 151);
            this.PictureDataBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "远端端口号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "远端IP地址";
            // 
            // FarIPPortTextBox
            // 
            this.FarIPPortTextBox.Location = new System.Drawing.Point(109, 183);
            this.FarIPPortTextBox.Name = "FarIPPortTextBox";
            this.FarIPPortTextBox.Size = new System.Drawing.Size(121, 21);
            this.FarIPPortTextBox.TabIndex = 9;
            this.FarIPPortTextBox.Text = "8088";
            // 
            // FarIPAddressTextBox
            // 
            this.FarIPAddressTextBox.Location = new System.Drawing.Point(109, 145);
            this.FarIPAddressTextBox.Name = "FarIPAddressTextBox";
            this.FarIPAddressTextBox.Size = new System.Drawing.Size(121, 21);
            this.FarIPAddressTextBox.TabIndex = 8;
            this.FarIPAddressTextBox.Text = "192.168.1.88";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(20, 223);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 12;
            this.ConnectButton.Text = "连接";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ClearReciveDataButton
            // 
            this.ClearReciveDataButton.Location = new System.Drawing.Point(134, 223);
            this.ClearReciveDataButton.Name = "ClearReciveDataButton";
            this.ClearReciveDataButton.Size = new System.Drawing.Size(75, 23);
            this.ClearReciveDataButton.TabIndex = 13;
            this.ClearReciveDataButton.Text = "清空数据区";
            this.ClearReciveDataButton.UseVisualStyleBackColor = true;
            this.ClearReciveDataButton.Click += new System.EventHandler(this.ClearReciveDataButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TypeOfProtocolComboBox);
            this.groupBox1.Controls.Add(this.ClearReciveDataButton);
            this.groupBox1.Controls.Add(this.LocalIPAddressTextBox);
            this.groupBox1.Controls.Add(this.ConnectButton);
            this.groupBox1.Controls.Add(this.LocalIPPortTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FarIPPortTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.FarIPAddressTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 259);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网络设置";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(12, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 177);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "机器人控制区";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(175, 104);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 22);
            this.button4.TabIndex = 22;
            this.button4.Text = "前进";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 104);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 22);
            this.button3.TabIndex = 21;
            this.button3.Text = "前进";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 22);
            this.button2.TabIndex = 20;
            this.button2.Text = "前进";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 22);
            this.button1.TabIndex = 19;
            this.button1.Text = "前进";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PictureDataBox);
            this.groupBox3.Location = new System.Drawing.Point(271, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 177);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "控制数据回显区";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 477);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "UDP Connect By Lai";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox LocalIPAddressTextBox;
        private System.Windows.Forms.ComboBox TypeOfProtocolComboBox;
        private System.Windows.Forms.TextBox LocalIPPortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox PictureDataBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FarIPPortTextBox;
        private System.Windows.Forms.TextBox FarIPAddressTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button ClearReciveDataButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}


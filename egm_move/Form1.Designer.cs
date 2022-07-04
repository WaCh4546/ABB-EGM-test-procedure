namespace egm_move1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.XBar = new System.Windows.Forms.TrackBar();
            this.YBar = new System.Windows.Forms.TrackBar();
            this.ZBar = new System.Windows.Forms.TrackBar();
            this.RollBar = new System.Windows.Forms.TrackBar();
            this.PitchBar = new System.Windows.Forms.TrackBar();
            this.YawBar = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LBar = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.XBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RollBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PitchBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.启动);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(126, 46);
            this.textBox1.TabIndex = 2;
            // 
            // XBar
            // 
            this.XBar.Location = new System.Drawing.Point(45, 30);
            this.XBar.Maximum = 20;
            this.XBar.Minimum = -20;
            this.XBar.Name = "XBar";
            this.XBar.Size = new System.Drawing.Size(297, 45);
            this.XBar.TabIndex = 3;
            this.XBar.ValueChanged += new System.EventHandler(this.X轴运动);
            // 
            // YBar
            // 
            this.YBar.Location = new System.Drawing.Point(47, 81);
            this.YBar.Maximum = 20;
            this.YBar.Minimum = -20;
            this.YBar.Name = "YBar";
            this.YBar.Size = new System.Drawing.Size(295, 45);
            this.YBar.TabIndex = 4;
            this.YBar.ValueChanged += new System.EventHandler(this.Y轴运动);
            // 
            // ZBar
            // 
            this.ZBar.Location = new System.Drawing.Point(47, 132);
            this.ZBar.Maximum = 20;
            this.ZBar.Minimum = -20;
            this.ZBar.Name = "ZBar";
            this.ZBar.Size = new System.Drawing.Size(295, 45);
            this.ZBar.TabIndex = 5;
            this.ZBar.ValueChanged += new System.EventHandler(this.Z轴运动);
            // 
            // RollBar
            // 
            this.RollBar.Location = new System.Drawing.Point(47, 175);
            this.RollBar.Maximum = 5;
            this.RollBar.Minimum = -5;
            this.RollBar.Name = "RollBar";
            this.RollBar.Size = new System.Drawing.Size(295, 45);
            this.RollBar.TabIndex = 6;
            this.RollBar.ValueChanged += new System.EventHandler(this.滚转运动);
            // 
            // PitchBar
            // 
            this.PitchBar.Location = new System.Drawing.Point(45, 226);
            this.PitchBar.Maximum = 5;
            this.PitchBar.Minimum = -5;
            this.PitchBar.Name = "PitchBar";
            this.PitchBar.Size = new System.Drawing.Size(297, 45);
            this.PitchBar.TabIndex = 7;
            this.PitchBar.ValueChanged += new System.EventHandler(this.俯仰运动);
            // 
            // YawBar
            // 
            this.YawBar.Location = new System.Drawing.Point(47, 277);
            this.YawBar.Maximum = 5;
            this.YawBar.Minimum = -5;
            this.YawBar.Name = "YawBar";
            this.YawBar.Size = new System.Drawing.Size(295, 45);
            this.YawBar.TabIndex = 8;
            this.YawBar.ValueChanged += new System.EventHandler(this.偏航运动);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "IRB4600";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.选中IRB4600);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 50);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "IRB6500S";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.选中IRB6650S);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "刹车";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.刹车);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(115, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "工作位置";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.工作位置);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(115, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "停机位置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.停机位置);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "ROLL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "PITCH";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "YAW";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(5, 14);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(114, 155);
            this.textBox2.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(81, 78);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制模型";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(8, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 88);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "控制按钮";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(92, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 78);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "提示信息";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Location = new System.Drawing.Point(236, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(125, 175);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "实时数据";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.LBar);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.XBar);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.YBar);
            this.groupBox5.Controls.Add(this.ZBar);
            this.groupBox5.Controls.Add(this.YawBar);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.PitchBar);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.RollBar);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(14, 193);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(349, 365);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "六自由度控制";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "L";
            // 
            // LBar
            // 
            this.LBar.Location = new System.Drawing.Point(45, 320);
            this.LBar.Maximum = 2000;
            this.LBar.Name = "LBar";
            this.LBar.Size = new System.Drawing.Size(265, 45);
            this.LBar.SmallChange = 5;
            this.LBar.TabIndex = 21;
            this.LBar.ValueChanged += new System.EventHandler(this.加油杆长变化);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.定期刷新状态);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(369, 580);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "飞机控制测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.关闭窗口);
            ((System.ComponentModel.ISupportInitialize)(this.XBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RollBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PitchBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LBar)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TrackBar XBar;
        public System.Windows.Forms.TrackBar YBar;
        public System.Windows.Forms.TrackBar ZBar;
        public System.Windows.Forms.TrackBar RollBar;
        public System.Windows.Forms.TrackBar PitchBar;
        public System.Windows.Forms.TrackBar YawBar;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TrackBar LBar;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer1;
    }
}


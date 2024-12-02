namespace ResultComp
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

        //#region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnQuit = new Button();
            lblRaceName0 = new Label();
            tbxPrgNo = new TextBox();
            tbxKumi = new TextBox();
            lblPrgNo = new Label();
            lblKumi = new Label();
            lblHyphen = new Label();
            btnShow = new Button();
            btnShowPrev = new Button();
            btnShowNext = new Button();
            btnStart = new Button();
            lblLapInterval = new Label();
            lbl2xpoolLength = new Label();
            cbxMonitorEnable = new CheckBox();
            toolTip2 = new ToolTip(components);
            lblPending = new Label();
            SuspendLayout();
            // 
            // btnQuit
            // 
            btnQuit.Location = new Point(2348, -2);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(100, 75);
            btnQuit.TabIndex = 1;
            btnQuit.Text = "終了";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnQuit_Click;
            // 
            // lblRaceName0
            // 
            lblRaceName0.AutoSize = true;
            lblRaceName0.BackColor = SystemColors.Control;
            lblRaceName0.Font = new Font("MS UI Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblRaceName0.Location = new Point(669, 33);
            lblRaceName0.Name = "lblRaceName0";
            lblRaceName0.Size = new Size(265, 54);
            lblRaceName0.TabIndex = 15;
            lblRaceName0.Text = "RaceName";
            // 
            // tbxPrgNo
            // 
            tbxPrgNo.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbxPrgNo.Location = new Point(27, 51);
            tbxPrgNo.Name = "tbxPrgNo";
            tbxPrgNo.Size = new Size(85, 39);
            tbxPrgNo.TabIndex = 16;
            // 
            // tbxKumi
            // 
            tbxKumi.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbxKumi.Location = new Point(150, 51);
            tbxKumi.Name = "tbxKumi";
            tbxKumi.Size = new Size(51, 39);
            tbxKumi.TabIndex = 17;
            tbxKumi.TextChanged += tbxKumi_TextChanged;
            // 
            // lblPrgNo
            // 
            lblPrgNo.AutoSize = true;
            lblPrgNo.Location = new Point(47, 21);
            lblPrgNo.Name = "lblPrgNo";
            lblPrgNo.Size = new Size(46, 32);
            lblPrgNo.TabIndex = 18;
            lblPrgNo.Text = "No";
            // 
            // lblKumi
            // 
            lblKumi.AutoSize = true;
            lblKumi.Location = new Point(152, 21);
            lblKumi.Name = "lblKumi";
            lblKumi.Size = new Size(38, 32);
            lblKumi.TabIndex = 19;
            lblKumi.Text = "組";
            // 
            // lblHyphen
            // 
            lblHyphen.AutoSize = true;
            lblHyphen.Location = new Point(118, 62);
            lblHyphen.Name = "lblHyphen";
            lblHyphen.Size = new Size(24, 32);
            lblHyphen.TabIndex = 20;
            lblHyphen.Text = "-";
            // 
            // btnShow
            // 
            btnShow.Location = new Point(225, 38);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(86, 59);
            btnShow.TabIndex = 21;
            btnShow.Text = "表示";
            btnShow.UseVisualStyleBackColor = true;
            btnShow.Click += btnShow_click;
            // 
            // btnShowPrev
            // 
            btnShowPrev.Location = new Point(338, 41);
            btnShowPrev.Name = "btnShowPrev";
            btnShowPrev.Size = new Size(66, 53);
            btnShowPrev.TabIndex = 22;
            btnShowPrev.Text = "<";
            btnShowPrev.UseVisualStyleBackColor = true;
            btnShowPrev.Click += btnShowPrev_Click;
            // 
            // btnShowNext
            // 
            btnShowNext.Location = new Point(430, 42);
            btnShowNext.Name = "btnShowNext";
            btnShowNext.Size = new Size(61, 52);
            btnShowNext.TabIndex = 23;
            btnShowNext.Text = ">";
            btnShowNext.UseVisualStyleBackColor = true;
            btnShowNext.Click += btnShowNext_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(0, 0);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 31;
            // 
            // lblLapInterval
            // 
            lblLapInterval.AutoSize = true;
            lblLapInterval.Location = new Point(1894, 25);
            lblLapInterval.Name = "lblLapInterval";
            lblLapInterval.Size = new Size(132, 32);
            lblLapInterval.TabIndex = 25;
            lblLapInterval.Text = "lap interval";
            // 
            // lbl2xpoolLength
            // 
            lbl2xpoolLength.AutoSize = true;
            lbl2xpoolLength.Location = new Point(2069, 25);
            lbl2xpoolLength.Name = "lbl2xpoolLength";
            lbl2xpoolLength.Size = new Size(61, 32);
            lbl2xpoolLength.TabIndex = 26;
            lbl2xpoolLength.Text = "50m";
            // 
            // cbxMonitorEnable
            // 
            cbxMonitorEnable.AutoSize = true;
            cbxMonitorEnable.Location = new Point(1661, 22);
            cbxMonitorEnable.Name = "cbxMonitorEnable";
            cbxMonitorEnable.Size = new Size(179, 36);
            cbxMonitorEnable.TabIndex = 27;
            cbxMonitorEnable.Text = "結果取り込み";
            cbxMonitorEnable.UseVisualStyleBackColor = true;
            // 
            // lblPending
            // 
            lblPending.AutoSize = true;
            lblPending.Font = new Font("MS UI Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblPending.ForeColor = Color.Red;
            lblPending.Location = new Point(1494, 11);
            lblPending.Name = "lblPending";
            lblPending.Size = new Size(131, 38);
            lblPending.TabIndex = 30;
            lblPending.Text = "中断中";
            // 
            // Form2
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(2418, 1020);
            Controls.Add(lblPending);
            Controls.Add(cbxMonitorEnable);
            Controls.Add(lbl2xpoolLength);
            Controls.Add(lblLapInterval);
            Controls.Add(btnStart);
            Controls.Add(btnShowNext);
            Controls.Add(btnShowPrev);
            Controls.Add(btnShow);
            Controls.Add(lblHyphen);
            Controls.Add(lblKumi);
            Controls.Add(lblPrgNo);
            Controls.Add(tbxKumi);
            Controls.Add(tbxPrgNo);
            Controls.Add(lblRaceName0);
            Controls.Add(btnQuit);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        //#endregion
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TextBox tbxPrgNo;
        private System.Windows.Forms.TextBox tbxKumi;
        private System.Windows.Forms.Label lblPrgNo;
        private System.Windows.Forms.Label lblKumi;
        private System.Windows.Forms.Label lblHyphen;
        private System.Windows.Forms.Label lblRaceName0;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnShowPrev;
        private System.Windows.Forms.Button btnShowNext;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblLapInterval;
        private System.Windows.Forms.Label lbl2xpoolLength;
        private System.Windows.Forms.CheckBox cbxMonitorEnable;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label lblPending;
    }
    //////////        
}
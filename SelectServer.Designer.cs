namespace ResultComp
{
    partial class SelectServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbxServerName = new TextBox();
            btnConfirm = new Button();
            btnOK = new Button();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // tbxServerName
            // 
            tbxServerName.Location = new Point(263, 16);
            tbxServerName.Name = "tbxServerName";
            tbxServerName.Size = new Size(278, 39);
            tbxServerName.TabIndex = 0;
            tbxServerName.Text = "SERVER";
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(188, 102);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(133, 76);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "接続確認";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(454, 102);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(131, 70);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += this.btnOK_Click;
            // 
            // lblMessage
            // 
            lblMessage.BackColor = SystemColors.Control;
            lblMessage.Location = new Point(193, 212);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(392, 178);
            lblMessage.TabIndex = 3;
            // 
            // SelectServer
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMessage);
            Controls.Add(btnOK);
            Controls.Add(btnConfirm);
            Controls.Add(tbxServerName);
            Name = "SelectServer";
            Text = "サーバー選択";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbxServerName;
        private Button btnConfirm;
        private Button btnOK;
        private Label lblMessage;
    }
}

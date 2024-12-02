namespace ResultComp
{
    partial class SelectEvent
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
            listEvent = new ListBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // listEvent
            // 
            listEvent.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listEvent.FormattingEnabled = true;
            listEvent.ItemHeight = 27;
            listEvent.Location = new Point(37, 31);
            listEvent.Name = "listEvent";
            listEvent.Size = new Size(1737, 760);
            listEvent.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(1496, 938);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(93, 66);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // SelectEvent
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1871, 1074);
            Controls.Add(btnOK);
            Controls.Add(listEvent);
            Name = "SelectEvent";
            Text = "大会選択";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listEvent;
        private Button btnOK;
    }
}
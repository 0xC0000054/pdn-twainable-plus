namespace TwainablePlus
{
    partial class ConfigDialog
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (process != null)
                {
                    process.Dispose();
                }
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
            this.selectSourceCbo = new System.Windows.Forms.ComboBox();
            this.acquireBtn = new System.Windows.Forms.Button();
            this.autoCloseCb = new System.Windows.Forms.CheckBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.selectSourceLabel = new PaintDotNet.HeaderLabel();
            this.SuspendLayout();
            // 
            // selectSourceCbo
            // 
            this.selectSourceCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectSourceCbo.Enabled = false;
            this.selectSourceCbo.FormattingEnabled = true;
            this.selectSourceCbo.Location = new System.Drawing.Point(12, 32);
            this.selectSourceCbo.Name = "selectSourceCbo";
            this.selectSourceCbo.Size = new System.Drawing.Size(176, 21);
            this.selectSourceCbo.TabIndex = 1;
            // 
            // acquireBtn
            // 
            this.acquireBtn.Enabled = false;
            this.acquireBtn.Location = new System.Drawing.Point(12, 77);
            this.acquireBtn.Name = "acquireBtn";
            this.acquireBtn.Size = new System.Drawing.Size(155, 23);
            this.acquireBtn.TabIndex = 3;
            this.acquireBtn.Text = "&Acquire to Clipboard...";
            this.acquireBtn.UseVisualStyleBackColor = true;
            this.acquireBtn.Click += new System.EventHandler(this.acquireBtn_Click);
            // 
            // autoCloseCb
            // 
            this.autoCloseCb.AutoSize = true;
            this.autoCloseCb.Enabled = false;
            this.autoCloseCb.Location = new System.Drawing.Point(12, 106);
            this.autoCloseCb.Name = "autoCloseCb";
            this.autoCloseCb.Size = new System.Drawing.Size(155, 17);
            this.autoCloseCb.TabIndex = 4;
            this.autoCloseCb.Text = "Close &window automatically";
            this.autoCloseCb.UseVisualStyleBackColor = true;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(195, 218);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "&Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // selectSourceLabel
            // 
            this.selectSourceLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.selectSourceLabel.Location = new System.Drawing.Point(12, 12);
            this.selectSourceLabel.Name = "selectSourceLabel";
            this.selectSourceLabel.Size = new System.Drawing.Size(176, 14);
            this.selectSourceLabel.TabIndex = 2;
            this.selectSourceLabel.TabStop = false;
            this.selectSourceLabel.Text = "Select source";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.autoCloseCb);
            this.Controls.Add(this.acquireBtn);
            this.Controls.Add(this.selectSourceLabel);
            this.Controls.Add(this.selectSourceCbo);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ConfigDialog";
            this.Text = "Twainable+";
            this.Controls.SetChildIndex(this.selectSourceCbo, 0);
            this.Controls.SetChildIndex(this.selectSourceLabel, 0);
            this.Controls.SetChildIndex(this.acquireBtn, 0);
            this.Controls.SetChildIndex(this.autoCloseCb, 0);
            this.Controls.SetChildIndex(this.closeBtn, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectSourceCbo;
        private PaintDotNet.HeaderLabel selectSourceLabel;
        private System.Windows.Forms.Button acquireBtn;
        private System.Windows.Forms.CheckBox autoCloseCb;
        private System.Windows.Forms.Button closeBtn;
    }
}
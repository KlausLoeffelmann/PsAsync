namespace WebApiHost
{
    partial class frmWebApiStatusForm
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnStartWebApi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(16, 45);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1108, 782);
            this.txtLog.TabIndex = 0;
            // 
            // btnStartWebApi
            // 
            this.btnStartWebApi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartWebApi.Location = new System.Drawing.Point(1134, 45);
            this.btnStartWebApi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartWebApi.Name = "btnStartWebApi";
            this.btnStartWebApi.Size = new System.Drawing.Size(186, 56);
            this.btnStartWebApi.TabIndex = 1;
            this.btnStartWebApi.Text = "Start WebAPI";
            this.btnStartWebApi.UseVisualStyleBackColor = true;
            this.btnStartWebApi.Click += new System.EventHandler(this.btnStartWebApi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "WebAPI LOG:";
            // 
            // frmWebApiStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 847);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartWebApi);
            this.Controls.Add(this.txtLog);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmWebApiStatusForm";
            this.Text = "SelfHost WebAPI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStartWebApi;
        private System.Windows.Forms.Label label1;
    }
}


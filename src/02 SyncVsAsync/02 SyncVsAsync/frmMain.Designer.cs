namespace SyncVsAsync
{
    partial class frmMain
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
            this.txtPiResult = new System.Windows.Forms.TextBox();
            this.lblCurrentPiFragment = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnPickPath = new System.Windows.Forms.Button();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.lblDigitNo = new System.Windows.Forms.Label();
            this.piCalculationProgress = new System.Windows.Forms.ProgressBar();
            this.btnWriteFileAsync = new System.Windows.Forms.Button();
            this.btnAwaitWriteFile = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.ThrottleTrackBar = new System.Windows.Forms.TrackBar();
            this.btnWriteFileSync = new System.Windows.Forms.Button();
            this.lblTitel = new System.Windows.Forms.Label();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.runOnceTimer = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThrottleTrackBar)).BeginInit();
            this.TableLayoutPanel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPiResult
            // 
            this.txtPiResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPiResult.Location = new System.Drawing.Point(37, 83);
            this.txtPiResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtPiResult.Multiline = true;
            this.txtPiResult.Name = "txtPiResult";
            this.txtPiResult.Size = new System.Drawing.Size(927, 470);
            this.txtPiResult.TabIndex = 10;
            this.txtPiResult.Visible = false;
            // 
            // lblCurrentPiFragment
            // 
            this.lblCurrentPiFragment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentPiFragment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentPiFragment.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPiFragment.Location = new System.Drawing.Point(15, 84);
            this.lblCurrentPiFragment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentPiFragment.Name = "lblCurrentPiFragment";
            this.lblCurrentPiFragment.Size = new System.Drawing.Size(926, 114);
            this.lblCurrentPiFragment.TabIndex = 1;
            this.lblCurrentPiFragment.Text = "3.14159265...";
            this.lblCurrentPiFragment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel1
            // 
            this.TableLayoutPanel1.SetColumnSpan(this.Panel1, 3);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.btnPickPath);
            this.Panel1.Controls.Add(this.txtTargetFolder);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(4, 556);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(956, 175);
            this.Panel1.TabIndex = 4;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 10);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(360, 25);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Path to Folder on USB Thumb Drive:";
            // 
            // btnPickPath
            // 
            this.btnPickPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickPath.Location = new System.Drawing.Point(672, 100);
            this.btnPickPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPickPath.Name = "btnPickPath";
            this.btnPickPath.Size = new System.Drawing.Size(271, 65);
            this.btnPickPath.TabIndex = 1;
            this.btnPickPath.Text = "Pick Path...";
            this.btnPickPath.UseVisualStyleBackColor = true;
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetFolder.Location = new System.Drawing.Point(12, 46);
            this.txtTargetFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(929, 31);
            this.txtTargetFolder.TabIndex = 0;
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.BackColor = System.Drawing.Color.Transparent;
            this.lblElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTime.Location = new System.Drawing.Point(239, 296);
            this.lblElapsedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(493, 46);
            this.lblElapsedTime.TabIndex = 4;
            this.lblElapsedTime.Text = "0:00:00:0";
            this.lblElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDigitNo
            // 
            this.lblDigitNo.Location = new System.Drawing.Point(15, 235);
            this.lblDigitNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDigitNo.Name = "lblDigitNo";
            this.lblDigitNo.Size = new System.Drawing.Size(927, 37);
            this.lblDigitNo.TabIndex = 3;
            this.lblDigitNo.Text = "Calculating #### digit";
            this.lblDigitNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // piCalculationProgress
            // 
            this.piCalculationProgress.Location = new System.Drawing.Point(15, 278);
            this.piCalculationProgress.Margin = new System.Windows.Forms.Padding(4);
            this.piCalculationProgress.Name = "piCalculationProgress";
            this.piCalculationProgress.Size = new System.Drawing.Size(929, 81);
            this.piCalculationProgress.TabIndex = 2;
            // 
            // btnWriteFileAsync
            // 
            this.btnWriteFileAsync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnWriteFileAsync.Location = new System.Drawing.Point(368, 753);
            this.btnWriteFileAsync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnWriteFileAsync.Name = "btnWriteFileAsync";
            this.btnWriteFileAsync.Size = new System.Drawing.Size(271, 62);
            this.btnWriteFileAsync.TabIndex = 2;
            this.btnWriteFileAsync.Text = "Write File Async...";
            this.btnWriteFileAsync.UseVisualStyleBackColor = true;
            this.btnWriteFileAsync.Click += new System.EventHandler(this.btnWriteFileAsync_Click);
            // 
            // btnAwaitWriteFile
            // 
            this.btnAwaitWriteFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAwaitWriteFile.Location = new System.Drawing.Point(682, 753);
            this.btnAwaitWriteFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAwaitWriteFile.Name = "btnAwaitWriteFile";
            this.btnAwaitWriteFile.Size = new System.Drawing.Size(271, 62);
            this.btnAwaitWriteFile.TabIndex = 3;
            this.btnAwaitWriteFile.Text = "Await Write File...";
            this.btnAwaitWriteFile.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(15, 383);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(927, 50);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Throttle:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ThrottleTrackBar
            // 
            this.ThrottleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThrottleTrackBar.Location = new System.Drawing.Point(296, 441);
            this.ThrottleTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.ThrottleTrackBar.Minimum = 1;
            this.ThrottleTrackBar.Name = "ThrottleTrackBar";
            this.ThrottleTrackBar.Size = new System.Drawing.Size(375, 90);
            this.ThrottleTrackBar.TabIndex = 5;
            this.ThrottleTrackBar.Value = 10;
            // 
            // btnWriteFileSync
            // 
            this.btnWriteFileSync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnWriteFileSync.Location = new System.Drawing.Point(32, 753);
            this.btnWriteFileSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnWriteFileSync.Name = "btnWriteFileSync";
            this.btnWriteFileSync.Size = new System.Drawing.Size(271, 62);
            this.btnWriteFileSync.TabIndex = 1;
            this.btnWriteFileSync.Text = "Write File Sync...";
            this.btnWriteFileSync.UseVisualStyleBackColor = true;
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Location = new System.Drawing.Point(11, 25);
            this.lblTitel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(475, 25);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Calculating Pi (3.1415926...) with 100,000 digits:";
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 3;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 292F));
            this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.btnWriteFileAsync, 1, 2);
            this.TableLayoutPanel1.Controls.Add(this.btnAwaitWriteFile, 2, 2);
            this.TableLayoutPanel1.Controls.Add(this.btnWriteFileSync, 0, 2);
            this.TableLayoutPanel1.Controls.Add(this.Panel2, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(20, 22);
            this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 3;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(964, 834);
            this.TableLayoutPanel1.TabIndex = 9;
            // 
            // Panel2
            // 
            this.TableLayoutPanel1.SetColumnSpan(this.Panel2, 3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.ThrottleTrackBar);
            this.Panel2.Controls.Add(this.lblElapsedTime);
            this.Panel2.Controls.Add(this.lblDigitNo);
            this.Panel2.Controls.Add(this.piCalculationProgress);
            this.Panel2.Controls.Add(this.lblCurrentPiFragment);
            this.Panel2.Controls.Add(this.lblTitel);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(4, 4);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(956, 545);
            this.Panel2.TabIndex = 5;
            // 
            // runOnceTimer
            // 
            this.runOnceTimer.Enabled = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 878);
            this.Controls.Add(this.txtPiResult);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Pi Calculate";
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThrottleTrackBar)).EndInit();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtPiResult;
        internal System.Windows.Forms.Label lblCurrentPiFragment;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button btnWriteFileAsync;
        internal System.Windows.Forms.Button btnAwaitWriteFile;
        internal System.Windows.Forms.Button btnWriteFileSync;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TrackBar ThrottleTrackBar;
        internal System.Windows.Forms.Label lblElapsedTime;
        internal System.Windows.Forms.Label lblDigitNo;
        internal System.Windows.Forms.ProgressBar piCalculationProgress;
        internal System.Windows.Forms.Label lblTitel;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnPickPath;
        internal System.Windows.Forms.TextBox txtTargetFolder;
        internal System.Windows.Forms.Timer runOnceTimer;
    }
}


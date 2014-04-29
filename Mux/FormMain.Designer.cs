namespace Mux
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ofdDialog = new System.Windows.Forms.OpenFileDialog();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.chFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilStatus = new System.Windows.Forms.ImageList(this.components);
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ofdDialog
            // 
            this.ofdDialog.AutoUpgradeEnabled = false;
            this.ofdDialog.DereferenceLinks = false;
            // 
            // lvFiles
            // 
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFile});
            this.lvFiles.Location = new System.Drawing.Point(3, 2);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(291, 554);
            this.lvFiles.SmallImageList = this.ilStatus;
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.List;
            // 
            // chFile
            // 
            this.chFile.Text = "File";
            this.chFile.Width = 300;
            // 
            // ilStatus
            // 
            this.ilStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilStatus.ImageStream")));
            this.ilStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.ilStatus.Images.SetKeyName(0, "gear.png");
            this.ilStatus.Images.SetKeyName(1, "btnStart16.png");
            this.ilStatus.Images.SetKeyName(2, "check16.png");
            this.ilStatus.Images.SetKeyName(3, "warning16.png");
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(3, 562);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open ";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Location = new System.Drawing.Point(84, 562);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(300, 2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(657, 554);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 588);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.lvFiles);
            this.Name = "FormMain";
            this.Text = "Mux";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader chFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ImageList ilStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox rtbLog;
        public System.Windows.Forms.OpenFileDialog ofdDialog;
    }
}
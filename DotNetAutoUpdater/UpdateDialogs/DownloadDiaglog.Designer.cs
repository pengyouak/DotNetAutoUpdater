namespace DotNetAutoUpdater.UpdateDialogs
{
    partial class DownloadDiaglog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadDiaglog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnShowList = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lsvUpdateItems = new System.Windows.Forms.ListView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.progressBarCurrent = new System.Windows.Forms.ProgressBar();
            this.lblTotalDownload = new System.Windows.Forms.Label();
            this.lblCurDownload = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSubTitle);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 70);
            this.panel1.TabIndex = 1;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Location = new System.Drawing.Point(99, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(305, 12);
            this.lblSubTitle.TabIndex = 2;
            this.lblSubTitle.Text = "本次更新可能持续数分钟，在这期间您可以做其他的事。";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(77, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(85, 13);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "正在下载更新";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnShowList);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 213);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 43);
            this.panel2.TabIndex = 3;
            // 
            // btnShowList
            // 
            this.btnShowList.Location = new System.Drawing.Point(25, 8);
            this.btnShowList.Name = "btnShowList";
            this.btnShowList.Size = new System.Drawing.Size(75, 23);
            this.btnShowList.TabIndex = 1;
            this.btnShowList.Text = "显示详情";
            this.btnShowList.UseVisualStyleBackColor = true;
            this.btnShowList.Click += new System.EventHandler(this.btnShowList_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(438, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(543, 143);
            this.panel3.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lsvUpdateItems);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(543, 37);
            this.panel5.TabIndex = 13;
            // 
            // lsvUpdateItems
            // 
            this.lsvUpdateItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvUpdateItems.FullRowSelect = true;
            this.lsvUpdateItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvUpdateItems.HideSelection = false;
            this.lsvUpdateItems.Location = new System.Drawing.Point(0, 0);
            this.lsvUpdateItems.Name = "lsvUpdateItems";
            this.lsvUpdateItems.Size = new System.Drawing.Size(543, 37);
            this.lsvUpdateItems.TabIndex = 0;
            this.lsvUpdateItems.UseCompatibleStateImageBehavior = false;
            this.lsvUpdateItems.View = System.Windows.Forms.View.Details;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.progressBarTotal);
            this.panel4.Controls.Add(this.progressBarCurrent);
            this.panel4.Controls.Add(this.lblTotalDownload);
            this.panel4.Controls.Add(this.lblCurDownload);
            this.panel4.Controls.Add(this.lblVersion);
            this.panel4.Controls.Add(this.lblFileName);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(543, 106);
            this.panel4.TabIndex = 12;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(18, 84);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(508, 12);
            this.progressBarTotal.Step = 1;
            this.progressBarTotal.TabIndex = 16;
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(18, 45);
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Size = new System.Drawing.Size(508, 12);
            this.progressBarCurrent.Step = 1;
            this.progressBarCurrent.TabIndex = 17;
            // 
            // lblTotalDownload
            // 
            this.lblTotalDownload.AutoSize = true;
            this.lblTotalDownload.Location = new System.Drawing.Point(17, 69);
            this.lblTotalDownload.Name = "lblTotalDownload";
            this.lblTotalDownload.Size = new System.Drawing.Size(95, 12);
            this.lblTotalDownload.TabIndex = 14;
            this.lblTotalDownload.Text = "Total Progress:";
            // 
            // lblCurDownload
            // 
            this.lblCurDownload.AutoSize = true;
            this.lblCurDownload.Location = new System.Drawing.Point(17, 30);
            this.lblCurDownload.Name = "lblCurDownload";
            this.lblCurDownload.Size = new System.Drawing.Size(77, 12);
            this.lblCurDownload.TabIndex = 15;
            this.lblCurDownload.Text = "Downloading:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(206, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(113, 12);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "1.0.0.0 to 2.0.0.0";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(18, 9);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(155, 12);
            this.lblFileName.TabIndex = 13;
            this.lblFileName.Text = "Name:  KnightsWarrior.exe";
            // 
            // DownloadDiaglog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 256);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(559, 570);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(559, 258);
            this.Name = "DownloadDiaglog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DownloadDiaglog";
            this.Load += new System.EventHandler(this.DownloadDiaglog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnShowList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ListView lsvUpdateItems;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar progressBarTotal;
        private System.Windows.Forms.ProgressBar progressBarCurrent;
        private System.Windows.Forms.Label lblTotalDownload;
        private System.Windows.Forms.Label lblCurDownload;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblFileName;
    }
}
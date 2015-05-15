namespace LivelogsPlayerStatParser
{
    partial class Menu
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
            this.txtLogUri = new System.Windows.Forms.TextBox();
            this.btnParseLog = new System.Windows.Forms.Button();
            this.lblParseLog = new System.Windows.Forms.Label();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.lstLogsTracked = new System.Windows.Forms.ListView();
            this.colLogID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemoveLogs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLogUri
            // 
            this.txtLogUri.Location = new System.Drawing.Point(56, 12);
            this.txtLogUri.Name = "txtLogUri";
            this.txtLogUri.Size = new System.Drawing.Size(273, 20);
            this.txtLogUri.TabIndex = 0;
            // 
            // btnParseLog
            // 
            this.btnParseLog.Location = new System.Drawing.Point(335, 12);
            this.btnParseLog.Name = "btnParseLog";
            this.btnParseLog.Size = new System.Drawing.Size(66, 21);
            this.btnParseLog.TabIndex = 1;
            this.btnParseLog.Text = "Parse Log";
            this.btnParseLog.UseVisualStyleBackColor = true;
            this.btnParseLog.Click += new System.EventHandler(this.btnParseLog_Click);
            // 
            // lblParseLog
            // 
            this.lblParseLog.AutoSize = true;
            this.lblParseLog.Location = new System.Drawing.Point(0, 15);
            this.lblParseLog.Name = "lblParseLog";
            this.lblParseLog.Size = new System.Drawing.Size(50, 13);
            this.lblParseLog.TabIndex = 2;
            this.lblParseLog.Text = "Log URL";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(298, 221);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(94, 26);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "Export to Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(215, 221);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(77, 25);
            this.btnClearHistory.TabIndex = 4;
            this.btnClearHistory.Text = "Clear History";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // lstLogsTracked
            // 
            this.lstLogsTracked.CheckBoxes = true;
            this.lstLogsTracked.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogID,
            this.colDate,
            this.colMap});
            this.lstLogsTracked.GridLines = true;
            this.lstLogsTracked.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLogsTracked.Location = new System.Drawing.Point(3, 38);
            this.lstLogsTracked.Name = "lstLogsTracked";
            this.lstLogsTracked.Size = new System.Drawing.Size(398, 178);
            this.lstLogsTracked.TabIndex = 5;
            this.lstLogsTracked.UseCompatibleStateImageBehavior = false;
            this.lstLogsTracked.View = System.Windows.Forms.View.Details;
            // 
            // colLogID
            // 
            this.colLogID.Text = "LogID";
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            // 
            // colMap
            // 
            this.colMap.Text = "Map";
            this.colMap.Width = 273;
            // 
            // btnRemoveLogs
            // 
            this.btnRemoveLogs.Location = new System.Drawing.Point(107, 221);
            this.btnRemoveLogs.Name = "btnRemoveLogs";
            this.btnRemoveLogs.Size = new System.Drawing.Size(102, 25);
            this.btnRemoveLogs.TabIndex = 6;
            this.btnRemoveLogs.Text = "Remove Selected";
            this.btnRemoveLogs.UseVisualStyleBackColor = true;
            this.btnRemoveLogs.Click += new System.EventHandler(this.btnRemoveLogs_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 259);
            this.Controls.Add(this.btnRemoveLogs);
            this.Controls.Add(this.lstLogsTracked);
            this.Controls.Add(this.btnClearHistory);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.lblParseLog);
            this.Controls.Add(this.btnParseLog);
            this.Controls.Add(this.txtLogUri);
            this.Name = "Menu";
            this.Text = "Livelogs Log Parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogUri;
        private System.Windows.Forms.Button btnParseLog;
        private System.Windows.Forms.Label lblParseLog;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.ListView lstLogsTracked;
        private System.Windows.Forms.ColumnHeader colLogID;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colMap;
        private System.Windows.Forms.Button btnRemoveLogs;
    }
}


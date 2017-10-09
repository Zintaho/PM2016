namespace PM2016
{
    partial class PM2016
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolCPU = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolMem = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnKill = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolProcess,
            this.toolCPU,
            this.toolMem});
            this.statusBar.Location = new System.Drawing.Point(0, 539);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(584, 22);
            this.statusBar.TabIndex = 1;
            // 
            // toolProcess
            // 
            this.toolProcess.Name = "toolProcess";
            this.toolProcess.Size = new System.Drawing.Size(99, 17);
            this.toolProcess.Text = Properties.Resources.textProcess + " ___ " + Properties.Resources.textGae;

            // 
            // toolCPU
            // 
            this.toolCPU.Name = "toolCPU";
            this.toolCPU.Size = new System.Drawing.Size(112, 17);
            this.toolCPU.Text = Properties.Resources.textCpuUsage + " : ___%";
            // 
            // toolMem
            // 
            this.toolMem.Name = "toolMem";
            this.toolMem.Size = new System.Drawing.Size(125, 17);
            this.toolMem.Text = Properties.Resources.textMemUsage + " : ___%";

            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(505, 513);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(80, 23);
            this.btnKill.TabIndex = 2;
            this.btnKill.Text = Properties.Resources.textKillProcess;
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.BtnKill_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // ctMenu
            // 
            this.ctMenu.Name = "ctMenu";
            this.ctMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // listView
            // 
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPid,
            this.chMem});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(584, 507);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
            // 
            // chName
            // 
            this.chName.Text = Properties.Resources.textListName;
            this.chName.Width = 284;
            // 
            // chPid
            // 
            this.chPid.Text = Properties.Resources.textListPID;
            this.chPid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chPid.Width = 50;
            // 
            // chMem
            // 
            this.chMem.Text = Properties.Resources.textListMem;
            this.chMem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chMem.Width = 230;
            // 
            // PM2016
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.statusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PM2016";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "PM2016";
            this.Load += new System.EventHandler(this.PM2016_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolProcess;
        private System.Windows.Forms.ToolStripStatusLabel toolCPU;
        private System.Windows.Forms.ToolStripStatusLabel toolMem;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip ctMenu;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chPid;
        private System.Windows.Forms.ColumnHeader chMem;
        private System.Windows.Forms.ListView listView;
    }
}



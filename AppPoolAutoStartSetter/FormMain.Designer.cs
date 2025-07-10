namespace AppPoolAutoStartSetter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDisableAll = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listViewAppPool = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAutoStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDisableAll);
            this.panel1.Controls.Add(this.buttonLoad);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 56);
            this.panel1.TabIndex = 0;
            // 
            // buttonDisableAll
            // 
            this.buttonDisableAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisableAll.Location = new System.Drawing.Point(275, 3);
            this.buttonDisableAll.Name = "buttonDisableAll";
            this.buttonDisableAll.Size = new System.Drawing.Size(109, 50);
            this.buttonDisableAll.TabIndex = 0;
            this.buttonDisableAll.Text = "すべて無効";
            this.buttonDisableAll.UseVisualStyleBackColor = true;
            this.buttonDisableAll.Click += new System.EventHandler(this.buttonDisableAll_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(3, 3);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(109, 50);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "読込";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listViewAppPool);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(387, 400);
            this.panel2.TabIndex = 1;
            // 
            // listViewAppPool
            // 
            this.listViewAppPool.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderAutoStart});
            this.listViewAppPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAppPool.HideSelection = false;
            this.listViewAppPool.Location = new System.Drawing.Point(0, 0);
            this.listViewAppPool.Name = "listViewAppPool";
            this.listViewAppPool.Size = new System.Drawing.Size(387, 400);
            this.listViewAppPool.TabIndex = 0;
            this.listViewAppPool.UseCompatibleStateImageBehavior = false;
            this.listViewAppPool.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "アプリケーションプール名";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderAutoStart
            // 
            this.columnHeaderAutoStart.Text = "autoStart";
            this.columnHeaderAutoStart.Width = 108;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 456);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(403, 495);
            this.Name = "FormMain";
            this.Text = "AppPool AutoStart Setter";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonDisableAll;
        private System.Windows.Forms.ListView listViewAppPool;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderAutoStart;
    }
}
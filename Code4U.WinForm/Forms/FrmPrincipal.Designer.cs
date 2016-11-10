namespace Code4U.WinForm
{
    partial class FrmPrincipal
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
            this.trvProject = new System.Windows.Forms.TreeView();
            this.ptgProperties = new System.Windows.Forms.PropertyGrid();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.loadModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadModelFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadModelFromDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadModelFromAssembly = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvProject
            // 
            this.trvProject.HideSelection = false;
            this.trvProject.Location = new System.Drawing.Point(12, 27);
            this.trvProject.Name = "trvProject";
            this.trvProject.Size = new System.Drawing.Size(424, 549);
            this.trvProject.TabIndex = 15;
            this.trvProject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvProject_AfterSelect);
            // 
            // ptgProperties
            // 
            this.ptgProperties.Location = new System.Drawing.Point(442, 27);
            this.ptgProperties.Name = "ptgProperties";
            this.ptgProperties.Size = new System.Drawing.Size(391, 549);
            this.ptgProperties.TabIndex = 16;
            this.ptgProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ptgProperties_PropertyValueChanged);
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadModelToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(845, 24);
            this.menuPrincipal.TabIndex = 17;
            this.menuPrincipal.Text = "menuPrincipal";
            // 
            // loadModelToolStripMenuItem
            // 
            this.loadModelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoadModelFromFile,
            this.menuLoadModelFromDatabase,
            this.menuLoadModelFromAssembly});
            this.loadModelToolStripMenuItem.Name = "loadModelToolStripMenuItem";
            this.loadModelToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.loadModelToolStripMenuItem.Text = "&Load Model";
            // 
            // menuLoadModelFromFile
            // 
            this.menuLoadModelFromFile.Name = "menuLoadModelFromFile";
            this.menuLoadModelFromFile.Size = new System.Drawing.Size(156, 22);
            this.menuLoadModelFromFile.Text = "From &File";
            // 
            // menuLoadModelFromDatabase
            // 
            this.menuLoadModelFromDatabase.Name = "menuLoadModelFromDatabase";
            this.menuLoadModelFromDatabase.Size = new System.Drawing.Size(156, 22);
            this.menuLoadModelFromDatabase.Text = "From &Database";
            this.menuLoadModelFromDatabase.Click += new System.EventHandler(this.menuLoadModelFromDatabase_Click);
            // 
            // menuLoadModelFromAssembly
            // 
            this.menuLoadModelFromAssembly.Name = "menuLoadModelFromAssembly";
            this.menuLoadModelFromAssembly.Size = new System.Drawing.Size(156, 22);
            this.menuLoadModelFromAssembly.Text = "From &Assembly";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 588);
            this.Controls.Add(this.ptgProperties);
            this.Controls.Add(this.trvProject);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPrincipal";
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvProject;
        private System.Windows.Forms.PropertyGrid ptgProperties;
        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem loadModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuLoadModelFromFile;
        private System.Windows.Forms.ToolStripMenuItem menuLoadModelFromDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuLoadModelFromAssembly;
    }
}
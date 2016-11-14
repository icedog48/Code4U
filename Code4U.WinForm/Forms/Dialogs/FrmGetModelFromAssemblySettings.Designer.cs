namespace Code4U.WinForm
{
    partial class FrmGetModelFromAssemblySettings
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
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAssemblyFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Location = new System.Drawing.Point(256, 70);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(92, 25);
            this.btnGenerateCode.TabIndex = 3;
            this.btnGenerateCode.Text = "Ok";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(114, 44);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(234, 20);
            this.txtNamespace.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Namespace to filter";
            // 
            // txtAssemblyFile
            // 
            this.txtAssemblyFile.Location = new System.Drawing.Point(114, 18);
            this.txtAssemblyFile.Name = "txtAssemblyFile";
            this.txtAssemblyFile.Size = new System.Drawing.Size(234, 20);
            this.txtAssemblyFile.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Assembly File";
            // 
            // FrmGetModelFromAssemblySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 109);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAssemblyFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerateCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGetModelFromAssemblySettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assembly Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAssemblyFile;
        private System.Windows.Forms.Label label2;
    }
}



namespace CommonCmpLib.Test
{
    partial class TestForm
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
            this.btn_xmlPath = new System.Windows.Forms.Button();
            this.lbl_xmlPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_xmlPath
            // 
            this.btn_xmlPath.Location = new System.Drawing.Point(12, 24);
            this.btn_xmlPath.Name = "btn_xmlPath";
            this.btn_xmlPath.Size = new System.Drawing.Size(117, 23);
            this.btn_xmlPath.TabIndex = 0;
            this.btn_xmlPath.Text = "Choose File Xml";
            this.btn_xmlPath.UseVisualStyleBackColor = true;
            this.btn_xmlPath.Click += new System.EventHandler(this.btn_xmlPath_Click);
            // 
            // lbl_xmlPath
            // 
            this.lbl_xmlPath.AutoSize = true;
            this.lbl_xmlPath.Location = new System.Drawing.Point(151, 29);
            this.lbl_xmlPath.Name = "lbl_xmlPath";
            this.lbl_xmlPath.Size = new System.Drawing.Size(49, 13);
            this.lbl_xmlPath.TabIndex = 1;
            this.lbl_xmlPath.Text = "Xml Path";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_xmlPath);
            this.Controls.Add(this.btn_xmlPath);
            this.Name = "TestForm";
            this.Text = "CommonCmpLib.Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_xmlPath;
        private System.Windows.Forms.Label lbl_xmlPath;
    }
}


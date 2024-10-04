
using CommonCmpLib_Test;

namespace CommonCmpLib_Test
{
    partial class TestFull
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabExcel = new System.Windows.Forms.TabPage();
            this.excelUC1 = new CommonCmpLib_Test.ExcelUC();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.jsonXmlConverterUC1 = new CommonCmpLib_Test.JsonXmlConverterUC();
            this.tabControl1.SuspendLayout();
            this.tabExcel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabExcel);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(825, 472);
            this.tabControl1.TabIndex = 0;
            // 
            // tabExcel
            // 
            this.tabExcel.Controls.Add(this.excelUC1);
            this.tabExcel.Location = new System.Drawing.Point(4, 22);
            this.tabExcel.Name = "tabExcel";
            this.tabExcel.Padding = new System.Windows.Forms.Padding(3);
            this.tabExcel.Size = new System.Drawing.Size(817, 446);
            this.tabExcel.TabIndex = 0;
            this.tabExcel.Text = "Excel to Xml";
            this.tabExcel.UseVisualStyleBackColor = true;
            // 
            // excelUC1
            // 
            this.excelUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excelUC1.Location = new System.Drawing.Point(3, 3);
            this.excelUC1.Name = "excelUC1";
            this.excelUC1.Size = new System.Drawing.Size(811, 440);
            this.excelUC1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.jsonXmlConverterUC1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(817, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Json <=> Xml";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // jsonXmlConverterUC1
            // 
            this.jsonXmlConverterUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonXmlConverterUC1.Location = new System.Drawing.Point(3, 3);
            this.jsonXmlConverterUC1.Name = "jsonXmlConverterUC1";
            this.jsonXmlConverterUC1.Size = new System.Drawing.Size(811, 440);
            this.jsonXmlConverterUC1.TabIndex = 0;
            // 
            // TestFull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 472);
            this.Controls.Add(this.tabControl1);
            this.Name = "TestFull";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabExcel.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabExcel;
        private System.Windows.Forms.TabPage tabPage2;
        private ExcelUC excelUC1;
        private CommonCmpLib_Test.JsonXmlConverterUC jsonXmlConverterUC1;
    }
}


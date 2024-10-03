
namespace CommonCmpLib_Test
{
    partial class ExcelUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Excel_Path = new System.Windows.Forms.Button();
            this.btn_XmlFolder = new System.Windows.Forms.Button();
            this.lbl_ExcelPath = new System.Windows.Forms.Label();
            this.lbl_xmlFolder = new System.Windows.Forms.Label();
            this.btnExcelToXml = new System.Windows.Forms.Button();
            this.btn_CreateTemplate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxt_SheetNames = new System.Windows.Forms.RichTextBox();
            this.rtxt_Log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_Excel_Path
            // 
            this.btn_Excel_Path.Location = new System.Drawing.Point(3, 3);
            this.btn_Excel_Path.Name = "btn_Excel_Path";
            this.btn_Excel_Path.Size = new System.Drawing.Size(75, 23);
            this.btn_Excel_Path.TabIndex = 0;
            this.btn_Excel_Path.Text = "Excel Path";
            this.btn_Excel_Path.UseVisualStyleBackColor = true;
            this.btn_Excel_Path.Click += new System.EventHandler(this.btn_Excel_Path_Click);
            // 
            // btn_XmlFolder
            // 
            this.btn_XmlFolder.Location = new System.Drawing.Point(3, 32);
            this.btn_XmlFolder.Name = "btn_XmlFolder";
            this.btn_XmlFolder.Size = new System.Drawing.Size(75, 23);
            this.btn_XmlFolder.TabIndex = 1;
            this.btn_XmlFolder.Text = "XML Folder";
            this.btn_XmlFolder.UseVisualStyleBackColor = true;
            this.btn_XmlFolder.Click += new System.EventHandler(this.btn_XmlFolder_Click);
            // 
            // lbl_ExcelPath
            // 
            this.lbl_ExcelPath.AutoSize = true;
            this.lbl_ExcelPath.Location = new System.Drawing.Point(113, 12);
            this.lbl_ExcelPath.Name = "lbl_ExcelPath";
            this.lbl_ExcelPath.Size = new System.Drawing.Size(58, 13);
            this.lbl_ExcelPath.TabIndex = 2;
            this.lbl_ExcelPath.Text = "Excel Path";
            // 
            // lbl_xmlFolder
            // 
            this.lbl_xmlFolder.AutoSize = true;
            this.lbl_xmlFolder.Location = new System.Drawing.Point(113, 37);
            this.lbl_xmlFolder.Name = "lbl_xmlFolder";
            this.lbl_xmlFolder.Size = new System.Drawing.Size(56, 13);
            this.lbl_xmlFolder.TabIndex = 2;
            this.lbl_xmlFolder.Text = "Xml Folder";
            // 
            // btnExcelToXml
            // 
            this.btnExcelToXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelToXml.Location = new System.Drawing.Point(706, 3);
            this.btnExcelToXml.Name = "btnExcelToXml";
            this.btnExcelToXml.Size = new System.Drawing.Size(75, 43);
            this.btnExcelToXml.TabIndex = 3;
            this.btnExcelToXml.Text = "Convert";
            this.btnExcelToXml.UseVisualStyleBackColor = true;
            this.btnExcelToXml.Click += new System.EventHandler(this.BtnExcelToXml_Click);
            // 
            // btn_CreateTemplate
            // 
            this.btn_CreateTemplate.Location = new System.Drawing.Point(4, 62);
            this.btn_CreateTemplate.Name = "btn_CreateTemplate";
            this.btn_CreateTemplate.Size = new System.Drawing.Size(75, 34);
            this.btn_CreateTemplate.TabIndex = 4;
            this.btn_CreateTemplate.Text = "Create New Template";
            this.btn_CreateTemplate.UseVisualStyleBackColor = true;
            this.btn_CreateTemplate.Click += new System.EventHandler(this.btn_CreateTemplate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Sheet Name";
            // 
            // rtxt_SheetNames
            // 
            this.rtxt_SheetNames.Location = new System.Drawing.Point(4, 130);
            this.rtxt_SheetNames.Name = "rtxt_SheetNames";
            this.rtxt_SheetNames.Size = new System.Drawing.Size(139, 116);
            this.rtxt_SheetNames.TabIndex = 7;
            this.rtxt_SheetNames.Text = "";
            // 
            // rtxt_Log
            // 
            this.rtxt_Log.Location = new System.Drawing.Point(301, 130);
            this.rtxt_Log.Name = "rtxt_Log";
            this.rtxt_Log.Size = new System.Drawing.Size(466, 323);
            this.rtxt_Log.TabIndex = 8;
            this.rtxt_Log.Text = "";
            // 
            // ExcelUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtxt_Log);
            this.Controls.Add(this.rtxt_SheetNames);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_CreateTemplate);
            this.Controls.Add(this.btnExcelToXml);
            this.Controls.Add(this.lbl_xmlFolder);
            this.Controls.Add(this.lbl_ExcelPath);
            this.Controls.Add(this.btn_XmlFolder);
            this.Controls.Add(this.btn_Excel_Path);
            this.Name = "ExcelUC";
            this.Size = new System.Drawing.Size(784, 472);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Excel_Path;
        private System.Windows.Forms.Button btn_XmlFolder;
        private System.Windows.Forms.Label lbl_ExcelPath;
        private System.Windows.Forms.Label lbl_xmlFolder;
        private System.Windows.Forms.Button btnExcelToXml;
        private System.Windows.Forms.Button btn_CreateTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxt_SheetNames;
        private System.Windows.Forms.RichTextBox rtxt_Log;
    }
}

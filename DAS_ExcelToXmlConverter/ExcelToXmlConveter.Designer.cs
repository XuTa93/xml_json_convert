
namespace ExcelToXmlConverter
{
    partial class ExcelToXmlConveter
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
            this.btn_ExcelToXml = new System.Windows.Forms.Button();
            this.lbl_xmlFolder = new System.Windows.Forms.Label();
            this.lbl_ExcelPath = new System.Windows.Forms.Label();
            this.btn_XmlFolder = new System.Windows.Forms.Button();
            this.btn_Excel_Path = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ExcelToXml
            // 
            this.btn_ExcelToXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ExcelToXml.Enabled = false;
            this.btn_ExcelToXml.Location = new System.Drawing.Point(712, 8);
            this.btn_ExcelToXml.Name = "btn_ExcelToXml";
            this.btn_ExcelToXml.Size = new System.Drawing.Size(75, 25);
            this.btn_ExcelToXml.TabIndex = 8;
            this.btn_ExcelToXml.Text = "Convert";
            this.btn_ExcelToXml.UseVisualStyleBackColor = true;
            this.btn_ExcelToXml.Click += new System.EventHandler(this.btn_ExcelToXml_Click);
            // 
            // lbl_xmlFolder
            // 
            this.lbl_xmlFolder.AutoSize = true;
            this.lbl_xmlFolder.Location = new System.Drawing.Point(143, 43);
            this.lbl_xmlFolder.Name = "lbl_xmlFolder";
            this.lbl_xmlFolder.Size = new System.Drawing.Size(10, 13);
            this.lbl_xmlFolder.TabIndex = 6;
            this.lbl_xmlFolder.Text = "-";
            this.lbl_xmlFolder.TextChanged += new System.EventHandler(this.lbl_ExcelPath_TextChanged);
            // 
            // lbl_ExcelPath
            // 
            this.lbl_ExcelPath.AutoSize = true;
            this.lbl_ExcelPath.Location = new System.Drawing.Point(143, 14);
            this.lbl_ExcelPath.Name = "lbl_ExcelPath";
            this.lbl_ExcelPath.Size = new System.Drawing.Size(10, 13);
            this.lbl_ExcelPath.TabIndex = 7;
            this.lbl_ExcelPath.Text = "-";
            this.lbl_ExcelPath.TextChanged += new System.EventHandler(this.lbl_ExcelPath_TextChanged);
            // 
            // btn_XmlFolder
            // 
            this.btn_XmlFolder.Location = new System.Drawing.Point(12, 37);
            this.btn_XmlFolder.Name = "btn_XmlFolder";
            this.btn_XmlFolder.Size = new System.Drawing.Size(125, 25);
            this.btn_XmlFolder.TabIndex = 5;
            this.btn_XmlFolder.Text = "Select Output Folder";
            this.btn_XmlFolder.UseVisualStyleBackColor = true;
            this.btn_XmlFolder.Click += new System.EventHandler(this.btn_XmlFolder_Click);
            // 
            // btn_Excel_Path
            // 
            this.btn_Excel_Path.Location = new System.Drawing.Point(12, 8);
            this.btn_Excel_Path.Name = "btn_Excel_Path";
            this.btn_Excel_Path.Size = new System.Drawing.Size(125, 25);
            this.btn_Excel_Path.TabIndex = 4;
            this.btn_Excel_Path.Text = "Select Excel Path";
            this.btn_Excel_Path.UseVisualStyleBackColor = true;
            this.btn_Excel_Path.Click += new System.EventHandler(this.btn_Excel_Path_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.btn_ExcelToXml);
            this.panel1.Controls.Add(this.btn_Excel_Path);
            this.panel1.Controls.Add(this.lbl_xmlFolder);
            this.panel1.Controls.Add(this.btn_XmlFolder);
            this.panel1.Controls.Add(this.lbl_ExcelPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 70);
            this.panel1.TabIndex = 9;
            // 
            // rtb_Log
            // 
            this.rtb_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Log.Enabled = false;
            this.rtb_Log.Location = new System.Drawing.Point(5, 18);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.ReadOnly = true;
            this.rtb_Log.Size = new System.Drawing.Size(780, 351);
            this.rtb_Log.TabIndex = 10;
            this.rtb_Log.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rtb_Log);
            this.groupBox1.Location = new System.Drawing.Point(5, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(790, 374);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "History";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Clear.Location = new System.Drawing.Point(712, 37);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 25);
            this.btn_Clear.TabIndex = 9;
            this.btn_Clear.Text = "Clear Log";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // ExcelToXmlConveter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ExcelToXmlConveter";
            this.Text = "Excel To Xml Converter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ExcelToXml;
        private System.Windows.Forms.Label lbl_xmlFolder;
        private System.Windows.Forms.Label lbl_ExcelPath;
        private System.Windows.Forms.Button btn_XmlFolder;
        private System.Windows.Forms.Button btn_Excel_Path;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Clear;
    }
}


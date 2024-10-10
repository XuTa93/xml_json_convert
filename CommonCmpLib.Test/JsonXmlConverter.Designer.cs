
using JsonXmlConverter_Tool;

namespace JsonXmlConverter_Tool
{
    partial class JsonXmlConverter
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
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Covert = new System.Windows.Forms.Button();
            this.btn_File_Path = new System.Windows.Forms.Button();
            this.lbl_xmlFolder = new System.Windows.Forms.Label();
            this.btn_XmlFolder = new System.Windows.Forms.Button();
            this.lbl_FilePath = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_DataCollectionPlan1 = new System.Windows.Forms.RadioButton();
            this.rbtn_EventTrigger = new System.Windows.Forms.RadioButton();
            this.rbtn_Trace = new System.Windows.Forms.RadioButton();
            this.rbtn_Parameter = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_EventRequest = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtn_Xml = new System.Windows.Forms.RadioButton();
            this.rbtn_Json = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.rtb_SelectedFiles = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.btn_Covert);
            this.panel1.Controls.Add(this.btn_File_Path);
            this.panel1.Controls.Add(this.lbl_xmlFolder);
            this.panel1.Controls.Add(this.btn_XmlFolder);
            this.panel1.Controls.Add(this.lbl_FilePath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 70);
            this.panel1.TabIndex = 10;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Clear.Location = new System.Drawing.Point(760, 37);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 25);
            this.btn_Clear.TabIndex = 9;
            this.btn_Clear.Text = "Clear Log";
            this.btn_Clear.UseVisualStyleBackColor = true;
            // 
            // btn_Covert
            // 
            this.btn_Covert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Covert.Enabled = false;
            this.btn_Covert.Location = new System.Drawing.Point(760, 8);
            this.btn_Covert.Name = "btn_Covert";
            this.btn_Covert.Size = new System.Drawing.Size(75, 25);
            this.btn_Covert.TabIndex = 8;
            this.btn_Covert.Text = "Convert";
            this.btn_Covert.UseVisualStyleBackColor = true;
            this.btn_Covert.Click += new System.EventHandler(this.btn_Convert_Click);
            // 
            // btn_File_Path
            // 
            this.btn_File_Path.Location = new System.Drawing.Point(12, 8);
            this.btn_File_Path.Name = "btn_File_Path";
            this.btn_File_Path.Size = new System.Drawing.Size(125, 25);
            this.btn_File_Path.TabIndex = 4;
            this.btn_File_Path.Text = "Select Convert Files";
            this.btn_File_Path.UseVisualStyleBackColor = true;
            this.btn_File_Path.Click += new System.EventHandler(this.btn_File_Path_Click);
            // 
            // lbl_xmlFolder
            // 
            this.lbl_xmlFolder.AutoSize = true;
            this.lbl_xmlFolder.Location = new System.Drawing.Point(143, 43);
            this.lbl_xmlFolder.Name = "lbl_xmlFolder";
            this.lbl_xmlFolder.Size = new System.Drawing.Size(10, 13);
            this.lbl_xmlFolder.TabIndex = 6;
            this.lbl_xmlFolder.Text = "-";
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
            // lbl_FilePath
            // 
            this.lbl_FilePath.AutoSize = true;
            this.lbl_FilePath.Location = new System.Drawing.Point(143, 14);
            this.lbl_FilePath.Name = "lbl_FilePath";
            this.lbl_FilePath.Size = new System.Drawing.Size(10, 13);
            this.lbl_FilePath.TabIndex = 7;
            this.lbl_FilePath.Text = "-";
            this.lbl_FilePath.TextChanged += new System.EventHandler(this.lbl_ExcelPath_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 181);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 205);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Convert Type";
            // 
            // rbtn_DataCollectionPlan1
            // 
            this.rbtn_DataCollectionPlan1.AutoSize = true;
            this.rbtn_DataCollectionPlan1.Location = new System.Drawing.Point(3, 143);
            this.rbtn_DataCollectionPlan1.Name = "rbtn_DataCollectionPlan1";
            this.rbtn_DataCollectionPlan1.Size = new System.Drawing.Size(130, 19);
            this.rbtn_DataCollectionPlan1.TabIndex = 6;
            this.rbtn_DataCollectionPlan1.Text = "DataCollectionPlan";
            this.rbtn_DataCollectionPlan1.UseVisualStyleBackColor = true;
            // 
            // rbtn_EventTrigger
            // 
            this.rbtn_EventTrigger.AutoSize = true;
            this.rbtn_EventTrigger.Location = new System.Drawing.Point(3, 73);
            this.rbtn_EventTrigger.Name = "rbtn_EventTrigger";
            this.rbtn_EventTrigger.Size = new System.Drawing.Size(94, 19);
            this.rbtn_EventTrigger.TabIndex = 5;
            this.rbtn_EventTrigger.Text = "EventTrigger";
            this.rbtn_EventTrigger.UseVisualStyleBackColor = true;
            // 
            // rbtn_Trace
            // 
            this.rbtn_Trace.AutoSize = true;
            this.rbtn_Trace.Location = new System.Drawing.Point(3, 38);
            this.rbtn_Trace.Name = "rbtn_Trace";
            this.rbtn_Trace.Size = new System.Drawing.Size(102, 19);
            this.rbtn_Trace.TabIndex = 4;
            this.rbtn_Trace.Text = "TraceRequest";
            this.rbtn_Trace.UseVisualStyleBackColor = true;
            // 
            // rbtn_Parameter
            // 
            this.rbtn_Parameter.AutoSize = true;
            this.rbtn_Parameter.Checked = true;
            this.rbtn_Parameter.Location = new System.Drawing.Point(3, 3);
            this.rbtn_Parameter.Name = "rbtn_Parameter";
            this.rbtn_Parameter.Size = new System.Drawing.Size(83, 19);
            this.rbtn_Parameter.TabIndex = 3;
            this.rbtn_Parameter.Text = "Parameter";
            this.rbtn_Parameter.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 97);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Type";
            // 
            // rbtn_EventRequest
            // 
            this.rbtn_EventRequest.AutoSize = true;
            this.rbtn_EventRequest.Location = new System.Drawing.Point(3, 108);
            this.rbtn_EventRequest.Name = "rbtn_EventRequest";
            this.rbtn_EventRequest.Size = new System.Drawing.Size(101, 19);
            this.rbtn_EventRequest.TabIndex = 7;
            this.rbtn_EventRequest.Text = "EventRequest";
            this.rbtn_EventRequest.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.rtb_SelectedFiles);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(168, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(167, 309);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Files";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rbtn_DataCollectionPlan1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.rbtn_EventRequest, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.rbtn_Parameter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbtn_Trace, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rbtn_EventTrigger, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(141, 176);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.rbtn_Json, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rbtn_Xml, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(138, 66);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // rbtn_Xml
            // 
            this.rbtn_Xml.AutoSize = true;
            this.rbtn_Xml.Checked = true;
            this.rbtn_Xml.Location = new System.Drawing.Point(3, 3);
            this.rbtn_Xml.Name = "rbtn_Xml";
            this.rbtn_Xml.Size = new System.Drawing.Size(104, 19);
            this.rbtn_Xml.TabIndex = 4;
            this.rbtn_Xml.TabStop = true;
            this.rbtn_Xml.Text = "*.xml => *.json";
            this.rbtn_Xml.UseVisualStyleBackColor = true;
            // 
            // rbtn_Json
            // 
            this.rbtn_Json.AutoSize = true;
            this.rbtn_Json.Location = new System.Drawing.Point(3, 38);
            this.rbtn_Json.Name = "rbtn_Json";
            this.rbtn_Json.Size = new System.Drawing.Size(104, 19);
            this.rbtn_Json.TabIndex = 5;
            this.rbtn_Json.Text = "*.json => *.xml";
            this.rbtn_Json.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rtb_Log);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(341, 77);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox4.Size = new System.Drawing.Size(498, 309);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "History";
            // 
            // rtb_Log
            // 
            this.rtb_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Log.Enabled = false;
            this.rtb_Log.Location = new System.Drawing.Point(5, 19);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.ReadOnly = true;
            this.rtb_Log.Size = new System.Drawing.Size(488, 285);
            this.rtb_Log.TabIndex = 10;
            this.rtb_Log.Text = "";
            // 
            // rtb_SelectedFiles
            // 
            this.rtb_SelectedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SelectedFiles.Location = new System.Drawing.Point(5, 19);
            this.rtb_SelectedFiles.Name = "rtb_SelectedFiles";
            this.rtb_SelectedFiles.ReadOnly = true;
            this.rtb_SelectedFiles.Size = new System.Drawing.Size(157, 285);
            this.rtb_SelectedFiles.TabIndex = 11;
            this.rtb_SelectedFiles.Text = "";
            // 
            // JsonXmlConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 394);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "JsonXmlConverter";
            this.Text = "Json Xml Converter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Covert;
        private System.Windows.Forms.Button btn_File_Path;
        private System.Windows.Forms.Label lbl_xmlFolder;
        private System.Windows.Forms.Button btn_XmlFolder;
        private System.Windows.Forms.Label lbl_FilePath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtn_EventRequest;
        private System.Windows.Forms.RadioButton rbtn_DataCollectionPlan1;
        private System.Windows.Forms.RadioButton rbtn_EventTrigger;
        private System.Windows.Forms.RadioButton rbtn_Trace;
        private System.Windows.Forms.RadioButton rbtn_Parameter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton rbtn_Json;
        private System.Windows.Forms.RadioButton rbtn_Xml;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.RichTextBox rtb_SelectedFiles;
    }
}


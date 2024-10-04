
namespace CommonCmpLib_Test
{
    partial class JsonXmlConverterUC
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
            this.btn_FilePath = new System.Windows.Forms.Button();
            this.rbtn_Xml = new System.Windows.Forms.RadioButton();
            this.rbtn_Json = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_FilePath = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxt_Log = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_Trace = new System.Windows.Forms.RadioButton();
            this.rbtn_DataCollectionPlan = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.rbtn_Parameter = new System.Windows.Forms.RadioButton();
            this.btn_Convert = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_FilePath
            // 
            this.btn_FilePath.BackColor = System.Drawing.SystemColors.Control;
            this.btn_FilePath.Location = new System.Drawing.Point(3, 3);
            this.btn_FilePath.Name = "btn_FilePath";
            this.btn_FilePath.Size = new System.Drawing.Size(99, 23);
            this.btn_FilePath.TabIndex = 2;
            this.btn_FilePath.Text = "Choose File";
            this.btn_FilePath.UseVisualStyleBackColor = false;
            this.btn_FilePath.Click += new System.EventHandler(this.btn_FilePath_Click);
            // 
            // rbtn_Xml
            // 
            this.rbtn_Xml.AutoSize = true;
            this.rbtn_Xml.Checked = true;
            this.rbtn_Xml.Location = new System.Drawing.Point(7, 19);
            this.rbtn_Xml.Name = "rbtn_Xml";
            this.rbtn_Xml.Size = new System.Drawing.Size(107, 20);
            this.rbtn_Xml.TabIndex = 3;
            this.rbtn_Xml.TabStop = true;
            this.rbtn_Xml.Text = "*.xml => *.json";
            this.rbtn_Xml.UseVisualStyleBackColor = true;
            // 
            // rbtn_Json
            // 
            this.rbtn_Json.AutoSize = true;
            this.rbtn_Json.Location = new System.Drawing.Point(125, 19);
            this.rbtn_Json.Name = "rbtn_Json";
            this.rbtn_Json.Size = new System.Drawing.Size(107, 20);
            this.rbtn_Json.TabIndex = 4;
            this.rbtn_Json.Text = "*.json => *.xml";
            this.rbtn_Json.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_Xml);
            this.groupBox1.Controls.Add(this.rbtn_Json);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 44);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Format File";
            // 
            // lbl_FilePath
            // 
            this.lbl_FilePath.AutoSize = true;
            this.lbl_FilePath.Location = new System.Drawing.Point(118, 8);
            this.lbl_FilePath.Name = "lbl_FilePath";
            this.lbl_FilePath.Size = new System.Drawing.Size(48, 13);
            this.lbl_FilePath.TabIndex = 6;
            this.lbl_FilePath.Text = "File Path";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rtxt_Log);
            this.groupBox2.Location = new System.Drawing.Point(9, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(821, 408);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Histrory";
            // 
            // rtxt_Log
            // 
            this.rtxt_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt_Log.Location = new System.Drawing.Point(3, 16);
            this.rtxt_Log.Name = "rtxt_Log";
            this.rtxt_Log.Size = new System.Drawing.Size(815, 389);
            this.rtxt_Log.TabIndex = 10;
            this.rtxt_Log.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Controls.Add(this.rbtn_DataCollectionPlan);
            this.groupBox3.Controls.Add(this.rbtn_Trace);
            this.groupBox3.Controls.Add(this.rbtn_Parameter);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(262, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(451, 44);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Name";
            // 
            // rbtn_Trace
            // 
            this.rbtn_Trace.AutoSize = true;
            this.rbtn_Trace.Checked = true;
            this.rbtn_Trace.Location = new System.Drawing.Point(118, 19);
            this.rbtn_Trace.Name = "rbtn_Trace";
            this.rbtn_Trace.Size = new System.Drawing.Size(62, 20);
            this.rbtn_Trace.TabIndex = 4;
            this.rbtn_Trace.TabStop = true;
            this.rbtn_Trace.Text = "Trace";
            this.rbtn_Trace.UseVisualStyleBackColor = true;
            // 
            // rbtn_DataCollectionPlan
            // 
            this.rbtn_DataCollectionPlan.AutoSize = true;
            this.rbtn_DataCollectionPlan.Checked = true;
            this.rbtn_DataCollectionPlan.Location = new System.Drawing.Point(207, 19);
            this.rbtn_DataCollectionPlan.Name = "rbtn_DataCollectionPlan";
            this.rbtn_DataCollectionPlan.Size = new System.Drawing.Size(60, 20);
            this.rbtn_DataCollectionPlan.TabIndex = 5;
            this.rbtn_DataCollectionPlan.TabStop = true;
            this.rbtn_DataCollectionPlan.Text = "Event";
            this.rbtn_DataCollectionPlan.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(302, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(141, 20);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "DataCollectionPlan";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // rbtn_Parameter
            // 
            this.rbtn_Parameter.AutoSize = true;
            this.rbtn_Parameter.Checked = true;
            this.rbtn_Parameter.Location = new System.Drawing.Point(6, 19);
            this.rbtn_Parameter.Name = "rbtn_Parameter";
            this.rbtn_Parameter.Size = new System.Drawing.Size(89, 20);
            this.rbtn_Parameter.TabIndex = 3;
            this.rbtn_Parameter.TabStop = true;
            this.rbtn_Parameter.Text = "Parameter";
            this.rbtn_Parameter.UseVisualStyleBackColor = true;
            // 
            // btn_Convert
            // 
            this.btn_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Convert.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Convert.Location = new System.Drawing.Point(728, 3);
            this.btn_Convert.Name = "btn_Convert";
            this.btn_Convert.Size = new System.Drawing.Size(99, 23);
            this.btn_Convert.TabIndex = 9;
            this.btn_Convert.Text = "Convert";
            this.btn_Convert.UseVisualStyleBackColor = false;
            this.btn_Convert.Click += new System.EventHandler(this.btn_Convert_Click);
            // 
            // JsonXmlConverterUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Convert);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lbl_FilePath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_FilePath);
            this.Name = "JsonXmlConverterUC";
            this.Size = new System.Drawing.Size(833, 493);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_FilePath;
        private System.Windows.Forms.RadioButton rbtn_Xml;
        private System.Windows.Forms.RadioButton rbtn_Json;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_FilePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxt_Log;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton rbtn_DataCollectionPlan;
        private System.Windows.Forms.RadioButton rbtn_Trace;
        private System.Windows.Forms.RadioButton rbtn_Parameter;
        private System.Windows.Forms.Button btn_Convert;
    }
}

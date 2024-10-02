
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
            this.tableLayoutPanel_Base = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Before = new System.Windows.Forms.TextBox();
            this.txt_After = new System.Windows.Forms.TextBox();
            this.lbl_JsonPath = new System.Windows.Forms.Label();
            this.btn_JsonPath = new System.Windows.Forms.Button();
            this.tableLayoutPanel_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_xmlPath
            // 
            this.btn_xmlPath.Location = new System.Drawing.Point(12, 4);
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
            this.lbl_xmlPath.Location = new System.Drawing.Point(151, 9);
            this.lbl_xmlPath.Name = "lbl_xmlPath";
            this.lbl_xmlPath.Size = new System.Drawing.Size(49, 13);
            this.lbl_xmlPath.TabIndex = 1;
            this.lbl_xmlPath.Text = "Xml Path";
            // 
            // tableLayoutPanel_Base
            // 
            this.tableLayoutPanel_Base.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_Base.ColumnCount = 2;
            this.tableLayoutPanel_Base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Base.Controls.Add(this.txt_After, 1, 0);
            this.tableLayoutPanel_Base.Controls.Add(this.txt_Before, 0, 0);
            this.tableLayoutPanel_Base.Location = new System.Drawing.Point(2, 62);
            this.tableLayoutPanel_Base.Name = "tableLayoutPanel_Base";
            this.tableLayoutPanel_Base.RowCount = 1;
            this.tableLayoutPanel_Base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Base.Size = new System.Drawing.Size(864, 428);
            this.tableLayoutPanel_Base.TabIndex = 2;
            // 
            // txt_Before
            // 
            this.txt_Before.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Before.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Before.Location = new System.Drawing.Point(3, 3);
            this.txt_Before.Multiline = true;
            this.txt_Before.Name = "txt_Before";
            this.txt_Before.Size = new System.Drawing.Size(426, 422);
            this.txt_Before.TabIndex = 0;
            // 
            // txt_After
            // 
            this.txt_After.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_After.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_After.Location = new System.Drawing.Point(435, 3);
            this.txt_After.Multiline = true;
            this.txt_After.Name = "txt_After";
            this.txt_After.Size = new System.Drawing.Size(426, 422);
            this.txt_After.TabIndex = 1;
            // 
            // lbl_JsonPath
            // 
            this.lbl_JsonPath.AutoSize = true;
            this.lbl_JsonPath.Location = new System.Drawing.Point(151, 38);
            this.lbl_JsonPath.Name = "lbl_JsonPath";
            this.lbl_JsonPath.Size = new System.Drawing.Size(54, 13);
            this.lbl_JsonPath.TabIndex = 4;
            this.lbl_JsonPath.Text = "Json Path";
            // 
            // btn_JsonPath
            // 
            this.btn_JsonPath.Location = new System.Drawing.Point(12, 33);
            this.btn_JsonPath.Name = "btn_JsonPath";
            this.btn_JsonPath.Size = new System.Drawing.Size(117, 23);
            this.btn_JsonPath.TabIndex = 3;
            this.btn_JsonPath.Text = "Choose File Json";
            this.btn_JsonPath.UseVisualStyleBackColor = true;
            this.btn_JsonPath.Click += new System.EventHandler(this.btn_JsonPath_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 493);
            this.Controls.Add(this.lbl_JsonPath);
            this.Controls.Add(this.btn_JsonPath);
            this.Controls.Add(this.tableLayoutPanel_Base);
            this.Controls.Add(this.lbl_xmlPath);
            this.Controls.Add(this.btn_xmlPath);
            this.Name = "TestForm";
            this.Text = "CommonCmpLib.Test";
            this.TopMost = true;
            this.tableLayoutPanel_Base.ResumeLayout(false);
            this.tableLayoutPanel_Base.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_xmlPath;
        private System.Windows.Forms.Label lbl_xmlPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Base;
        private System.Windows.Forms.TextBox txt_After;
        private System.Windows.Forms.TextBox txt_Before;
        private System.Windows.Forms.Label lbl_JsonPath;
        private System.Windows.Forms.Button btn_JsonPath;
    }
}


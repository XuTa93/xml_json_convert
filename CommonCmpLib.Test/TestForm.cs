using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonCmpLib.Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void btn_xmlPath_Click(object sender, EventArgs e)
        {
            string strBeforContent;
            // Create an OpenFileDialog object
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the default directory to the root folder of the application
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Set filter options for the file dialog (e.g., XML files only)
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false; // Only allow selecting one file at a time

                // Show the dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string filePath = openFileDialog.FileName;

                    // Assign the file path to the Label (lbl_xmlPath)
                    lbl_xmlPath.Text = filePath;

                    //Source File
                    strBeforContent = File.ReadAllText(filePath);
                    txt_Before.Text = strBeforContent;

                    //read xmlfile
                    txt_After.Text =(CommonCmpLib.ConvertXmlToJson_Parameter(filePath, ""));

                }
            }
        }

        private void btn_JsonPath_Click(object sender, EventArgs e)
        {
            string strBeforContent;
            // Create an OpenFileDialog object
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the default directory to the root folder of the application
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Set filter options for the file dialog (e.g., XML files only)
                openFileDialog.Filter = "Json files (*.json)|*.json| (*.js)|*.js| All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false; // Only allow selecting one file at a time

                // Show the dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string filePath = openFileDialog.FileName;

                    // Assign the file path to the Label (lbl_xmlPath)
                    lbl_xmlPath.Text = filePath;

                    //Source File
                    strBeforContent = File.ReadAllText(filePath);
                    txt_Before.Text = strBeforContent;

                    //read xmlfile
                    txt_After.Text =(CommonCmpLib.ConvertJsonToXml_Parameter(filePath, ""));

                }
            }
        }
    }
}

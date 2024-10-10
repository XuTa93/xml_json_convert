using JsonXmlConveter;
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

namespace JsonXmlConverter_Tool
{
    public partial class JsonXmlConverter : Form
    {
        string[] m_strFilePaths;
        string[] m_strFileNames;
        public JsonXmlConverter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens a file dialog to select either an XML or JSON file based on the selected radio button.
        /// </summary>
        private void btn_File_Path_Click(object objSender, EventArgs objEvt)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog objFileDlg = new OpenFileDialog();

            // Allow the selection of multiple files
            objFileDlg.Multiselect = true;

            // Set the filter and title based on the selected radio button
            if (rbtn_Xml.Checked == true)
            {
                objFileDlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"; // Filter for XML files and all files
                objFileDlg.Title = "Select an XML File"; // Title for XML file selection
            }
            else
            {
                objFileDlg.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"; // Filter for JSON files and all files
                objFileDlg.Title = "Select a JSON File"; // Title for JSON file selection
            }

            // Show the dialog and check if files are selected
            if (objFileDlg.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file paths
                m_strFilePaths = objFileDlg.FileNames;
                m_strFileNames = objFileDlg.SafeFileNames;

                // Display each selected file path in a message box
                rtb_SelectedFiles.Clear();
                foreach (string strFile in m_strFileNames)
                {
                    rtb_SelectedFiles.AppendText(strFile + Environment.NewLine);
                }

                //Auto Get Folder Output
                if (lbl_xmlFolder.Text == "-")
                {
                    lbl_xmlFolder.Text = Path.GetDirectoryName(objFileDlg.FileName);
                }

                lbl_FilePath.Text = Path.GetDirectoryName(objFileDlg.FileName);
            }
        }

        /// <summary>
        /// Verify Convert Button
        /// </summary>
        private void lbl_ExcelPath_TextChanged(object sender, EventArgs e)
        {
            if ((lbl_FilePath.Text != "-") && (lbl_xmlFolder.Text != "-"))
            {
                btn_Covert.Enabled = true;
            }
            else
            {
                btn_Covert.Enabled = false;
            }
        }

        /// <summary>
        /// Checks if the selected file exists and proceeds with the conversion logic.
        /// </summary>
        private void btn_Convert_Click(object objSender, EventArgs objEvt)
        {
            // Declare variables at the beginning
            string strFilePath;
            // Assign value
            strFilePath = lbl_FilePath.Text;

            // Check if the file exists
            if (File.Exists(strFilePath) == false)
            {
                MessageBox.Show("The selected file does not exist. Please check the file path.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the file does not exist
            }
            else
            {
                // Check which radio button is checked and call the corresponding method
                if (rbtn_Parameter.Checked == true)
                {
                    HandleParameterFile(strFilePath);
                }
                else if (rbtn_Trace.Checked == true)
                {
                    HandleTraceFile();
                }
                else if (rbtn_DataCollectionPlan1.Checked == true)
                {
                    HandleEventFile();
                }
                else if (rbtn_DataCollectionPlan1.Checked == true)
                {
                    HandleDataCollectionPlanFile();
                }

            }
        }

        /// <summary>
        /// Choose Folder Output
        /// </summary>
        private void btn_XmlFolder_Click(object objSender, EventArgs objEvt)
        {
            using (FolderBrowserDialog objFolder = new FolderBrowserDialog())
            {
                objFolder.Description = "Select a Folder";
                objFolder.ShowNewFolderButton = true; // Allow user to create new folders if necessary
                                                      // Set the default folder path to the project's root folder
                if (objFolder.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected folder
                    lbl_xmlFolder.Text = objFolder.SelectedPath;

                    // Display the folder path in the label
                }
            }
        }

        /// <summary>
        /// Handles the logic for the Parameter selection.
        /// </summary>
        private void HandleParameterFile(string x_strFilePath)
        {
            string strExtesionFile;
            string strTimestamp;
            ConvertResult objResult;
            // Get the current timestamp for logging
            strTimestamp = DateTime.Now.ToString("HH:mm:ss");

            // Check if the selected file format is XML
            if (rbtn_Xml.Checked == true)
            {
                // If XML is selected, change the file extension to ".json"
                strExtesionFile = Path.ChangeExtension(x_strFilePath, ".json");

                // Convert the XML file to JSON format
                objResult = JsonXmlConveter.Convert.ConvertXmlToJson_Parameter(x_strFilePath, strExtesionFile);
            }
            else
            {
                // If not XML, change the file extension to ".xml"
                strExtesionFile = Path.ChangeExtension(x_strFilePath, ".xml");

                // Convert the JSON file to XML format
                objResult = JsonXmlConveter.Convert.ConvertJsonToXml_Parameter(x_strFilePath, strExtesionFile);
            }

            // Log the result with the status and message
            rtb_Log.AppendText($"{strTimestamp} - {objResult.Message}{Environment.NewLine}");

        }

        /// <summary>
        /// Handles the logic for the Trace selection.
        /// </summary>
        private void HandleTraceFile()
        {

        }

        /// <summary>
        /// Handles the logic for the Event selection.
        /// </summary>
        private void HandleEventFile()
        {

        }

        /// <summary>
        /// Handles the logic for the Data Collection Plan selection.
        /// </summary>
        private void HandleDataCollectionPlanFile()
        {

        }

    }
}

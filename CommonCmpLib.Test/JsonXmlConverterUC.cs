using System;
using System.IO;
using System.Windows.Forms;
using CommonCmpLib;

namespace CommonCmpLib_Test
{
    public partial class JsonXmlConverterUC : UserControl
    {
        public JsonXmlConverterUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens a file dialog to select either an XML or JSON file based on the selected radio button.
        /// </summary>
        private void btn_FilePath_Click(object objSender, EventArgs objEvt)
        {
            // Declare the OpenFileDialog object
            using (OpenFileDialog objOpenFileDialog = new OpenFileDialog())
            {
                // Set the filter and title based on the selected radio button
                if (rbtn_Xml.Checked == true)
                {
                    objOpenFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"; // Filter for XML files and all files
                    objOpenFileDialog.Title = "Select an XML File"; // Title for XML file selection
                }
                else
                {
                    objOpenFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"; // Filter for JSON files and all files
                    objOpenFileDialog.Title = "Select a JSON File"; // Title for JSON file selection
                }

                // Show the dialog and check if the user selected a file
                if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected file and display it in the label
                    lbl_FilePath.Text = objOpenFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// Checks if the selected file exists and proceeds with the conversion logic.
        /// </summary>
        private void   btn_Convert_Click(object objSender, EventArgs objEvt)
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
                else if (rbtn_DataCollectionPlan.Checked == true)
                {
                    HandleEventFile();
                }
                else if (rbtn_DataCollectionPlan.Checked == true)
                {
                    HandleDataCollectionPlanFile();
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
            JsonXmlConvertResult objResult;
            // Get the current timestamp for logging
            strTimestamp = DateTime.Now.ToString("HH:mm:ss");

            // Check if the selected file format is XML
            if (rbtn_Xml.Checked == true)
            {
                // If XML is selected, change the file extension to ".json"
                strExtesionFile = Path.ChangeExtension(x_strFilePath, ".json");

                // Convert the XML file to JSON format
                objResult = Common.ConvertXmlToJson_Parameter(x_strFilePath, strExtesionFile);
            }
            else
            {
                // If not XML, change the file extension to ".xml"
                strExtesionFile = Path.ChangeExtension(x_strFilePath, ".xml");

                // Convert the JSON file to XML format
                objResult = Common.ConvertJsonToXml_Parameter(x_strFilePath, strExtesionFile);
            }

            // Log the result with the status and message
            rtxt_Log.AppendText($"{strTimestamp} - {objResult.Message}{Environment.NewLine}");

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

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
        string m_strInputFolder;
        string m_strOutFolder;
        string[] m_strFilePaths;
        string[] m_strFileNames;
        DataType m_selectedType;

        string GetTime
        {
            get
            {
                return " - " + DateTime.Now.ToString() + " : ";
            }
        }
    
        public JsonXmlConverter()
        {
            InitializeComponent();
            m_selectedType = DataType.Parameter;
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
                m_strInputFolder = Path.GetDirectoryName(objFileDlg.FileName);

                //Auto Get Folder Output
                if (lbl_xmlFolder.Text == "-")
                {
                    m_strOutFolder = m_strInputFolder;
                    lbl_xmlFolder.Text = m_strInputFolder;
                }

               
                lbl_FilePath.Text = m_strInputFolder;
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
            bool bVefify;
            //Validate Convert Type
            bVefify =  ValidateConvertType(m_strFilePaths);
            if (bVefify == false)
            {
                return;
            }

            foreach (string strFilePath in m_strFilePaths)
            {
                // Check which radio button is checked and call the corresponding method
                if (rbtn_Parameter.Checked == true)
                {
                    HandleParameterFile(strFilePath);
                }
                else if (rbtn_TraceRequest.Checked == true)
                {
                    HandleTraceRequestFile(strFilePath);
                }
                else if (rbtn_EventTrigger.Checked == true)
                {
                    HandleEventFile();
                }
                else if (rbtn_EventRequest.Checked == true)
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
        /// Validates the file types in the given paths against the selected convert type.
        /// </summary>
        private bool ValidateConvertType(string[] x_strFilePaths)
        {
            DialogResult objMbResutl;
            DataType datatType;
            string strResult = string.Empty;
            string strFileName;
            bool bIsSuccess = true;

            foreach (string strFilePath in x_strFilePaths)
            {
                // Get the file name from the file path
                strFileName = Path.GetFileName(strFilePath);
                // Validate the file type
                datatType = JsonXmlConveter.Convert.ValidateType(strFilePath);

                // Check if the file type matches the selected convert type
                if (datatType != m_selectedType)
                {
                    bIsSuccess = false;
                    strResult += $"Error: {strFileName} is {datatType} Type" + Environment.NewLine;
                }
            }

            // If there is a type mismatch, prompt the user to continue or not
            if (bIsSuccess == false)
            {
                objMbResutl = MessageBox.Show(this,
                    strResult + "\r\n Do you want to continue?",
                    "Convert Type Error",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                // If the user selects "Yes", return true to continue
                if (objMbResutl == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    // If the user selects "No", return false
                    return false;
                }
            }

            // Return true if all file types match
            return true;
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
        /// Change Extension file to xml
        /// </summary>
        /// <returns></returns>
        private string GetXmlFile(string x_XmlFileName)
        {
            return Path.Combine(m_strInputFolder, Path.ChangeExtension(x_XmlFileName, ".xml"));
        }

        /// <summary>
        /// Change Extension file to Json
        /// </summary>
        /// <returns></returns>
        private string GetJsonFile(string x_XmlFileName)
        {
            return Path.Combine(m_strInputFolder, Path.ChangeExtension(x_XmlFileName, ".json"));
        }

        /// <summary>
        /// Handles the logic for the Parameter selection.
        /// </summary>
        private void HandleParameterFile(string x_strFileName)
        {
            string strXmlPath;
            string strJsonPath;
            ConvertResult objResult;
     
            strXmlPath = GetXmlFile(x_strFileName);
            strJsonPath = GetJsonFile(x_strFileName);

            // Check if the selected file format is XML
            if (rbtn_Xml.Checked == true)
            {
                // Convert the XML file to JSON format
                objResult = JsonXmlConveter.Convert.ConvertXmlToJson_Parameter(strXmlPath, strJsonPath);
                Log(objResult);
            }
            else
            {
                // Convert the JSON file to XML format
                objResult = JsonXmlConveter.Convert.ConvertJsonToXml(strJsonPath, strXmlPath);
                Log(objResult);
            }
        }

        /// <summary>
        /// Handles the logic for the Trace selection.
        /// </summary>
        private void HandleTraceRequestFile(string x_strFileName)
        {
            string strXmlPath;
            string strJsonPath;
            ConvertResult objResult;

            strXmlPath = GetXmlFile(x_strFileName);
            strJsonPath = GetJsonFile(x_strFileName);

            // Check if the selected file format is XML
            if (rbtn_Xml.Checked == true)
            {
                // Convert the XML file to JSON format
                objResult = JsonXmlConveter.Convert.ConvertXmlToJson_TraceRequest(strXmlPath, strJsonPath);
                Log(objResult);
            }
            else
            {
                // Convert the JSON file to XML format
                objResult = JsonXmlConveter.Convert.ConvertJsonToXml(strJsonPath, strXmlPath);
                Log(objResult);
            }
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

        /// <summary>
        /// Log History
        /// </summary>
        void Log(ConvertResult x_objResult)
        {
            if (x_objResult.IsSuccess == true)
            {
                SuccessedLog(x_objResult.Message);
            }
            else
            {
                ErrorLog(x_objResult.Message);
            }
        }

        /// <summary>
        /// Successfully Log
        /// </summary>
        void SuccessedLog(string x_strMsg)
        {
            rtb_Log.SelectionColor = Color.DarkGreen;
            AddLog(x_strMsg);
        }

        /// <summary>
        /// Error Log
        /// </summary>
        void ErrorLog(string x_strMsg)
        {
            rtb_Log.SelectionColor = Color.Red;
            AddLog(x_strMsg);
        }

        /// <summary>
        /// Add Log with time
        /// </summary>
        void AddLog(string x_strMsg)
        {
            rtb_Log.SelectionStart = rtb_Log.Text.Length;
            rtb_Log.Text.TrimEnd();
            rtb_Log.AppendText(GetTime + x_strMsg + System.Environment.NewLine);
            rtb_Log.ScrollToCaret();
        }

        /// <summary>
        /// Clear History Log
        /// </summary>
        private void btn_Clear_Click(object objSender, EventArgs objEvt)
        {
            rtb_Log.Clear();
        }

        private void ConvertType_CheckedChanged(object objSender, EventArgs objEvt)
        {
            DataType selectedType;
            RadioButton objRadioButton;
            bool bIsValid;
            // Check if objSender is a RadioButton

            objRadioButton = objSender as RadioButton;

            if (objRadioButton != null)
            {
                // Try to convert RadioButton's Text to the ConvertType enum
                
                bIsValid = Enum.TryParse(objRadioButton.Text, out selectedType);

                if (bIsValid == true)
                {
                    // If conversion is successful, assign the value to m_selectedType
                    m_selectedType = selectedType;
                }
                else
                {
                    // If conversion fails, set m_selectedType to Unknown
                    m_selectedType = DataType.Unknown;
                }
            }
            else
            {
                m_selectedType = DataType.Unknown;
            }
        }
    }

}

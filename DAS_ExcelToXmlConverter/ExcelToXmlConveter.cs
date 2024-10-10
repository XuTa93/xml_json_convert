using CommonCmpLib;
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

namespace DAS_ExcelToXmlConverter
{
    public partial class ExcelToXmlConveter : Form
    {
        string GetTime
        {
            get
            {
                return " - " + DateTime.Now.ToString() + " : ";
            }
        }

        public ExcelToXmlConveter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Verify Convert Button
        /// </summary>
        private void lbl_ExcelPath_TextChanged(object objSender, EventArgs objEvt)
        {
            if ((lbl_ExcelPath.Text != "-") && (lbl_xmlFolder.Text != "-"))
            {
                btn_ExcelToXml.Enabled = true;
            }
            else
            {
                btn_ExcelToXml.Enabled = false;
            }
        }

        /// <summary>
        /// Choose Excel File
        /// </summary>
        private void btn_Excel_Path_Click(object objSender, EventArgs objEvt)
        {
            using (OpenFileDialog objDialog = new OpenFileDialog())
            {
                // Set the filter to only allow Excel files (.xls and .xlsx)
                objDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
                objDialog.Title = "Select an Excel File";

                if (objDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected Excel file
                    lbl_ExcelPath.Text = objDialog.FileName;
                    if (lbl_xmlFolder.Text == "-")
                    {
                        lbl_xmlFolder.Text = Path.GetDirectoryName(lbl_ExcelPath.Text);
                    }
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

        private void btn_ExcelToXml_Click(object objSender, EventArgs objEvt)
        {
            rtb_Log.SelectionColor = Color.Black;
            rtb_Log.AppendText("--------------------------------------------------->\r\n");
            GenerateParamater();
            GenerateTrace();
            GenerateEvent();
            GenerateDCP();
            rtb_Log.SelectionColor = Color.Black;
            rtb_Log.AppendText("<---------------------------------------------------\r\n");
        }

        void GenerateParamater()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objResult;
            string srtParaResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.Parameter);
            objResult = objExcelData.Read(lbl_ExcelPath.Text);
            srtParaResult = objResult.Message;

            if (objResult.IsSuccess == true)
            {
                Log(srtParaResult);
                
                string resurlt = XmlServices.GenerateParameterToXml(objResult.Models, lbl_xmlFolder.Text);
            }
            else
            {
                ErrLog(srtParaResult);
            }
        }
        void GenerateTrace()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objResult;
            string srtParaResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.Trace);
            objResult = objExcelData.Read(lbl_ExcelPath.Text);
            srtParaResult = objResult.Message;

            if (objResult.IsSuccess == true)
            {
                Log(srtParaResult);
                string resurlt = XmlServices.GenerateTraceToXml(objResult.Models, lbl_xmlFolder.Text);
            }
            else
            {
                ErrLog(srtParaResult);
            }
        }
        void GenerateEvent()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objResult;
            string strExResult;
            string strXmlResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.Event);
            objResult = objExcelData.Read(lbl_ExcelPath.Text);
            strExResult = objResult.Message;

            if (objResult.IsSuccess == true)
            {
                strXmlResult = XmlServices.GenerateEventTriggerToXml(objResult.Models, lbl_xmlFolder.Text);
                if (string.IsNullOrEmpty(strXmlResult))
                {
                    Log(strExResult);
                }
                else
                {
                    ErrLog(strXmlResult);
                }

                strXmlResult = XmlServices.GenerateEventRequestToXml(objResult.Models, lbl_xmlFolder.Text);
                if (string.IsNullOrEmpty(strXmlResult))
                {
                    Log(strExResult);
                }
                else
                {
                    ErrLog(strXmlResult);
                }
            }
            else
            {
                ErrLog(strExResult);
            }
        }
        void GenerateDCP()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objResult;
            string strExResult;
            string strXmlResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.DataCollectionPlan);
            objResult = objExcelData.Read(lbl_ExcelPath.Text);
            strExResult = objResult.Message;

            if (objResult.IsSuccess == true)
            {
                Log(strExResult);
                strXmlResult = XmlServices.GenerateDCPXml(objResult.Models, lbl_xmlFolder.Text);
                if (string.IsNullOrEmpty(strXmlResult))
                {
                    Log(strExResult);
                }
                else
                {
                    ErrLog(strXmlResult);
                }
            }
            else
            {
                ErrLog(strExResult);
            }
        }
        void ErrLog(string x_strMsg)
        {
            rtb_Log.SelectionColor = Color.Red;
            AddLog(x_strMsg);
        }

        void WarningLog(string x_strMsg)
        {
            rtb_Log.SelectionColor = Color.GreenYellow;
            AddLog(x_strMsg);
        }
        void Log(string x_strMsg)
        {
            rtb_Log.SelectionColor = Color.DarkGreen;
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

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            rtb_Log.Clear();
        }
    }
}

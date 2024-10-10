
using ExcelToXmlList;
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

namespace ExcelToXmlConverter
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
            ExcelProcessResult objExlResult;
            ConvertResult objXmlResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.Parameter);
            objExlResult = objExcelData.Read(lbl_ExcelPath.Text);


            if (objExlResult.IsSuccess == true)
            {
                objXmlResult = XmlServices.GenerateParameterToXml(objExlResult.Models, lbl_xmlFolder.Text, $"{ExcelSheetName.Parameter}.xml");
                if (objXmlResult.IsSuccess)
                {
                    Log(objXmlResult.Message);
                }
                else
                {
                    ErrLog(objXmlResult.Message);
                }
            }
            else
            {
                ErrLog(objExlResult.Message);
            }
        }
        void GenerateTrace()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objExlResult;
            ConvertResult objXmlResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.TraceRequest);
            objExlResult = objExcelData.Read(lbl_ExcelPath.Text);

            if (objExlResult.IsSuccess == true)
            {
                objXmlResult = XmlServices.GenerateTraceToXml(objExlResult.Models, lbl_xmlFolder.Text, $"{ExcelSheetName.TraceRequest}.xml");
                if (objXmlResult.IsSuccess)
                {
                    Log(objXmlResult.Message);
                }
                else
                {
                    ErrLog(objXmlResult.Message);
                }
            }
            else
            {
                ErrLog(objExlResult.Message);
            }
        }
        void GenerateEvent()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objExlResult;
            ConvertResult objXmlResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.Event);
            objExlResult = objExcelData.Read(lbl_ExcelPath.Text);

            if (objExlResult.IsSuccess == true)
            {
                objXmlResult = XmlServices.GenerateEventTriggerToXml(objExlResult.Models, lbl_xmlFolder.Text, $"{EVENT.SHEET_NAME_TRIGGER}.xml");
                if (objXmlResult.IsSuccess)
                {
                    Log(objXmlResult.Message);
                }
                else
                {
                    ErrLog(objXmlResult.Message);
                }

                objXmlResult = XmlServices.GenerateEventRequestToXml(objExlResult.Models, lbl_xmlFolder.Text, $"{EVENT.SHEET_NAME_REQUEST}.xml");
                if (objXmlResult.IsSuccess)
                {
                    Log(objXmlResult.Message);
                }
                else
                {
                    ErrLog(objXmlResult.Message);
                }
            }
            else
            {
                ErrLog(objExlResult.Message);
            }
        }
        void GenerateDCP()
        {
            ExcelDataService objExcelData;
            ExcelProcessResult objExlResult;
            ConvertResult objXmlResult;

            objExcelData = ExcelDataService.Initialize(ExcelSheetName.DataCollectionPlan);
            objExlResult = objExcelData.Read(lbl_ExcelPath.Text);


            if (objExlResult.IsSuccess == true)
            {
                objXmlResult = XmlServices.GenerateDCPXml(objExlResult.Models, lbl_xmlFolder.Text);
                if (objXmlResult.IsSuccess == true)
                {
                    Log(objXmlResult.Message);
                }
                else
                {
                    ErrLog(objXmlResult.Message);
                }
            }
            else
            {
                ErrLog(objExlResult.Message);
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

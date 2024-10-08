using CommonCmpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CommonCmpLib_Test
{
    public partial class ExcelUC : UserControl
    {
        string m_strExcel_Path;
        string m_srtXmlFolder;
        List<string> m_lstSheetName;
        public ExcelUC()
        {
            InitializeComponent();
        }

        private void ExportParameterSheet(string x_strExcel_Path)
        {
            ExcelProcessResult<ExlParameterModel> objParameterProcess;
            bool IsSheetExits;
        }
        private void btn_Excel_Path_Click(object objSender, EventArgs objEvt)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the filter to only allow Excel files (.xls and .xlsx)
                openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
                openFileDialog.Title = "Select an Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected Excel file
                    m_strExcel_Path = openFileDialog.FileName;
                    lbl_ExcelPath.Text = m_strExcel_Path;
                    GetSheetNames();
                }
            }
        }

        private void btn_XmlFolder_Click(object objSender, EventArgs objEvt)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a Folder";
                folderBrowserDialog.ShowNewFolderButton = true; // Allow user to create new folders if necessary
                                                                // Set the default folder path to the project's root folder
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected folder
                    m_srtXmlFolder = folderBrowserDialog.SelectedPath;

                    // Display the folder path in the label
                    lbl_xmlFolder.Text = m_srtXmlFolder;
                }
            }
        }

        private void BtnExcelToXml_Click(object sender, EventArgs e)
        {
            //string filePath = "parameters.xml";
            //File.WriteAllText(filePath, xmlString);
            //var a = Common.ConvertXmlToJson_Parameter(filePath, "parameters.json");
            //var b =Common.ConvertJsonToXml_Parameter("parameters.json", filePath);

            GetSheetNames();
            if (m_lstSheetName == null)
            {
                return;
            }
            ExportParameterSheet(m_strExcel_Path);
        }

        private void btn_CreateTemplate_Click(object sender, EventArgs e)
        {
            //Common.ListTraceToXml();
            //TraceService.ReadFromExcel(m_strExcel_Path);
            //ParameterServices.ReadFromExcel(m_strExcel_Path);
            //EventService.ReadFromExcel(m_strExcel_Path);
            //DCPService.ReadFromExcel(m_strExcel_Path);
            rtxt_Log.Text += "--------------------------------------------------->\r\n";

            var @event = ExcelDataService.Create(ExcelSheetName.Event);
            var evResutl = @event.Read(m_strExcel_Path);
            if (evResutl.IsSuccess == true)
            {
                Common.DictionaryDataXml(evResutl);
            }
            rtxt_Log.Text += evResutl.Message;

            var dcp = ExcelDataService.Create(ExcelSheetName.DataCollectionPlan);
            var dcpresult = dcp.Read(m_strExcel_Path);
            if (dcpresult.IsSuccess == true)
            {
                //Common.DictionaryDataXml(dcpresult);
            }
            rtxt_Log.Text += dcpresult.Message;

            var excelData = ExcelDataService.Create(ExcelSheetName.Parameter);
            var objPara = excelData.Read(m_strExcel_Path);
            if (objPara.IsSuccess == true)
            {
                //Common.DictionaryDataXml(objPara);
            }
            rtxt_Log.Text += objPara.Message;

            var trace = ExcelDataService.Create(ExcelSheetName.Trace);
            var traceresult = trace.Read(m_strExcel_Path);
            if (traceresult.IsSuccess == true)
            {
                //Common.DictionaryDataXml(traceresult);
            }
            rtxt_Log.Text += traceresult.Message;
            rtxt_Log.Text += "<--------------------------------------------------\r\n";
        }

        private void GetSheetNames()
        {
            m_lstSheetName = ExcelDataService.GetSheetNames(m_strExcel_Path);
            if (m_lstSheetName == null)
            {
                MessageBox.Show("File Not Found!");
                return;
            }
            else if (m_lstSheetName.Count == 0)
            {
                MessageBox.Show("Can Not Find Sheets");
                return;
            }

            // Add each sheet name to the ListView
            rtxt_SheetNames.Clear();
            foreach (string sheetName in m_lstSheetName)
            {
                rtxt_SheetNames.Text += sheetName + Environment.NewLine;
            }
        }

    }
}

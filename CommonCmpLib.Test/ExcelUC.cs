﻿using CommonCmpLib;
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
            IsSheetExits = m_lstSheetName
                            .Any(sheetName => sheetName == ExcelSheetName.Parameter.ToString());
            if (IsSheetExits)
            {
                objParameterProcess = ParameterServices.ReadFromExcel(x_strExcel_Path);
                rtxt_Log.Text += $" ParameterSheet: IsSuccess = {objParameterProcess.IsSuccess}, " +
                    $"Tool Row = {objParameterProcess.TotalRow },Row Err = {objParameterProcess.CellError.Count}, " +
                    $"Header Err = {objParameterProcess.HeadersError.Count}\r\n";

                if (objParameterProcess.IsSuccess == true)
                {

                }
            }
            else
            {
                rtxt_Log.Text += $"{ExcelSheetName.Parameter} Sheet Not Exits!\r\n";
            }
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
            Common.ListTraceToXml();
            //TraceService.ReadFromExcel(m_strExcel_Path);
            //ParameterServices.ReadFromExcel(m_strExcel_Path);
            //EventService.ReadFromExcel(m_strExcel_Path);
            //DCPService.ReadFromExcel(m_strExcel_Path);
            Dictionary<string, string> COLUMN_HEADERS = new Dictionary<string, string>
        {
            { "A1", "No."  },
            { "B1", "MachineName" },
            { "C1", "PlanID" },
            { "D1", "PlanName" },
            { "E1", "Description" },
            { "F1", "StartEvent" },
            { "G1", "EndEvent" },
            { "H1", "TimeRequest" },
            { "I2", "ParameterID" }
        };
            string[] Mandatoryfields = new string[]{ "No.", "MachineName", "PlanID", "PlanName", "ParameterID" };

            ExcelDataService excelDataService = new ExcelDataService(COLUMN_HEADERS, "DataCollectionPlan", 2, 9, Mandatoryfields);
            excelDataService.ReadFromExcel(m_strExcel_Path);
        }

        private void GetSheetNames()
        {
            m_lstSheetName = ExcelComonServices.GetSheetNames(m_strExcel_Path);
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

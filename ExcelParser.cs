using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelDevice
{
    class ExcelImportUtility
    {
        //public string _filePath;
        public IWorkbook _workbook;
        public List<string> _sheetname { get; set; }
        public ExcelImportUtility()
        { 
            _sheetname = new List<string>();
        }
        /// <summary>
        /// 传入一个Excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<string> UpdateExcel(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open);
            _workbook = WorkbookFactory.Create(fs);
            return GetSheetNames();
        }
        /// <summary>
        /// 解析Excel文件，得到sheet的具体名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetNames()
        {
            //one book contains many sheets
            int count = _workbook.NumberOfSheets;
            for (int i = 0; i < count; ++i)
            {
                _sheetname.Add(_workbook.GetSheetName(i));
            }
            return _sheetname;
        }
        /// <summary>
        /// We can get all the datat from the inputed excels
        /// </summary>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        public DataTable Getinfo(List<string> sheetname)
        {
            ISheet sheet = _workbook.GetSheetAt(0);
            var firstRow = sheet.GetRow(0);
            int cellCount = firstRow.LastCellNum;
            int rowCount = sheet.LastRowNum;
            DataTable dt = new DataTable();
            for (int i =firstRow.FirstCellNum; i < rowCount+1; i++)
            {
               DataRow dataRow = dt.NewRow();
               for (int j = firstRow.FirstCellNum; j < cellCount; j++)
            {
                //.StringCellValue;
                var column = new DataColumn(Convert.ToChar(((int)'A') + j).ToString());
                var columnName = sheet.GetRow(i).GetCell(j).ToString();
                column = new DataColumn(columnName);
                dt.Columns.Add(column);
            }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
        public void PrintDataTable(DataTable dataTable) 
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Console.WriteLine(dataTable.Rows[i][j].ToString());
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.OleDb;

namespace Common.Util
{
    public class ExcelUtil
    {
        public Microsoft.Office.Interop.Excel.Application _application;
        public Microsoft.Office.Interop.Excel.Workbooks _workbooks;
        public Microsoft.Office.Interop.Excel.Workbook _workbook;
        public Microsoft.Office.Interop.Excel.Worksheet _worksheet;

        private string _strFilePath;

        public void Create(string strFilePath)
        {
            _strFilePath = strFilePath;

            _application = new Microsoft.Office.Interop.Excel.Application();
            _workbooks = _application.Workbooks;
            _workbook = _workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            _worksheet = (Microsoft.Office.Interop.Excel.Worksheet)_workbook.Worksheets[1];

        }


        public void Open(string strFilePath)
        {
            _strFilePath = strFilePath;

            _application = new Microsoft.Office.Interop.Excel.Application();
            _workbooks = _application.Workbooks;
            _workbook = _workbooks.Open(_strFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            _worksheet = (Microsoft.Office.Interop.Excel.Worksheet)_workbook.Worksheets[1];

        }


        public bool Save()
        {

            bool flag1;

            try
            {

                _workbook.Save();
                _workbook.Saved = true;
                _application.Visible = false;

                Close();
                flag1 = true;
            }

            catch
            {
                Close();
                flag1 = false;

            }



            return flag1;
        }


        public bool SaveCopyAs(string strFilePath)
        {

            bool flag1;

            try
            {
                _application.Visible = false;
                System.IO.FileInfo info = new System.IO.FileInfo(strFilePath);
                if (!info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                info=null;
                _workbook.SaveCopyAs(strFilePath);
                _workbook.Saved = true;


                flag1 = true;
            }

            catch
            {

                flag1 = false;

            }



            return flag1;
        }



        public bool SaveAs(object FileName)
        {

            bool flag1;

            try
            {
                _workbook.Saved = true;
                _application.Visible = true;

                this._workbook.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                flag1 = true;
            }

            catch
            {

                flag1 = false;

            }

            return flag1;

        }


        #region 工作表操作
        /// <summary>
        /// 添加工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Worksheet AddSheet(string SheetName)
        {

            Worksheet worksheet = (Worksheet)this._workbook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            worksheet.Name = SheetName;

            return worksheet;

        }

        /// <summary>
        /// 删除工作表
        /// </summary>
        /// <param name="SheetName"></param>
        public void DelSheet(string SheetName)
        {

            ((Worksheet)_workbook.Worksheets[SheetName]).Delete();

        }

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Worksheet GetSheet(string SheetName)
        {
            try
            {
                return (Worksheet)this._workbook.Worksheets[SheetName];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public Worksheet GetSheet(int sheetIndex)
        {
            return (Worksheet)_workbook.Worksheets[sheetIndex];
        }


        /// <summary>
        /// 重命名工作表名称
        /// </summary>
        /// <param name="OldSheetName"></param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Worksheet RenameSheet(string OldSheetName, string NewSheetName)
        {

            Worksheet worksheet2 = (Worksheet)this._workbook.Worksheets[OldSheetName];

            worksheet2.Name = NewSheetName;

            return worksheet2;

        }

        public System.Data.DataTable GetSheetData(Worksheet worksheet)
        {
            try
            {

                System.Data.DataTable dt = new System.Data.DataTable();

                if (worksheet != null)
                {
                    int rows = worksheet.UsedRange.Rows.Count;
                    int cols = worksheet.UsedRange.Columns.Count;
                    //增加行
                    for (int intRowIndex = 0; intRowIndex < rows; intRowIndex++)
                    {
                        dt.Rows.Add(dt.NewRow());
                    }
                    //增加新列
                    for (int intColIndex = 0; intColIndex < cols; intColIndex++)
                    {
                        //dt.Columns.Add(intColIndex.ToString());
                        dt.Columns.Add();
                        //填写默认值为空串
                        dt.Columns[intColIndex].DefaultValue = "";
                    }

                    int i, j;
                    for (i = 0; i < rows; i++)
                    {
                        for (j = 0; j < cols; j++)
                        {
                            Range r = ((Range)worksheet.Cells[i + 1, j + 1]);

                            dt.Rows[i][j] = r.Text.ToString().Trim();
                        }
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }



        }

        /// <summary>
        /// 獲取Excel的sheets name
        /// </summary>
        /// <returns></returns>
        public string[] GetExcelSheet()
        {
            int t = _workbook.Worksheets.Count;

            string[] names = new string[t];
            for (int i = 0; i < t; i++)
            {
                names[i] = ((Worksheet)_workbook.Worksheets[i + 1]).Name;
            }
            return names;
        }
        #endregion






        /// <summary>
        /// 默认第一个sheet取
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Range GetRange(string cell1, string cell2)
        {
            //Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)_workbook.Worksheets[1];
            //// Microsoft.Office.Interop.Excel.Range range = ws.Cells;
            //Microsoft.Office.Interop.Excel.Range ran = ws.get_Range(cell1, cell2);

            Microsoft.Office.Interop.Excel.Range ran = GetRange(1, cell1, cell2);
            return ran;
        }

        public Microsoft.Office.Interop.Excel.Range GetRange(int sheetIndex, string cell1, string cell2)
        {
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)_workbook.Worksheets[sheetIndex];
            Microsoft.Office.Interop.Excel.Range ran = ws.get_Range(cell1, cell2);
            return ran;
        }


        public void AddValidation(Microsoft.Office.Interop.Excel.Range r, string data)
        {

            r.Validation.Add(Microsoft.Office.Interop.Excel.XlDVType.xlValidateList, Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop, Type.Missing, data, Type.Missing);

        }


        /// <summary>
        /// 必须每次操作完都执行，否则可能将对应的excel 锁住。
        /// 注意：1，关闭不保存对文件的修改，在关闭之前，请执行保存方法
        /// </summary>
        public void Close()
        {

            _worksheet = null;
            if (_workbook != null)
            {
                _workbook.Close(false, null, null);
            }
            if (_workbooks != null)
            {
                _workbooks.Close();
            }

            _workbook = null;
            if (_application != null)
            {

                IntPtr t = new IntPtr(_application.Hwnd); //得到这个句柄，具体作用是得到这块内存入口  
                _application.Quit();
                int k = 0;
                GetWindowThreadProcessId(t, out k); //得到本进程唯一标志k 
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); //得到对进程k的引用 
                p.Kill(); //关闭进程k 
            }




            //KillProcess("Excel");
        }
        //杀死进程

        private void KillProcess(string processName)
        {
            //获得进程对象，以用来操作   
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程    
            try
            {
                //获得需要杀死的进程名   
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    //立即杀死进程   
                    thisproc.Kill();
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }

        private void NAR(Object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        public bool AddValueToCell(int intRowID, int intColumnID, string strCellValue)
        {

            return AddValueToCell(1, intRowID, intColumnID, strCellValue);
        }


        public bool AddValueToCell(int sheetIndex, int intRowID, int intColumnID, string strCellValue)
        {

            try
            {
                Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)_workbook.Worksheets[sheetIndex];

                Range range = (Microsoft.Office.Interop.Excel.Range)ws.Cells[intRowID, intColumnID];

                if ((bool)range.MergeCells)
                {
                    Microsoft.Office.Interop.Excel.Range margeRange = range.MergeArea;
                    range = (Microsoft.Office.Interop.Excel.Range)_worksheet.Cells[margeRange.Row, margeRange.Column];
                }
                range.Value2 = strCellValue;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>

        public bool DeleteRows(int sheetIndex, int rowIndex, int count)
        {
            if (sheetIndex > this._workbook.Worksheets.Count)
            {
                Close();
                throw new Exception("索引超出范围，WorkSheet索引不能大于WorkSheet数量！");
            }

            try
            {
                var workSheet = GetSheet(sheetIndex);


                for (int i = 0; i <= count - 1; i++)
                {
                    var range = (Range)workSheet.Rows[rowIndex, Missing.Value];
                    range.Delete(XlDirection.xlDown);
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
                throw e;

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilityLibrary.WinControls;
using Model.IModel;
using Model.General;
using System.Reflection;

namespace WindowUI.ClassLibrary.Public
{
    public static class ExMethod
    {
        /// <summary>
        /// 通過Value值定位到相應的記錄
        /// </summary>
        /// <param name="ctlComboBox">ComboBox控件</param>
        /// <param name="strValue">Value值</param>
        public static void SelectItemForValue(this ComboBox ctlComboBox, string strValue)
        {

            if (ctlComboBox.Items.Count > 0)
            {
                ctlComboBox.SelectedValue = strValue.Trim();
                for (int i = 0; i < ctlComboBox.Items.Count; i++)
                {
                    //ctlComboBox.SelectedIndex = i;
                    if (ctlComboBox.Items[i] != null)
                    {

                        //string value= ctlComboBox.Items[i].ToString().Trim();
                        //if (value == strValue.Trim())
                        //{
                        //    ctlComboBox.SelectedIndex = i;
                        //    break;
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// 設置控件的狀態
        /// </summary>
        /// <param name="ctlTextBox">控件對象</param>
        /// <param name="isReadOnly">是否只讀狀態</param>
        public static void TextBoxSetStatus(this TextBox ctlTextBox, bool isReadOnly)
        {
            if (isReadOnly)
            {
                ctlTextBox.BackColor = System.Drawing.SystemColors.Info;
                ctlTextBox.Enabled = false;
            }
            else
            {
                ctlTextBox.BackColor = System.Drawing.SystemColors.Window;
                ctlTextBox.Enabled = true;
            }
        }

        public static void ComboBoxSetStatc(this ComboBox ctlComboBox, bool isReadOnly)
        {
            if (isReadOnly)
            {
                ctlComboBox.Enabled = false;
            }
            else
            {
                ctlComboBox.BackColor = System.Drawing.SystemColors.Window;
                ctlComboBox.Enabled = true;
            }
        }


        /// <summary>
        /// 设置控件数据源
        /// </summary>
        /// <param name="ctlComboBox"></param>
        /// <param name="datasource">数据源</param>
        public static void SetDataSource(this ComboBox ctlComboBox, List<IModelObject> datasource)
        {

            if (datasource == null || datasource.Count <= 0)
            {
                ctlComboBox.DataSource = null;
                return;
            }

            List<ComboboxDataInfo> Comboboxdatas = new List<ComboboxDataInfo>();

            foreach (ComboboxDataInfo item in datasource)
            {
                Comboboxdatas.Add(item);
            }

            ctlComboBox.DisplayMember = ComboboxDataInfoEnum.DisplayMember.ToString();
            ctlComboBox.ValueMember = ComboboxDataInfoEnum.ValueMember.ToString();
            ctlComboBox.DataSource = Comboboxdatas;
        }


        /// </summary>
        /// <param name="ctlComboBox"></param>
        /// <param name="datasource">数据源</param>
        public static void SetDataSource(this ListBox ctlListBox, List<IModelObject> datasource)
        {

            if (datasource == null || datasource.Count <= 0)
            {
                return;
            }

            List<ComboboxDataInfo> Comboboxdatas = new List<ComboboxDataInfo>();

            foreach (ComboboxDataInfo item in datasource)
            {
                Comboboxdatas.Add(item);
            }

            ctlListBox.DisplayMember = ComboboxDataInfoEnum.DisplayMember.ToString();
            ctlListBox.ValueMember = ComboboxDataInfoEnum.ValueMember.ToString();
            ctlListBox.DataSource = Comboboxdatas;
        }

        /// <summary>
        /// 设置控件数据源
        /// </summary>
        /// <param name="ctlComboBox"></param>
        /// <param name="datasource">数据源</param>
        /// <param name="blankSpaceIndex">在某一個位置上增加一空白選項</param>
        public static void SetDataSource(this ComboBox ctlComboBox, List<IModelObject> datasource, int blankSpaceIndex)
        {
            if (datasource == null || datasource.Count <= 0)
            {
                return;
            }

            List<ComboboxDataInfo> Comboboxdatas = new List<ComboboxDataInfo>();
            int index = 0;

            foreach (ComboboxDataInfo item in datasource)
            {
                if (index == blankSpaceIndex)
                {
                    ComboboxDataInfo bspaceInfo = new ComboboxDataInfo();
                    bspaceInfo.DisplayMember = "";
                    bspaceInfo.ValueMember = "";

                    Comboboxdatas.Add(bspaceInfo);
                }

                Comboboxdatas.Add(item);
                index++;
            }

            if (blankSpaceIndex >= index)
            {
                ComboboxDataInfo bspaceInfo = new ComboboxDataInfo();
                bspaceInfo.DisplayMember = "";
                bspaceInfo.ValueMember = "";

                Comboboxdatas.Add(bspaceInfo);
            }


            ctlComboBox.DisplayMember = ComboboxDataInfoEnum.DisplayMember.ToString();
            ctlComboBox.ValueMember = ComboboxDataInfoEnum.ValueMember.ToString();
            ctlComboBox.DataSource = Comboboxdatas;
            ctlComboBox.SelectedValue = "";
        }

        /// <summary>
        /// 根据ListView 的Column Tag 与数据源 的属性比较，填充对应的值
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="listView">控件</param>
        /// <param name="dataSources">数据源</param>
        /// <param name="insertIndex">插入的位置</param>
        public static void AddItemDataSource<T>(this  ListView listView, T dataSource,int insertIndex)
        {
            if (dataSource == null)
            {
                return;
            }

            if (listView.Columns.Count == 0)
            {
                return;
            }

            #region 数据源

            Dictionary<string, string> dic = null;

            Type type = dataSource.GetType();
            PropertyInfo[] pis = type.GetProperties();
            PropertyInfo pi;

            if (pis.Length > 0)
            {
                dic = new Dictionary<string, string>();
                for (int i = 0; i < pis.Length; i++)
                {
                    pi = pis[i];
                    object value = pi.GetValue(dataSource, null);
                    string valueString = "";
                    if (value != null)
                    {
                        valueString = value.ToString().Trim();
                    }

                    dic.Add(pi.Name, valueString);
                }
            }
            

            #endregion

            #region 填充数据

            ListViewItem lvItem = null;
            
            for (int j = 0; j < listView.Columns.Count; j++)
            {
                string Rtv = "";
                if (listView.Columns[j].Tag != null && dic.TryGetValue(listView.Columns[j].Tag.ToString(), out Rtv))
                {
                    if (j == 0)
                    {
                        lvItem = new ListViewItem(dic[listView.Columns[j].Tag.ToString()]);
                    }
                    else
                    {
                        lvItem.SubItems.Add(dic[listView.Columns[j].Tag.ToString()]);
                    }
                }
                else
                {
                    if (j == 0)
                    {
                        lvItem = new ListViewItem("");
                    }
                    else
                    {
                        lvItem.SubItems.Add("");
                    }
                }
            }
            if (lvItem != null)
            {
                if (insertIndex == -1)
                {
                    listView.Items.Add(lvItem);
                }
                else
                {
                    listView.Items.Insert(insertIndex, lvItem);
                }
            }

            #endregion
        }

        /// <summary>
        /// 根据ListView 的Column Tag 与数据源 的属性比较，填充对应的值
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="listView">控件</param>
        /// <param name="dataSources">数据源</param>
        /// <param name="insertStartIndex">插入的位置</param>
        static void SetListViewDataSource<T>(ListView listView, List<T> dataSources, int insertStartIndex)
        {
            listView.Items.Clear();
            if (dataSources == null)
            {
                return;
            }
            if (dataSources.Count == 0)
            {
                return;
            }


            if (listView.Columns.Count == 0)
            {
                return;
            }

            #region 数据源

            List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
            Dictionary<string, string> dic = null;


            foreach (T dataSource in dataSources)
            {
                Type type = dataSource.GetType();
                PropertyInfo[] pis = type.GetProperties();
                PropertyInfo pi;

                if (pis.Length > 0)
                {
                    dic = new Dictionary<string, string>();
                    for (int i = 0; i < pis.Length; i++)
                    {
                        pi = pis[i];
                        object value = pi.GetValue(dataSource, null);
                        string valueString = "";
                        if (value != null)
                        {
                            valueString = value.ToString().Trim();
                        }

                        dic.Add(pi.Name, valueString);
                    }
                }

                dicList.Add(dic);
            }

            #endregion

            #region 填充数据

            if (dicList.Count > 0)
            {
                ListViewItem lvItem = null;
                for (int i = 0; i < dicList.Count; i++)
                {
                    dic = dicList[i];
                    for (int j = 0; j < listView.Columns.Count; j++)
                    {
                        string Rtv = "";
                        if (listView.Columns[j].Tag != null && dic.TryGetValue(listView.Columns[j].Tag.ToString(), out Rtv))
                        {
                            if (j == 0)
                            {
                                lvItem = new ListViewItem(dic[listView.Columns[j].Tag.ToString()]);
                            }
                            else
                            {
                                lvItem.SubItems.Add(dic[listView.Columns[j].Tag.ToString()]);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                lvItem = new ListViewItem("");
                            }
                            else
                            {
                                lvItem.SubItems.Add("");
                            }
                        }
                    }
                    if (lvItem != null)
                    {
                        if (insertStartIndex == -1)
                        {
                            listView.Items.Add(lvItem);
                        }
                        else
                        {
                            listView.Items.Insert(insertStartIndex, lvItem);
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 根据ListView 的Column Tag 与数据源 的属性比较，填充对应的值
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="listView">控件</param>
        /// <param name="dataSources">数据源</param>
        public static void SetDataSource<T>(this  ListView listView, List<T> dataSources)
        {
            SetListViewDataSource<T>(listView, dataSources, -1);

            #region MyRegion
            
            //listView.Items.Clear();
            //if (dataSources == null)
            //{
            //    return;
            //}
            //if (dataSources.Count == 0)
            //{
            //    return;
            //}


            //if (listView.Columns.Count == 0)
            //{
            //    return;
            //}

            //#region 数据源

            //List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
            //Dictionary<string, string> dic = null;


            //foreach (T dataSource in dataSources)
            //{
            //    Type type = dataSource.GetType();
            //    PropertyInfo[] pis = type.GetProperties();
            //    PropertyInfo pi;

            //    if (pis.Length > 0)
            //    {
            //        dic = new Dictionary<string, string>();
            //        for (int i = 0; i < pis.Length; i++)
            //        {
            //            pi = pis[i];
            //            object value = pi.GetValue(dataSource, null);
            //            string valueString = "";
            //            if (value != null)
            //            {
            //                valueString = value.ToString().Trim();
            //            }

            //            dic.Add(pi.Name, valueString);
            //        }
            //    }

            //    dicList.Add(dic);
            //}

            //#endregion

            //#region 填充数据

            //if (dicList.Count > 0)
            //{
            //    ListViewItem lvItem = null;
            //    for (int i = 0; i < dicList.Count; i++)
            //    {
            //        dic = dicList[i];
            //        for (int j = 0; j < listView.Columns.Count; j++)
            //        {
            //            string Rtv = "";
            //            if (listView.Columns[j].Tag != null && dic.TryGetValue(listView.Columns[j].Tag.ToString(), out Rtv))
            //            {
            //                if (j == 0)
            //                {
            //                    lvItem = new ListViewItem(dic[listView.Columns[j].Tag.ToString()]);
            //                }
            //                else
            //                {
            //                    lvItem.SubItems.Add(dic[listView.Columns[j].Tag.ToString()]);
            //                }
            //            }
            //            else
            //            {
            //                if (j == 0)
            //                {
            //                    lvItem = new ListViewItem("");
            //                }
            //                else
            //                {
            //                    lvItem.SubItems.Add("");
            //                }
            //            }
            //        }
            //        if (lvItem != null)
            //        {
            //            listView.Items.Add(lvItem);
            //        }
            //    }
            //}
            //#endregion

            #endregion
        }


        /// <summary>
        /// 根据ListView 的Column Tag 与数据源 的属性比较，填充对应的值
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="listView">控件</param>
        /// <param name="dataSources">数据源</param>
        /// <param name="ClearOldRecords">清除原先數據</param>
        public static void SetDataSource<T>(this  ListView listView, List<T> dataSources, bool ClearOldRecords)
        {

            if (ClearOldRecords)
            {
                listView.Items.Clear();
            }


            if (dataSources == null)
            {
                return;
            }
            if (dataSources.Count == 0)
            {
                return;
            }


            if (listView.Columns.Count == 0)
            {
                return;
            }

            #region 数据源




            List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
            Dictionary<string, string> dic = null;


            foreach (T dataSource in dataSources)
            {


                Type type = dataSource.GetType();
                PropertyInfo[] pis = type.GetProperties();
                PropertyInfo pi;

                if (pis.Length > 0)
                {
                    dic = new Dictionary<string, string>();
                    for (int i = 0; i < pis.Length; i++)
                    {
                        pi = pis[i];
                        object value = pi.GetValue(dataSource, null);
                        string valueString = "";
                        if (value != null)
                        {
                            valueString = value.ToString().Trim();
                        }

                        dic.Add(pi.Name, valueString);
                    }
                }

                dicList.Add(dic);
            }

            #endregion

            #region 填充数据

            if (dicList.Count > 0)
            {
                ListViewItem lvItem = null;
                for (int i = 0; i < dicList.Count; i++)
                {
                    dic = dicList[i];
                    for (int j = 0; j < listView.Columns.Count; j++)
                    {
                        string Rtv = "";
                        if (listView.Columns[j].Tag != null && dic.TryGetValue(listView.Columns[j].Tag.ToString(), out Rtv))
                        {
                            if (j == 0)
                            {
                                lvItem = new ListViewItem(dic[listView.Columns[j].Tag.ToString()]);
                            }
                            else
                            {
                                lvItem.SubItems.Add(dic[listView.Columns[j].Tag.ToString()]);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                lvItem = new ListViewItem("");
                            }
                            else
                            {
                                lvItem.SubItems.Add("");
                            }
                        }
                    }
                    if (lvItem != null)
                    {
                        listView.Items.Add(lvItem);
                    }
                }
            }
            #endregion


        }



    }
}

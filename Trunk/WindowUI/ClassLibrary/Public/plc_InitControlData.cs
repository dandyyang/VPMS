using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Model.Master;
using GeneralBll = BLL.General;
using Model.IModel;
using Model.General;
using cmm = Common;

namespace WindowUI.ClassLibrary.Public
{
    class plc_InitControlData
    {
        #region ListView 填充ListView控件
        /// <summary>
        /// 填充ListView控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="propertyNameList"></param>
        /// <returns></returns>
        public static void InitListViewItem<T>(T[] dataSources, List<string> propertyNameList, ListView listView)
        {
            listView.Items.Clear();
            if (dataSources == null || propertyNameList == null)
            {
                return;
            }
            if (dataSources.Length == 0)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                T dataSource;
                List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = null;

                for (int x = 0; x < dataSources.Length; x++)
                {
                    dataSource = dataSources[x];

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

                if (dicList.Count > 0)
                {
                    ListViewItem lvItem = null;
                    for (int i = 0; i < dicList.Count; i++)
                    {
                        dic = dicList[i];
                        for (int j = 0; j < propertyNameList.Count; j++)
                        {
                            if (j == 0)
                            {
                                lvItem = new ListViewItem(dic[propertyNameList[j]]);
                            }
                            else
                            {
                                lvItem.SubItems.Add(dic[propertyNameList[j]]);
                            }
                        }
                        listView.Items.Add(lvItem);
                    }
                }
            }
        }


        public static void InitListViewItem<T>(List<T> dataSources, List<string> propertyNameList, ListView listView)
        {
            listView.Items.Clear();
            if (dataSources == null || propertyNameList == null)
            {
                return;
            }
            if (dataSources.Count == 0)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                //T dataSource;
                List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = null;


                foreach (T dataSource in dataSources)
                {
                    //dataSource = dataSources[x];

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


                if (dicList.Count > 0)
                {
                    ListViewItem lvItem = null;
                    for (int i = 0; i < dicList.Count; i++)
                    {
                        dic = dicList[i];
                        for (int j = 0; j < propertyNameList.Count; j++)
                        {

                            //switch (j)
                            //{
                            //    case 0:
                            //        lvItem = new ListViewItem(dic[propertyNameList[j]]);
                            //        break;

                            //    default:
                            //        lvItem.SubItems.Add(dic[propertyNameList[j]]);
                            //        break;
                            //}

                            if (j == 0)
                            {
                                lvItem = new ListViewItem(dic[propertyNameList[j]]);
                            }
                            else
                            {
                                lvItem.SubItems.Add(dic[propertyNameList[j]]);
                            }
                        }
                        listView.Items.Add(lvItem);
                    }
                }
            }
        }
        /// <summary>
        /// 填充ListView控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="propertyNameList"></param>
        /// <returns></returns>
        public static void InitListViewItem<T>(T[] dataSources, List<string> propertyNameList, ListView listView, bool isShortDate)
        {
            listView.Items.Clear();
            if (dataSources == null || propertyNameList == null)
            {
                return;
            }
            if (dataSources.Length == 0)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                T dataSource;
                List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = null;

                for (int x = 0; x < dataSources.Length; x++)
                {
                    dataSource = dataSources[x];

                    Type type = dataSource.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    PropertyInfo pi;
                    Type propertyType = null;
                    Type classPropertyType = null;

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
                                classPropertyType = pi.PropertyType;
                                propertyType = Nullable.GetUnderlyingType(classPropertyType);

                                if (propertyType == null)
                                {
                                    propertyType = classPropertyType;
                                }

                                if (propertyType == typeof(DateTime))
                                {
                                    if (isShortDate)
                                    {
                                        valueString = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        valueString = value.ToString().Trim();
                                    }
                                }
                                else
                                {
                                    valueString = value.ToString().Trim();
                                }
                            }

                            dic.Add(pi.Name, valueString);
                        }
                    }

                    dicList.Add(dic);
                }

                if (dicList.Count > 0)
                {
                    ListViewItem lvItem = null;
                    for (int i = 0; i < dicList.Count; i++)
                    {
                        dic = dicList[i];
                        for (int j = 0; j < propertyNameList.Count; j++)
                        {
                            if (j == 0)
                            {
                                lvItem = new ListViewItem(dic[propertyNameList[j]]);
                            }
                            else
                            {
                                lvItem.SubItems.Add(dic[propertyNameList[j]]);
                            }
                        }
                        listView.Items.Add(lvItem);
                    }
                }
            }
        }

        /// <summary>
        /// 新增ListView记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="propertyNameList"></param>
        /// <param name="ctlListView"></param>
        public static void AddListViewItem<T>(T dataSource, List<string> propertyNameList, ListView listView)
        {
            if (dataSource == null || propertyNameList == null)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Type type = dataSource.GetType();
                PropertyInfo[] pis = type.GetProperties();
                PropertyInfo pi;

                if (pis.Length > 0)
                {
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

                ListViewItem lvItem = null;

                for (int j = 0; j < propertyNameList.Count; j++)
                {
                    if (j == 0)
                    {
                        lvItem = new ListViewItem(dic[propertyNameList[j]]);
                    }
                    else
                    {
                        lvItem.SubItems.Add(dic[propertyNameList[j]]);
                    }
                }
                listView.Items.Add(lvItem);
            }
        }

        /// <summary>
        /// 新增ListView记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="propertyNameList"></param>
        /// <param name="ctlListView"></param>
        public static void AddListViewItem<T>(T dataSource, List<string> propertyNameList, ListView listView, bool isShortDate)
        {
            if (dataSource == null || propertyNameList == null)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Type type = dataSource.GetType();
                PropertyInfo[] pis = type.GetProperties();
                PropertyInfo pi;
                Type propertyType = null;
                Type classPropertyType = null;

                if (pis.Length > 0)
                {
                    for (int i = 0; i < pis.Length; i++)
                    {
                        pi = pis[i];
                        object value = pi.GetValue(dataSource, null);
                        string valueString = "";
                        if (value != null)
                        {
                            classPropertyType = pi.PropertyType;
                            propertyType = Nullable.GetUnderlyingType(classPropertyType);

                            if (propertyType == null)
                            {
                                propertyType = classPropertyType;
                            }

                            if (propertyType == typeof(DateTime))
                            {
                                if (isShortDate)
                                {
                                    valueString = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    valueString = value.ToString().Trim();
                                }
                            }
                            else
                            {
                                valueString = value.ToString().Trim();
                            }
                        }
                        dic.Add(pi.Name, valueString);
                    }
                }

                ListViewItem lvItem = null;

                for (int j = 0; j < propertyNameList.Count; j++)
                {
                    if (j == 0)
                    {
                        lvItem = new ListViewItem(dic[propertyNameList[j]]);
                    }
                    else
                    {
                        lvItem.SubItems.Add(dic[propertyNameList[j]]);
                    }
                }
                listView.Items.Add(lvItem);
            }
        }

        /// <summary>
        /// 修改ListView记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="propertyNameList"></param>
        /// <param name="listView"></param>
        public static void UpdateListViewItem<T>(T dataSource, List<string> propertyNameList, ListView listView)
        {
            if (dataSource == null || propertyNameList == null)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Type type = dataSource.GetType();
                PropertyInfo[] pis = type.GetProperties();
                PropertyInfo pi;

                if (pis.Length > 0)
                {
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

                for (int j = 0; j < propertyNameList.Count; j++)
                {
                    if (j == 0)
                    {
                        listView.SelectedItems[0].Text = dic[propertyNameList[j]];
                    }
                    else
                    {
                        listView.SelectedItems[0].SubItems[j].Text = dic[propertyNameList[j]];
                    }
                }
            }
        }

        /// <summary>
        /// 修改ListView记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="propertyNameList"></param>
        /// <param name="listView"></param>
        public static void UpdateListViewItem<T>(T dataSource, List<string> propertyNameList, ListView listView, bool isShortDate)
        {
            if (dataSource == null || propertyNameList == null)
            {
                return;
            }

            if (propertyNameList.Count > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Type type = dataSource.GetType();
                PropertyInfo[] pis = type.GetProperties();
                PropertyInfo pi;
                Type propertyType = null;
                Type classPropertyType = null;

                if (pis.Length > 0)
                {
                    for (int i = 0; i < pis.Length; i++)
                    {
                        pi = pis[i];
                        object value = pi.GetValue(dataSource, null);
                        string valueString = "";
                        if (value != null)
                        {
                            classPropertyType = pi.PropertyType;
                            propertyType = Nullable.GetUnderlyingType(classPropertyType);

                            if (propertyType == null)
                            {
                                propertyType = classPropertyType;
                            }

                            if (propertyType == typeof(DateTime))
                            {
                                if (isShortDate)
                                {
                                    valueString = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    valueString = value.ToString().Trim();
                                }
                            }
                            else
                            {
                                valueString = value.ToString().Trim();
                            }
                        }
                        dic.Add(pi.Name, valueString);
                    }
                }

                for (int j = 0; j < propertyNameList.Count; j++)
                {
                    if (j == 0)
                    {
                        listView.SelectedItems[0].Text = dic[propertyNameList[j]];
                    }
                    else
                    {
                        listView.SelectedItems[0].SubItems[j].Text = dic[propertyNameList[j]];
                    }
                }
            }
        }

        #endregion

        #region 填充Combox数据

        /// <summary>
        /// 填充部门数据
        /// </summary>
        /// <param name="cboDeptCombobox"></param>
        public static void InitDeptCombobox(ComboBox cboDeptCombobox, int iDropDownWidth)
        {
            DeptMasterInfo[] deptMasterInfos = null;
            GeneralBll.General general = new BLL.General.General();

            try
            {
                deptMasterInfos = general.GetDeptMasterDatas();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            DeptMasterInfo[] depts = null;
            if (deptMasterInfos != null)
            {
                depts = new DeptMasterInfo[deptMasterInfos.Length + 1];
                DeptMasterInfo deptMasterInfo = new DeptMasterInfo();

                depts[0] = deptMasterInfo;
                for (int i = 0; i < deptMasterInfos.Length; i++)
                {
                    depts[i + 1] = deptMasterInfos[i];
                }
            }
            cboDeptCombobox.DataSource = depts;
            cboDeptCombobox.DisplayMember = DeptMasterInfoEnum.DpmCDeptName.ToString();
            cboDeptCombobox.ValueMember = DeptMasterInfoEnum.DpmCDeptNumber.ToString();

            if (iDropDownWidth != 0)
            {
                cboDeptCombobox.DropDownWidth = iDropDownWidth;
            }
        }

        /// <summary>
        /// 设置装载COM口列表
        /// </summary>
        /// <param name="destination">目的控件</param>
        public static void SetPortList(ComboBox destination)
        {
            List<IModelObject> result = new List<IModelObject>();
            cmm.Entity.CommInfo[] cmmInfos = cmm.General.GetCommInfos();
            ComboboxDataInfo info = null;

            if (cmmInfos != null && cmmInfos.Length > 0)
            {
                for (int i = 0; i < cmmInfos.Length; i++)
                {
                    info = new ComboboxDataInfo();

                    info.DisplayMember = cmmInfos[i].CommPort.ToString().Trim();
                    info.ValueMember = cmmInfos[i].CommPort.ToString().Trim();

                    result.Add(info);
                }
            }


            destination.SetDataSource(result);
            if (destination.Items != null && destination.Items.Count > 0)
            {
                destination.SelectedIndex = 0;
            }
        }

        #endregion
    }
}

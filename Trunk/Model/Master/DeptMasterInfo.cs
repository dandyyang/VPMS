using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Master
{
    public class DeptMasterInfo:IModelObject
    {
        public DeptMasterInfo()
        {
            this.RecordID = 0;
            this.DpmCDeptName = string.Empty;
            this.DpmCDeptNumber = string.Empty;
            this.Remark = string.Empty;
        }
        
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DpmCDeptNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DpmCDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        public string Add
        {
            get;
            set;
        }
        
        public DateTime? AddDate
        {
            get;
            set;
        }
        
        public string Last
        {
            get;
            set;
        }

        public DateTime? LastDate
        {
            get;
            set;
        }
        /// <summary>
        /// 部门编号长度
        /// </summary>
        public int DeptNumber_Lenght
        {
            set;
            get;
        }
        /// <summary>
        /// 部门名称长度
        /// </summary>
        public int DeptName_Lenght
        {
            set;
            get;
        }
        /// <summary>
        /// 备注长度
        /// </summary>
        public int Remark_Lenght
        {
            set;
            get;
        }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }

    public enum DeptMasterInfoEnum
    {
        /// <summary>
        /// 記錄ID
        /// </summary>
        RecordID,
        /// <summary>
        /// 部门编号
        /// </summary>
        DpmCDeptNumber,
        /// <summary>
        /// 部门名称
        /// </summary>
        DpmCDeptName,
        /// <summary>
        /// 备注
        /// </summary>
        Remark,
        Add,
        AddDate,
        Last,
        LastDate,
        TableName
    }
}

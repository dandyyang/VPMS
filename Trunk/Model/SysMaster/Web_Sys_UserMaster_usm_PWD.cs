using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Web_Sys_UserMaster_usm_PWD:IModelObject
    {
        public Web_Sys_UserMaster_usm_PWD()
        {
            this.usm_cUserLoginID = string.Empty;
            this.usm_cOldPasswork = string.Empty;
            this.usm_cNewPasswork = string.Empty;
        }
        public string usm_cUserLoginID
        { get; set; }
        public string usm_cOldPasswork
        { get; set; }
        public string usm_cNewPasswork
        { get; set; }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.Master
{
    public class UserSkin_urs_Info:IModelObject
    {

        #region 构造函数

        public UserSkin_urs_Info()
        {

            urs_iRecordID = 0;

            urs_cUserID = string.Empty;

            urs_cSkinName = string.Empty;

        }
        #endregion

        #region 析构函数
        ~UserSkin_urs_Info()
        {

        }
        #endregion

        #region 属性


        public Int32 urs_iRecordID { set; get; }

        public string urs_cUserID { set; get; }

        public string urs_cSkinName { set; get; }
        #endregion

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Master;
using DAL.Factory.Master;
using DAL.IDAL.Master;

namespace BLL.General
{
    public class General
    {
        public General()
        {
        }

        /// <summary>
        /// 获得部门数据数组
        /// </summary>
        /// <returns></returns>
        public DeptMasterInfo[] GetDeptMasterDatas()
        {
            DeptMasterInfo[] deptMasterInfos = null;
            List<DeptMasterInfo> deptMasterInfoList = null;
            IDeptMasterDA iDeptMasterDA = MasterDAFactory.Instance.GetDeptMasterDA();
            try
            {
                deptMasterInfoList = iDeptMasterDA.GetAllRecord();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (deptMasterInfoList != null)
            {
                deptMasterInfos = deptMasterInfoList.ToArray();
            }

            return deptMasterInfos;
        }
    }
}

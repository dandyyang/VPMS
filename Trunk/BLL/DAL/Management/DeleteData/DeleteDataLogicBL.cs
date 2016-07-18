using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.Management.DeleteData;
using Common;
//using Common.FileMgtService;
using DAL.Factory.Management;
using DAL.IDAL.Management.DeleteData;
using Model.General;
using Model.Management.DeleteData;
using ServiceManage.FilesManage;

namespace BLL.DAL.Management.DeleteData
{
    class DeleteDataLogicBL : IDeleteDataLogicBL
    {
        IDeleteDataLogicDA _deleteDataLogicDA;
        AbstracFilesManage _fileManage;

        public DeleteDataLogicBL()
        {
            this._deleteDataLogicDA = MasterDAFactory.GetDAL<IDeleteDataLogicDA>(MasterDAFactory.DeleteDataLogic_ddl);
            try
            {
                this._fileManage = FilesManageFactory.GetFilesManage();
            }
            catch (Exception)
            { }
        }

        #region IDeleteDataLogicBL Members

        public Model.General.ReturnValueInfo DeleteData(DeleteDataLogic_ddl_Info info)
        {
            Model.General.ReturnValueInfo returnValue = new Model.General.ReturnValueInfo();
            switch (info.ddl_TableName)
            {
                case Common.DefineConstantValue.MasterType.SchoolMaster:
                    break;
                case Common.DefineConstantValue.MasterType.SpecialtyMaster:
                    break;
                case Common.DefineConstantValue.MasterType.BuildingMaster:
                    break;
                case Common.DefineConstantValue.MasterType.DepartmentMaster:
                    break;
                case Common.DefineConstantValue.MasterType.SiteMaster:
                    break;
                case Common.DefineConstantValue.MasterType.CourseMaster:
                    break;
                case Common.DefineConstantValue.MasterType.WinFormSkin:
                    break;
                case Common.DefineConstantValue.MasterType.CodeMaster_Key1:
                    break;
                case Common.DefineConstantValue.MasterType.CodeMaster_Key2:
                    break;
                default:
                    break;
            }

            return returnValue;
        }



        #endregion
    }
}

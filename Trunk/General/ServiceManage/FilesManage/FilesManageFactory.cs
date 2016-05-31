using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.FileMgtService;

namespace ServiceManage.FilesManage
{
    public class FilesManageFactory
    {

      public static readonly string _FilesManage = "ServiceManage.WebService.FilesManage,ServiceManage";

        static AbstracFilesManage _filesManageObject = null;

        /// <summary>
        /// 獲得文件管理實體類
        /// </summary>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static AbstracFilesManage GetFilesManage()
        {
            if (_filesManageObject == null)
            {
                //動態創建實例類型 
                try
                {
                    Type accessorType = Type.GetType(_FilesManage, false);
                    return (AbstracFilesManage)Activator.CreateInstance(accessorType, new object[] { });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return _filesManageObject;
            }

        }
    }
}

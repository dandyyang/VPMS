using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;
using Model.IModel;
using Model.Base;

namespace BLL.IBLL
{
    public interface IMainBL
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        IModelObject DisplayRecord(IModelObject itemEntity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MDobject"></param>
        /// <returns></returns>
        List<IModelObject> SearchRecords(IModelObject itemEntity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="EditMode"></param>
        /// <returns></returns>
        ReturnValueInfo Save(IModelObject itemEntity,  Common.DefineConstantValue.EditStateEnum EditMode);

 
    }
}

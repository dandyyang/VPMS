using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;
using Common;

namespace BLL.IBLL.SysMaster
{
    public interface IArticleTypeDefineBL :  IMainBL, IExtraBL
    {

        /// <summary>
        /// 取得该项下一级所有子项
        /// </summary>
        /// <param name="ArticleTypeInfo"></param>
        /// <returns></returns>
         List<ArticleTypeDefine_atd_Info> GetAllChildren(ArticleTypeDefine_atd_Info ArticleTypeInfo);

        /// <summary>
        /// 取得树头
        /// </summary>
        /// <returns></returns>
         ArticleTypeDefine_atd_Info GetTreeRoot(string codeMasterDefineKey2);



    }
}

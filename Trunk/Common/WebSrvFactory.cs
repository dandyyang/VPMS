using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.FileMgtService;



namespace Common
{
    public class WebSrvFactory
    {
        /// <summary>
        /// 获取网络文件管理SERVICE
        /// </summary>
        /// <returns></returns>
        public static FileMgtSoapClient GetFileMgt()
        {

            FileMgtSoapClient soap = new FileMgtSoapClient();
            // soap.Endpoint.Address = address;
            return soap;
        }
    }
}

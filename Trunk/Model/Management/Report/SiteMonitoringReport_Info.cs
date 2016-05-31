using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
    public class SiteMonitoringReport_Info
    {
        public SiteMonitoringReport_Info()
        {
            _site = string.Empty;
            _specialtyNum = string.Empty;
            _graduationPeriod = string.Empty;
            _class = string.Empty;
            _userIdentity = string.Empty;
            _userNum = string.Empty;
            _userName = string.Empty;

            _passType = string.Empty;
            _isData = string.Empty;
            _cardID = string.Empty;

            _sum = 1000;

            #region 2011-10-11
            //rgm_cMonitoringLocation = string.Empty;
            //ClassName = string.Empty;
            //cus_cChaName = string.Empty;
            //cus_cNumber = string.Empty;
            //Cad_cPassType = string.Empty;
            //rcm_cCard24GID = string.Empty;
            #endregion
        }

        #region 2011-10-11
        //public string rgm_cMonitoringLocation { set; get; }
        //public string ClassName { set; get; }
        //public string cus_cNumber { set; get; }
        //public string cus_cChaName { set; get; }
        //public DateTime? cad_dRecordDateTime { set; get; }
        //public string Cad_cPassType { set; get; }
        //public string rcm_cCard24GID { set; get; }
        //public DateTime? LastDateTime { set; get; }
        #endregion


        /// <summary>
        /// 显示总条数

        /// </summary>
        public int _sum { set; get; }

        /// <summary>
        /// 地点
        /// </summary>
        public string _site { set; get; }
        /// <summary>
        /// 专业
        /// </summary>
        public string _specialtyNum { set; get; }
        /// <summary>
        /// 届别
        /// </summary>
        public string _graduationPeriod { set; get; }
        /// <summary>
        /// 班级
        /// </summary>
        public string _class { set; get; }
        /// <summary>
        /// 用户身份
        /// </summary>
        public string _userIdentity { set; get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string _userNum { set; get; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string _userName { set; get; }
        /// <summary>
        /// 开始时间

        /// </summary>
        public DateTime? _startTme { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? _endTime { set; get; }
        /// <summary>
        /// 经过时间
        /// </summary>
        public DateTime? _passTime { set; get; }
        /// <summary>
        /// 出入类型
        /// </summary>
        public string _passType { set; get; }
        /// <summary>
        /// 存在出入数据
        /// </summary>
        public string _isData { set; get; }
        /// <summary>
        /// 卡ID
        /// </summary>
        public string _cardID { set; get; }
        /// <summary>
        /// 最后读取时间

        /// </summary>
        public DateTime? _lastDataTime { set; get; }
    }
}

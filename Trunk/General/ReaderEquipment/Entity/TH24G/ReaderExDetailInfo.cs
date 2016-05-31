using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.TH24G
{
    /// <summary>
    /// 读写器故障明细
    /// </summary>
    public class ReaderExDetailInfo
    {
        public ReaderExDetailInfo()
        {
            this.Ex_dtLastRunTime = DateTime.MinValue;
            this.Ex_dtLastTimeChk = DateTime.MinValue;
            this.Ex_dtRvData = DateTime.Now;
            this.Ex_dtRvDataFormatChk = DateTime.MinValue;
            this.CurrentStatus = EnumEquipmentStatus.Disconneted;
        }

        public int Ex_MacID { get; set; }

        /// <summary>
        /// 机器当前状态
        /// </summary>
        public EnumEquipmentStatus CurrentStatus { get; set; }

        #region ***检查设备返回的数据是否持续异常***

        /// <summary>
        /// 是否已发送返回数据全部异常警告的标识
        /// </summary>
        public bool Ex_lIsSendInvalidDataWarning;
        /// <summary>
        /// 接收到异常数据的错误重试累加器
        /// </summary>
        //public int Ex_iCounterInvalidData;
        /// <summary>
        /// 数据完整性检查通过的最后时间
        /// </summary>
        public DateTime Ex_dtRvDataFormatChk;

        #endregion

        #region ***检查是否无数据返回相关***

        /// <summary>
        /// 最后有接收到返回数据的时间
        /// </summary>
        public DateTime Ex_dtRvData;
        /// <summary>
        /// 是否已发送无数据返回警告的标识
        /// </summary>
        public bool Ex_lIsSendNullDataWarning;
        /// <summary>
        /// 检查无数据返回的计数器
        /// </summary>
        public short Ex_iCounterRvData;

        #endregion

        #region  ***获取时间自检相关***

        /// <summary>
        /// 是否已发送当日机器时间值异常警告的标识
        /// </summary>
        public bool Ex_lIsSendTimeChkWarning;
        /// <summary>
        /// 时间自检重试计数器
        /// </summary>
        public short Ex_iCounterTimeChk;
        /// <summary>
        /// 最后自检获取机器时间的成功时间
        /// </summary>
        public DateTime Ex_dtLastTimeChk;
        /// <summary>
        /// 最后运行自检获取机器时间的日期
        /// </summary>
        public DateTime Ex_dtLastRunTime;

        #endregion

        /// <summary>
        /// 重置所有状态标识值
        /// </summary>
        public void ResetAllSign()
        {
            this.CurrentStatus = EnumEquipmentStatus.Disconneted;
            this.Ex_dtLastRunTime = DateTime.MinValue;
            this.Ex_dtLastTimeChk = DateTime.MinValue;
            this.Ex_dtRvData = DateTime.MinValue;
            this.Ex_dtRvDataFormatChk = DateTime.MinValue;
            this.Ex_iCounterRvData = 0;
            this.Ex_iCounterTimeChk = 0;
            this.Ex_lIsSendInvalidDataWarning = false;
            this.Ex_lIsSendNullDataWarning = false;
            this.Ex_lIsSendTimeChkWarning = false;
        }

        /// <summary>
        /// 自检状态下重置其他标识值
        /// </summary>
        public void ResetSign()
        {
            //this.CurrentStatus = EnumEquipmentStatus.Disconneted;
            //this.Ex_dtLastRunTime = DateTime.MinValue;
            //this.Ex_dtLastTimeChk = DateTime.MinValue;
            this.Ex_dtRvData = DateTime.MinValue;
            this.Ex_dtRvDataFormatChk = DateTime.MinValue;
            this.Ex_iCounterRvData = 0;
            //this.Ex_iCounterTimeChk = 0;
            this.Ex_lIsSendInvalidDataWarning = false;
            this.Ex_lIsSendNullDataWarning = false;
            //this.Ex_lIsSendTimeChkWarning = false;
        }
    }
}

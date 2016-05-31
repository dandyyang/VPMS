using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentEquipment.IDevice
{
    public abstract class AbstractSubsidizeDevice
    {
        protected AbstractSubsidizeDevice()
        {
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="iCom">COM</param>
        /// <param name="iBaud">波特率</param>
        /// <returns></returns>
        public abstract bool Conn(int iCom, int iBaud);

        /// <summary>
        /// COM口号
        /// </summary>
        public int ComPort { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 设置管理卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public abstract bool SetAdminCard(int p_intDeviceNo, int p_intCardNo);

        /// <summary>
        /// 设置机号
        /// </summary>
        /// <param name="p_intOldDeviceNo"></param>
        /// <param name="p_intNewDeviceNo"></param>
        /// <returns></returns>
        public abstract bool SetDeviceNo(int p_intOldDeviceNo, int p_intNewDeviceNo);

        /// <summary>
        /// 同步机器时间
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract bool SyncDeviceDateTime(int p_intDeviceNo);

        /// <summary>
        /// 取得机器时间
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract DateTime GetDeviceDateTiem(int p_intDeviceNo);

        /// <summary>
        /// 设置最大消费金额
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intMoney"></param>
        /// <returns></returns>
        public abstract bool SetMaxPayMoney(int p_intDeviceNo, int p_intMoney);

        /// <summary>
        /// 设置机器消费密码
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_strOldPassword"></param>
        /// <param name="p_strNewPassword"></param>
        /// <returns></returns>
        public abstract bool SetDevicePassword(int p_intDeviceNo, string p_strOldPassword, string p_strNewPassword);

        /// <summary>
        /// 清空补贴资料
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract bool CleanSubsidizeInfo(int p_intDeviceNo);

        /// <summary>
        /// 下传补贴资料
        /// </summary>
        /// <param name="p_intDeviceNo">机号</param>
        /// <param name="p_intCardNo">卡号</param>
        /// <param name="p_intMoney">金额</param>
        /// <param name="p_intGroup">补贴组</param>
        /// <param name="p_intNo">补贴编号</param>
        /// <returns></returns>
        public abstract bool AddSubsidizeInfo(int p_intDeviceNo, int p_intCardNo, int p_intMoney, int p_intGroup, int p_intNo);

        /// <summary>
        /// 设置清零日期
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_datExpiry"></param>
        /// <returns></returns>
        public abstract bool SetExpiryDate(int p_intDeviceNo, DateTime p_datExpiry);

    }
}

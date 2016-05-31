using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.Entity;

namespace PaymentEquipment.IDevice
{
    /// <summary>
    /// 消费机抽象类
    /// </summary>
    public abstract class AbstractPayDevice
    {
        protected AbstractPayDevice()
        {
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="iCom">COM口号</param>
        /// <param name="iBaud">波特率</param>
        /// <returns></returns>
        public abstract bool Conn(int iCom, int iBaud);

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <returns></returns>
        public abstract bool Conn();

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
        public abstract DateTime GetDeviceDateTime(int p_intDeviceNo);

        /// <summary>
        /// 添加挂失卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public abstract bool AddLossCard(int p_intDeviceNo, int p_intCardNo);

        /// <summary>
        /// 移除挂失卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public abstract bool RemoveLossCard(int p_intDeviceNo, int p_intCardNo);

        /// <summary>
        /// 清除挂失卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract bool CleanLossCard(int p_intDeviceNo);

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
        /// <param name="p_intDeviceNo">机号</param>
        /// <param name="p_strOldPassword">旧密码</param>
        /// <param name="p_strNewPassword">新密码</param>
        /// <returns></returns>
        public abstract bool SetDevicePassword(int p_intDeviceNo, string p_strOldPassword, string p_strNewPassword);

        /// <summary>
        /// 取得消费记录条数
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract int GetPaymentNum(int p_intDeviceNo);

        /// <summary>
        /// 取得消费总金额
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract double GetPaymentMoney(int p_intDeviceNo);

        /// <summary>
        /// 取得消费资料
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract List<PaymentInfo> GetPaymentInfo(int p_intDeviceNo);

        /// <summary>
        /// 清除消费资料
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public abstract bool CleanPaymentInfo(int p_intDeviceNo);

        /// <summary>
        /// 串口
        /// </summary>
        public int ComPort
        {
            set;
            get;
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            set;
            get;
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            set;
            get;
        }

        /// <summary>
        /// 机器编号
        /// </summary>
        public int MachinesNum
        {
            set;
            get;
        }

    }
}

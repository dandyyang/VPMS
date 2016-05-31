using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;
using PaymentEquipment.DLL;
using PaymentEquipment.Funtion;

namespace PaymentEquipment.DeviceImplement
{
    public class LeoJocatSubsidizeDevice : AbstractSubsidizeDevice
    {
        public LeoJocatSubsidizeDevice()
        { }


        public override bool Conn(int iCom, int iBaud)
        {
            return true;
        }

        /// <summary>
        /// 设置管理卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public override bool SetAdminCard(int p_intDeviceNo, int p_intCardNo)
        {
            return JocatDevice.SetAdminCard(this.ComPort, this.BaudRate, p_intDeviceNo, p_intCardNo);
        }        

        /// <summary>
        /// 设置机号
        /// </summary>
        /// <param name="p_intOldDeviceNo"></param>
        /// <param name="p_intNewDeviceNo"></param>
        /// <returns></returns>
        public override bool SetDeviceNo(int p_intOldDeviceNo, int p_intNewDeviceNo)
        {
            return JocatDevice.SetMachineNo(this.ComPort, this.BaudRate, p_intOldDeviceNo, p_intNewDeviceNo);
        }

        /// <summary>
        /// 同步机器时间
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override bool SyncDeviceDateTime(int p_intDeviceNo)
        {
            return JocatDevice.SetMachineTime(this.ComPort, this.BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 取得机器时间
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override DateTime GetDeviceDateTiem(int p_intDeviceNo)
        {
            string l_strDate = JocatDevice.GetMachineTime(this.ComPort, this.BaudRate, p_intDeviceNo);
            return DateTime.Now;
        }

        /// <summary>
        /// 设置最大消费金额
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intMoney"></param>
        /// <returns></returns>
        public override bool SetMaxPayMoney(int p_intDeviceNo, int p_intMoney)
        {
            return JocatDevice.SetPaymentMaxAmount(this.ComPort, this.BaudRate, p_intDeviceNo, p_intMoney);
        }

        /// <summary>
        /// 设置机器密码
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_strOldPassword"></param>
        /// <param name="p_strNewPassword"></param>
        /// <returns></returns>
        public override bool SetDevicePassword(int p_intDeviceNo, string p_strOldPassword, string p_strNewPassword)
        {
            return JocatDevice.SetMachinePassword(this.ComPort, this.BaudRate, p_intDeviceNo, p_strOldPassword, p_strNewPassword);
        }

        /// <summary>
        /// 清空补贴资料
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override bool CleanSubsidizeInfo(int p_intDeviceNo)
        {
            return JocatDevice.CleanRechargeInfo(this.ComPort, this.BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 下传补贴资料
        /// </summary>
        /// <param name="p_intDeviceNo">机号</param>
        /// <param name="p_intCardNo">卡号</param>
        /// <param name="p_intMoney">金额</param>
        /// <param name="p_intGroup">补贴组</param>
        /// <param name="p_intNo">补贴编号</param>
        /// <returns></returns>
        public override bool AddSubsidizeInfo(int p_intDeviceNo, int p_intCardNo, int p_intMoney, int p_intGroup, int p_intNo)
        {
            return JocatDevice.SetRechargeInfo(this.ComPort, this.BaudRate, p_intDeviceNo, p_intGroup, p_intNo, p_intCardNo, p_intMoney);
        }

        /// <summary>
        /// 设置清零日期
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_datExpiry"></param>
        /// <returns></returns>
        public override bool SetExpiryDate(int p_intDeviceNo, DateTime p_datExpiry)
        {
            return JocatDevice.SetExpiryDate(this.ComPort, this.BaudRate, p_intDeviceNo, p_datExpiry.ToString("yyMMdd"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;
using System.Reflection;
using System.Configuration;

namespace PaymentEquipment.DeviceFactory
{
    public sealed class PaymentSubsidizedFactory
    {
        /// <summary>
        /// 获取水表设备实现类
        /// </summary>
        /// <param name="p_intComPort">连接COM口号</param>
        /// <param name="p_intBaudRate">波特率</param>
        /// <returns></returns>
        public static AbstractSubsidizeDevice CreateDevice()
        {
            try
            {
                string strClassName = ConfigurationSettings.AppSettings["PaymentSubsidizeDeviceLab"];
                if (string.IsNullOrEmpty(strClassName))
                {
                    return null;
                }

                Type accessorType = Type.GetType(strClassName, false);
                return (AbstractSubsidizeDevice)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);
                return null;
            }
        }
    }
}

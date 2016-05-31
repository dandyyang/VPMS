using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;
using System.Reflection;

namespace PaymentEquipment.DeviceFactory
{
    public sealed class PaymentDeviceFactory
    {
        /// <summary>
        /// 优卡特
        /// </summary>
        public static string Jocat = "PaymentEquipment.DeviceImplement.LeoJocatPayDevice";

        /// <summary>
        /// 获取消费设备实现类（需配置配置文件节点"PaymentDeviceLab"，"PaymentDeviceLab"为类的全命名路径）
        /// </summary>
        /// <returns></returns>
        public static AbstractPayDevice CreateDevice()
        {
            string strClassName = ConfigurationSettings.AppSettings["PaymentDeviceLab"];

            AbstractPayDevice payDevice = null;

            try
            {
                payDevice=CreateDevice(strClassName);
            }
            catch (Exception ex)
            {
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);

                throw ex;
            }

            return payDevice;
        }

        /// <summary>
        /// 获取消费设备实现类
        /// </summary>
        /// <param name="accessorFullName">实现类的完整路径</param>
        /// <returns></returns>
        public static AbstractPayDevice CreateDevice(string accessorFullName)
        {
            if (string.IsNullOrEmpty(accessorFullName))
            {
                return null;
            }

            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (AbstractPayDevice)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);

                throw ex;
            }
        }

    }
}

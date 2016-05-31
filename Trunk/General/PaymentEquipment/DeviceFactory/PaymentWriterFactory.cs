using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;
using System.Reflection;
using System.Configuration;

namespace PaymentEquipment.DeviceFactory
{
    public sealed class PaymentWriterFactory
    {
        /// <summary>
        /// 获得读写器实现类
        /// </summary>
        /// <param name="p_intComPort">连接COM口号</param>
        /// <param name="p_intSectorNo">需访问的扇区号</param>
        /// <param name="strPwd">扇区密码</param>
        /// <returns></returns>
        public static AbstractICWriter CreateWriter()
        {
            try
            {
                string strClassName = ConfigurationSettings.AppSettings["PaymentWriterLab"];
                if (string.IsNullOrEmpty(strClassName))
                {
                    return null;
                }

                Type accessorType = Type.GetType(strClassName, false);
                return (AbstractICWriter)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);
                return null;
            }

        }
    }
}

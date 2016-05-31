using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace PaymentEquipment.Enum
{
    public class PaymentEnumMaster : DefineConstantValue
    {
        /// <summary>
        /// 功能枚举
        /// </summary>
        public enum enmFunctionType
        {
            /// <summary>
            /// 打折功能
            /// </summary>
            Discount = 0,
            /// <summary>
            /// 限机号功能
            /// </summary>
            LimitedMachine = 1,
            /// <summary>
            /// 限额功能
            /// </summary>
            LimitedMoney = 2,
            /// <summary>
            /// 限次功能
            /// </summary>
            LimitedTimes = 3,
            /// <summary>
            /// 保留最低余额功能
            /// </summary>
            MinRemainderKeep = 4,
            /// <summary>
            /// 密码消费功能
            /// </summary>
            WithPosPwd = 5,
            /// <summary>
            /// 二次消费功能
            /// </summary>
            MultipleUse = 6,
            /// <summary>
            /// 补贴功能
            /// </summary>
            SubsidizeUse = 7,
            /// <summary>
            /// 时间间隔功能
            /// </summary>
            IntervalUse = 8,
            /// <summary>
            /// 时间段功能
            /// </summary>
            TimeSpanUse = 9
        }
        /// <summary>
        /// 获取功能名称
        /// </summary>
        /// <param name="funcType">功能代码</param>
        /// <returns></returns>
        public static string GetFunctionName(enmFunctionType funcType)
        {
            int iFuncName = (int)funcType;
            switch (iFuncName)
            {
                case (int)enmFunctionType.Discount:
                    {
                        return "打折消费";
                    }
                case (int)enmFunctionType.IntervalUse:
                    {
                        return "启用时间间隔";
                    }
                case (int)enmFunctionType.LimitedMachine:
                    {
                        return "限制机号";
                    }
                case (int)enmFunctionType.LimitedMoney:
                    {
                        return "每日限额";
                    }
                case (int)enmFunctionType.LimitedTimes:
                    {
                        return "限次";
                    }
                case (int)enmFunctionType.MinRemainderKeep:
                    {
                        return "保留最低余额";
                    }
                case (int)enmFunctionType.MultipleUse:
                    {
                        return "二次消费";
                    }
                case (int)enmFunctionType.SubsidizeUse:
                    {
                        return "补贴消费";
                    }
                case (int)enmFunctionType.TimeSpanUse:
                    {
                        return "时段消费";
                    }
                case (int)enmFunctionType.WithPosPwd:
                    {
                        return "启用消费密码";
                    }
                default:
                    break;
            }
            return string.Empty;
        }

        #region 宇川一卡通设备相关关键字

        /// <summary>
        /// 宇川消费机接收Socket数据的方式
        /// </summary>
        public enum YCPOS_EnumSocketRecType
        {
            /// <summary>
            /// 字符串模式
            /// </summary>
            Type_String = 0,
            /// <summary>
            /// Byte数组模式
            /// </summary>
            Type_ByteArray = 1,
            /// <summary>
            /// 无返回模式
            /// </summary>
            Type_Null = 2
        }

        /// <summary>
        /// 设置正确(宇川消费机)
        /// </summary>
        public static readonly string YC_POS_SET_OK = "SET_OK";

        /// <summary>
        /// 设置服务器IP成功
        /// </summary>
        public static readonly string YC_POS_SET_IP_OK = "SET_IP_OK";

        /// <summary>
        /// 设置网关成功
        /// </summary>
        public static readonly string YC_POS_SET_NET_GW_OK = "SET_NET_GW_OK";

        #endregion
    }
}

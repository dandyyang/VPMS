using System;
using System.Collections.Generic;
using System.Text;
using PaymentEquipment.Enum;

namespace PaymentEquipment.Entity
{
    public class PaymentCard
    {
        /// <summary>
        /// 卡片出厂编号
        /// </summary>
        public string MACID
        {
            get;
            set;
        }

        /// <summary>
        /// 卡片编号
        /// </summary>
        public int No
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Valid
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime? ExpiryDate
        {
            get;
            set;
        }

        /// <summary>
        /// 功能
        /// </summary>
        /// <remarks>        
        /// </remarks>
        public string FunctionCode
        {
            get;
            set;
        }

        /// <summary>
        /// 最低余额
        /// </summary>
        public decimal LimitRemainder
        {
            get;
            set;
        }

        /// <summary>
        /// 消费密码
        /// </summary>
        public string PaymentPassword
        {
            get;
            set;
        }

        /// <summary>
        /// 每天最大消费额
        /// </summary>
        public decimal DailyMaxPayment
        {
            get;
            set;
        }

        /// <summary>
        /// 第一时段消费限次
        /// </summary>
        public int Span1PaymentCount
        {
            get;
            set;
        }

        /// <summary>
        /// 第二时段消费限次
        /// </summary>
        public int Span2PaymentCount
        {
            get;
            set;
        }

        /// <summary>
        /// 第三时段消费限次
        /// </summary>
        public int Span3PaymentCount
        {
            get;
            set;
        }

        /// <summary>
        /// 第四时段消费限次
        /// </summary>
        public int Span4PaymentCount
        {
            get;
            set;
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 个人金额
        /// </summary>
        public decimal Money
        {
            get;
            set;
        }

        /// <summary>
        /// 个人金额备份
        /// </summary>
        public decimal MoneyBackup
        {
            get;
            set;
        }

        /// <summary>
        /// 补贴金额
        /// </summary>
        public decimal Subsidize
        {
            get;
            set;
        }

        /// <summary>
        /// 补贴金额备份
        /// </summary>
        public decimal SubsidizeBackup
        {
            get;
            set;
        }

        /// <summary>
        /// 可用机台清单
        /// </summary>
        public string MachineList
        {
            get;
            set;
        }

        /// <summary>
        /// 亲情号码
        /// </summary>
        public List<string> FamilyPhoneNum
        {
            get;
            set;
        }

        /// <summary>
        /// 亲情号码备注
        /// </summary>
        public string FamilyPhoneInfo
        {
            get;
            set;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Master
{
    public class CardUserPhoneNumMaster_cup_Info
    {
        public CardUserPhoneNumMaster_cup_Info()
        {
            this.cup_CardUserNum = string.Empty;
            this.cup_ID = 0;
            this.cup_Phone1 = string.Empty;
            this.cup_Phone2 = string.Empty;
            this.cup_Phone3 = string.Empty;
            this.cup_Phone4 = string.Empty;
            this.data1 = string.Empty;
            this.data2 = string.Empty;
        }

        /// <summary>
        /// 自增ID
        /// </summary>
        public int cup_ID { get; set; }

        /// <summary>
        /// 卡用户编号
        /// </summary>
        public string cup_CardUserNum { get; set; }

        /// <summary>
        /// 亲情号码一
        /// </summary>
        public string cup_Phone1 { get; set; }

        /// <summary>
        /// 亲情号码二
        /// </summary>
        public string cup_Phone2 { get; set; }

        /// <summary>
        /// 亲情号码三
        /// </summary>
        public string cup_Phone3 { get; set; }

        /// <summary>
        /// 亲情号码四
        /// </summary>
        public string cup_Phone4 { get; set; }

        /// <summary>
        /// 备用字段一
        /// </summary>
        public string data1 { get; set; }

        /// <summary>
        /// 备用字段二
        /// </summary>
        public string data2 { get; set; }
    }
}

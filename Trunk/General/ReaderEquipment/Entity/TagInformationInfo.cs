using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderEquipment.Entity.TH24G;

namespace ReaderEquipment.Entity
{
    public class TagInformationInfo : C24GEntity
    {
        public TagInformationInfo()
        {
            this.TagID = string.Empty;
            this.ReadDatetime = DateTime.Now;
            this.TagExistFirstDatetime = DateTime.Now;
            this.RSSI = 0;
            this.Antenna = 0;
            this.IsRemark = false;
            this.ReaderKey = string.Empty;
            this.RecordIndex = string.Empty;
        }

        public TagInformationInfo GetTagInformationInfoIns(string strSource)
        {
            TagInformationInfo tagInfo = new TagInformationInfo();
            tagInfo.MachineID = Convert.ToInt32(strSource.Substring(2, 2), 16).ToString();//StrMacID
            try
            {
                tagInfo.ReaderKey = tagInfo.MachineID.ToString();
                tagInfo.States = strSource.Substring(4, 2);//StrStutasCode
                tagInfo.IDNum = strSource.Substring(8, 8);//StrID
                tagInfo.RecordIndex = strSource.Substring(16, 4);//StrRecordIndex
                tagInfo.TabNum = tagInfo.RecordIndex;
                tagInfo.Keyboard = strSource.Substring(20, 2);//StrTagKey
                tagInfo.Power = strSource.Substring(22, 2);//StrPowerValue
                tagInfo.TagID = strSource.Substring(24, 6);//StrTagID
                tagInfo.TagExistFirstDatetime = DateTime.Parse(ReaderGeneral.ChangetoDatebyHexstring(strSource.Substring(30, 12)));//StrFirstReadTime
                //this.StrValid = strSource.Substring(42, 2);
                tagInfo.ReadTimes = (int)Convert.ToInt64(strSource.Substring(44, 2), 16);//StrReadTimes
                tagInfo.ReadDatetime = DateTime.Parse(tagInfo.TagExistFirstDatetime.Date.ToShortDateString() + " " + ReaderGeneral.ChangetoDatebyHexstring(strSource.Substring(46, 6)));//StrLastReadTime
                tagInfo.CRC = strSource.Substring(52, 4);//StrCRC
                tagInfo.IsPowerLow = this.Power != "00" ? true : false;
                tagInfo.Antenna = 1;
            }
            catch (Exception ex)
            {
                if (tagInfo.MachineID != "0" || tagInfo.MachineID != "255")
                {
                    Exception exx = new Exception(ex.Message + "-" + tagInfo.MachineID);
                    throw exx;
                }
                throw ex;
            }
            return tagInfo;
        }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string RecordIndex { get; set; }

        /// <summary>
        /// 标签编号
        /// </summary>
        public string TagID { set; get; }

        /// <summary>
        /// 标签读取时间
        /// </summary>
        public DateTime ReadDatetime { set; get; }

        public int RSSI { set; get; }

        public int Antenna { set; get; }

        public bool IsRemark { set; get; }

        /// <summary>
        /// 标签首次读到时间
        /// </summary>
        public DateTime TagExistFirstDatetime { set; get; }

        public string ReaderKey { set; get; }

        /// <summary>
        /// 读写器的机器号
        /// </summary>
        public string MachineID { get; set; }

    }
}

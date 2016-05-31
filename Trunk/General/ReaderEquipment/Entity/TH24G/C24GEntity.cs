using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.TH24G
{
    public class C24GEntity : EventArgs
    {

        public C24GEntity()
        {
            this.TabNum = "";
            this.BegFlg = "";
            this.Address = "";
            this.States = "";
            this.DataLength = "";
            this.Keyboard = "";
            this.Power = "";
            this.IDNum = "";
            this.CRC = "";
            this.EndFlg = "";
            this.TabNum = "";
            this.IsActiveTab = false;
            this.OptionalValue = "";
            this.ReadTimes = 0;
            this.IsPowerLow = false;
        }


        public string BegFlg { get; set; } //禎頭
        public string Address { get; set; } //地址碼

        public string States { get; set; } // 狀態

        public string DataLength { get; set; }// 數據長度
        public string Keyboard { get; set; } //按鍵
        public string Power { get; set; }//电量值
        public bool IsPowerLow { get; set; } //電量低狀態

        public string IDNum { get; set; } //ID號

        public string CRC { get; set; } //CRC-CCIT16校驗 
        public string EndFlg { get; set; } //禎尾
        public string TabNum { get; set; }//整個編號

        public bool IsActiveTab { get; set; } //是否有效Tab
        public string OptionalValue { get; set; } //選項按鈕
        public int ReadTimes { get; set; } //讀取次數



    }
}

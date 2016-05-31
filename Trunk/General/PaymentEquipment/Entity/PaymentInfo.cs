using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentEquipment.Entity
{
    public class PaymentInfo
    {
        public int RecordNo
        {
            get;
            set;
        }

        public int CardNo
        {
            get;
            set;
        }

        public int DeviceNo
        {
            get;
            set;
        }

        public DateTime PaymentDate
        {
            get;
            set;
        }

        public decimal PrivateMoney
        {
            get;
            set;
        }

        public decimal PayemtnMoney
        {
            get;
            set;
        }

        public decimal OtherMoney
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity
{
    public class ControllerInformationInfo
    {
        public long ControllerSN
        {
            get;
            set;
        }

        public string IPAddr
        {
            get;
            set;
        }

        public string newIPAddr
        {
            get;
            set;
        }

        public string MachineDate
        {
            get;
            set;
        }

        public string EntranceRecord
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }
        public string DoorNumber
        {
            get;
            set;
        }

        public long BeginIndex
        {
            get;
            set;
        }

        public long EndIndex
        {
            get;
            set;
        }

        public long ErrorCode
        {
            get;
            set;
        }
    }
}

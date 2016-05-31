using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderEquipment.Entity;

namespace ReaderEquipment.Entity
{
    public class TagInfoResultEventArgs : EventArgs
    {
        public TagInfoResultEventArgs()
        {
            this.TagInformationList = new List<TagInformationInfo>();
            this.TagInformation = new TagInformationInfo();
            string ExamineNum = string.Empty;
            int EQWId = 0;
        }

        public List<TagInformationInfo> TagInformationList
        {
            set;
            get;
        }

        public TagInformationInfo TagInformation
        {
            set;
            get;
        }

        public object InformationObject
        {
            set;
            get;
        }

        public string ExamineNum
        {
            set;
            get;
        }
        public int EQWId
        {
            set;
            get;
        }

        public string TagSourceInformation
        {
            set;
            get;
        }
    }
}

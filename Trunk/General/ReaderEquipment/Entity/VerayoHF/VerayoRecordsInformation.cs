using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.VerayoHF
{
    public class VerayoRecordsInformation:EventArgs
    {
        public VerayoRecordsInformation()
        {
            this._recordsInformationList = new List<VerayoRecordInformation>();
            //this._recordInformation = new VerayoRecordInformation();
        }
        public List<VerayoRecordInformation> _recordsInformationList
        {
            get;
            set;
        }
        //public VerayoRecordInformation _recordInformation
        //{
        //    get;
        //    set;
        //}
    }
}

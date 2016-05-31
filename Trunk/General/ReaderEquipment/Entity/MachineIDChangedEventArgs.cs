using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity
{
    public class MachineIDChangedEventArgs : EventArgs
    {
        public int OldMacID { get; set; }
        public int NewMacID { get; set; }
    }
}

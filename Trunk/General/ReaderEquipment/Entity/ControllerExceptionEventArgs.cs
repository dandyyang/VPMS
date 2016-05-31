using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity
{
    public class ControllerExceptionEventArgs : EventArgs
    {
        public Exception ExceptionObject
        {
            get;
            set;
        }

        public ControllerExceptionEventArgs()
        {
            this.ExceptionObject = null;
        }
    }
}

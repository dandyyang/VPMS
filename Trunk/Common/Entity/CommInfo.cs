using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Entity
{
    public class CommInfo
    {
        public CommInfo()
        {
            this.CommPort = 0;
        }

        public short CommPort
        {
            set;
            get;
        }
    }
}

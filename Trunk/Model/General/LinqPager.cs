using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.General
{
   public class LinqPager
    {
       public int Start { get; set; }
       public int Limit { get; set; }
       public string Sort { get; set; }
       public string Dir { get; set; }
    }
}

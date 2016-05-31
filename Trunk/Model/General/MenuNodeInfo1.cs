using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.General
{
    public class MenuNodeInfo1
    {
        public MenuNodeInfo1()
        {
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Text = string.Empty;
            this.Index = 0;
            this.MenuNodeInfos1 = null;
        }

        public string Text
        {
            set;
            get;
        }

        public string Name
        {
            set;
            get;
        }

        public string Tag
        {
            set;
            get;
        }

        public int Index
        {
            set;
            get;
        }

        public MenuNodeInfo1[] MenuNodeInfos1
        {
            set;
            get;
        }
    }
}

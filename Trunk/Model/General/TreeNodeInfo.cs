using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.General
{
    public class TreeNodeInfo
    {
        public TreeNodeInfo()
        {
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Text = string.Empty;
            this.Index = 0;
            this.ImageIndex = 0;
            this.TreeNodeInfos = null;
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

        public int ImageIndex
        {
            set;
            get;
        }

        public string Remark
        {
            set;
            get;
        }

        public string FileName
        {
            set;
            get;
        }

        public string WorkingDirectory
        {
            set;
            get;
        }

        public TreeNodeInfo[] TreeNodeInfos
        {
            set;
            get;
        }
    }
}

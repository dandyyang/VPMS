using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace WindowUI.ClassLibrary.Public
{
    /// <summary>
    /// 用於ListView的欄位排序
    /// </summary>
    class ListViewItemComparer : IComparer
    {
        private int col;
        public ListViewItemComparer()
        {
            col = 0;
        }

        public ListViewItemComparer(int iColumn)
        {
            col = iColumn;
        }

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }

        #endregion
    }
}

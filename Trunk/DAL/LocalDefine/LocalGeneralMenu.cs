using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;

namespace DAL.LocalDefine
{
    class LocalGeneralMenu
    {
        public static readonly LocalGeneralMenu Instance = new LocalGeneralMenu();

        public LocalGeneralMenu()
        {

        }
        /// <summary>
        /// 插入一个树节点
        /// </summary>
        /// <param name="pTNode">父节点</param>
        /// <param name="strText">节点文字</param>
        /// <param name="tag">节点标示符</param>
        /// <param name="name">节点名称</param>
        /// <param name="index">节点索引号</param>
        /// <returns>TreeNode</returns>
        public TreeNodeInfo InsertTreeNode(TreeNodeInfo pTNode, string text, string tag, string name, int index, int imageIndex)
        {
            if (pTNode == null)
            {
                return null;
            }

            TreeNodeInfo[] tNodes = null;
            TreeNodeInfo tNode = new TreeNodeInfo();

            tNode.Text = text;
            tNode.Tag = tag;
            tNode.Name = name;
            tNode.Index = index;
            tNode.ImageIndex = imageIndex;

            tNodes = pTNode.TreeNodeInfos;
            List<TreeNodeInfo> tNodeList = null;
            if (tNodes != null)
            {
                tNodeList = tNodes.ToList<TreeNodeInfo>();
            }
            else
            {
                tNodeList = new List<TreeNodeInfo>();
            }
            tNodeList.Add(tNode);

            pTNode.TreeNodeInfos = tNodeList.ToArray();

            return tNode;
        }
    }
}
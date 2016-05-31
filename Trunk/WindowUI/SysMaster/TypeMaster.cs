using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowUI.ClassLibrary.Public;
using Common;
using BLL.IBLL.SysMaster;
using BLL.Factory.SysMaster;
using Model.SysMaster;
using Model.General;


namespace WindowUI.SysMaster
{
    public partial class TypeMaster : BaseForm
    {
        TextBox _txtcTypeNum;
        TextBox _txtcTypeName;
        CheckBox _chbValid;
        TextBox _txtcSysNum;

        string _currentNodeName;

        int _currentID;

        IArticleTypeDefineBL _articleTypeDefineBL;

        public TypeMaster()
        {
            InitializeComponent();

            this._articleTypeDefineBL = MasterBLLFactory.GetBLL<IArticleTypeDefineBL>(MasterBLLFactory.ArticleTypeDefine);

            this._txtcTypeName = new TextBox();
            this._txtcTypeName.Width = 140;

            this._txtcTypeNum = new TextBox();
            this._txtcTypeNum.Width = 80;

            this._txtcSysNum = new TextBox();
            this._txtcSysNum.Width = 60;


            this._chbValid = new CheckBox();
            this._chbValid.Text = "有效";
            this._chbValid.Width = 50;

            this._currentNodeName = string.Empty;

            this._currentID = 0;

            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
        }

        void SetControlStatus(DefineConstantValue.EditStateEnum editStatus)
        {
            switch (editStatus)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    this._txtcTypeName.TextBoxSetStatus(true);
                    this._txtcTypeNum.TextBoxSetStatus(true);
                    this._txtcSysNum.TextBoxSetStatus(true);

                    this.btnSave.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnAdd.Visible = true;
                    this.btnModify.Visible = true;
                    this.btnDeleted.Visible = true;

                    this.tvwTypeList.Enabled = true;
                    this._chbValid.Enabled = false;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    this._txtcTypeNum.TextBoxSetStatus(true);
                    this._txtcTypeName.TextBoxSetStatus(false);
                    this._txtcSysNum.TextBoxSetStatus(false);

                    this.btnSave.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnAdd.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnDeleted.Visible = false;

                    this.tvwTypeList.Enabled = false;
                    this._chbValid.Enabled = true;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    this._txtcTypeNum.TextBoxSetStatus(false);
                    this._txtcTypeName.TextBoxSetStatus(false);
                    this._txtcSysNum.TextBoxSetStatus(false);

                    this.btnSave.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnAdd.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnDeleted.Visible = false;

                    this.tvwTypeList.Enabled = false;
                    this._chbValid.Enabled = true;

                    break;
                default:

                    break;
            }
        }

        List<TreeNode> GetTreeNodeFullPaths(TreeNode parentNode)
        {
            List<TreeNode> nodes = null;
            if (parentNode != null)
            {
                if (parentNode.Parent != null)
                {
                    nodes = GetTreeNodeFullPaths(parentNode.Parent);
                }

                if (nodes == null)
                {
                    nodes = new List<TreeNode>();
                }

                nodes.Add(parentNode);

            }
            return nodes;
        }

        /// <summary>
        /// 显示类型节点的所有路径
        /// </summary>
        /// <param name="treeNodePaths">节点路径列表</param>
        /// <param name="isNewNode">是否要新增新节点</param>
        void ShowTreeNodeFullPath(List<TreeNode> treeNodePaths, bool isNewNode)
        {
            this.pnlType.Controls.Clear();

            if (treeNodePaths == null || treeNodePaths.Count < 1)
            {
                return;
            }

            TableLayoutPanel typeLayout = new TableLayoutPanel();
            int layoutColumnCount = treeNodePaths.Count + 4;

            if (isNewNode)
            {
                layoutColumnCount += 1;

            }
            typeLayout.ColumnCount = layoutColumnCount;

            typeLayout.AutoSize = true;
            typeLayout.Dock = DockStyle.Left;

            Label nodeText = null;

            if (treeNodePaths.Count > 1)
            {
                int count = 0;
                if (isNewNode)
                {
                    count = treeNodePaths.Count;
                }
                else
                {
                    count = treeNodePaths.Count - 1;
                }

                for (int i = 0; i < count; i++)
                {
                    nodeText = new Label();
                    nodeText.TextAlign = ContentAlignment.MiddleCenter;
                    nodeText.AutoSize = true;
                    nodeText.Dock = DockStyle.Fill;
                    nodeText.Text = treeNodePaths[i].Text + "-->";
                    typeLayout.Controls.Add(nodeText);
                }
            }

            if (isNewNode)
            {
                this._txtcTypeNum.Text = "";
                this._txtcTypeName.Text = "";
                this._txtcSysNum.Text = "";
            }
            else
            {
                string[] typeRecord = treeNodePaths[treeNodePaths.Count - 1].Text.Trim().Split(DefineConstantValue.SystemTypeMasterRecordNumAndNameSeparator.ToCharArray());

                if (typeRecord != null && typeRecord.Count() == 2)
                {
                    this._txtcTypeNum.Text = typeRecord[0].Trim();
                    this._txtcTypeName.Text = typeRecord[1].Trim();
                    this._txtcSysNum.Text = treeNodePaths[treeNodePaths.Count - 1].ToolTipText;
                }
                else
                {
                    this._txtcTypeNum.Text = "";
                    this._txtcTypeName.Text = "";
                    this._txtcSysNum.Text = "";
                    this._chbValid.Checked = true;

                }
            }

           
            typeLayout.Controls.Add(this._txtcTypeNum);
            typeLayout.Controls.Add(this._txtcTypeName);
            typeLayout.Controls.Add(this._chbValid);

            if (this.UserInformation.usm_cUserLoginID=="sa")
            {
                nodeText = new Label();
                nodeText.TextAlign = ContentAlignment.MiddleCenter;
                nodeText.AutoSize = true;
                nodeText.Dock = DockStyle.Fill;
                nodeText.Text = "系统编号：";
                typeLayout.Controls.Add(nodeText);
                typeLayout.Controls.Add(this._txtcSysNum); 
            }
            
            

            this.pnlType.Controls.Add(typeLayout);
        }

        /// <summary>
        /// 显示所选择的类型节点的所有路径
        /// </summary>
        /// <param name="node"></param>
        void DisplaySelectNodeFullPath(TreeNode node)
        {
            List<TreeNode> nodeFullPaths = null;

            nodeFullPaths = GetTreeNodeFullPaths(node);

            ShowTreeNodeFullPath(nodeFullPaths, false);
        }

        void SetAddRecordDefautInfo()
        {
            this._chbValid.Checked = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Insert);

            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;

            SetAddRecordDefautInfo();

            if (this.tvwTypeList.SelectedNode != null)
            {
                List<TreeNode> nodeFullPaths = null;

                nodeFullPaths = GetTreeNodeFullPaths(this.tvwTypeList.SelectedNode);

                ShowTreeNodeFullPath(nodeFullPaths, true);
            }

            this._txtcTypeNum.Focus();
        }

        private void tvwTypeList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private void tvwTypeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currentNodeName = e.Node.Name;

            _currentID = Convert.ToInt32(e.Node.Tag);

            DisplaySelectNodeFullPath(e.Node);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Update);

            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

            DisplaySelectNodeFullPath(this.tvwTypeList.SelectedNode);

            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            ArticleTypeDefine_atd_Info objInfo = new ArticleTypeDefine_atd_Info();

            objInfo.atd_cParentRecordNum = _currentNodeName;

            objInfo.atd_cTypeNum = this._txtcTypeNum.Text;

            objInfo.atd_cDescript = this._txtcTypeName.Text;

            

            objInfo.atd_lValid = this._chbValid.Checked;

            ReturnValueInfo returnInfo = new ReturnValueInfo();

            try
            {
                switch (this.EditState)
                {
                    case DefineConstantValue.EditStateEnum.OE_Insert:

                        returnInfo = _articleTypeDefineBL.Save(objInfo, DefineConstantValue.EditStateEnum.OE_Insert);

                        break;
                    case DefineConstantValue.EditStateEnum.OE_Update:

                        objInfo.atd_iRecordID = _currentID;

                        returnInfo = _articleTypeDefineBL.Save(objInfo, DefineConstantValue.EditStateEnum.OE_Update);

                        break;
                    case DefineConstantValue.EditStateEnum.OE_Delete:
                        break;
                    case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                        break;
                    default:
                        break;
                }

                if (returnInfo.boolValue)
                {
                    ShowInformationMessage("操作成功！");

                    tvwTypeList.Nodes.Clear();

                    LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE);

                    LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE);

                    LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_PROPERTY);

                    LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_LIABILITIES);

                    LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_NETASSETS);

                    SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                }
                else
                {
                    if (returnInfo.messageText == null)
                    {
                        ShowErrorMessage("操作失败！");
                    }
                    else
                    {
                        ShowWarningMessage(returnInfo.messageText);
                    }
                }
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex.Message);
            }
        }

        private void TypeMaster_Load(object sender, EventArgs e)
        {

            tvwTypeList.Nodes.Clear();

            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE);

            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE);

            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_PROPERTY);

            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_LIABILITIES);

            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_NETASSETS);
        }

        /// <summary>
        /// 加载树
        /// </summary>
        private void LoadTree(string codeMasterDefineKey2)
        {
            try
            {
                ArticleTypeDefine_atd_Info rootInfo = _articleTypeDefineBL.GetTreeRoot(codeMasterDefineKey2);

                if (rootInfo != null)
                {
                    TreeNode root = new TreeNode();

                    root.Text = rootInfo.atd_cDescript;

                    root.Name = rootInfo.atd_cTypeNum;

                    root.ToolTipText = rootInfo.atd_cSysNum;

                    LoadTreeNode(root);

                    tvwTypeList.Nodes.Add(root);

                    tvwTypeList.ExpandAll();
                }
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex.Message);
            }
        }

        /// <summary>
        /// 加载节点下的项
        /// </summary>
        /// <param name="node"></param>
        private void LoadTreeNode(TreeNode node)
        {
            if (node != null)
            {
                ArticleTypeDefine_atd_Info rootInfo = new ArticleTypeDefine_atd_Info();

                rootInfo.atd_cTypeNum = node.Name;

                List<ArticleTypeDefine_atd_Info> nodes = null;

                nodes = _articleTypeDefineBL.GetAllChildren(rootInfo);

                if (nodes != null)
                {
                    foreach (ArticleTypeDefine_atd_Info item in nodes)
                    {

                        TreeNode treeNode = new TreeNode();

                        treeNode.Text = item.atd_cTypeNum + "-" + item.atd_cDescript;

                        treeNode.Name = item.atd_cTypeNum;

                        treeNode.ToolTipText = item.atd_cSysNum;

                        treeNode.Tag = item.atd_iRecordID;

                        if (item.hasChild)
                        {
                            LoadTreeNode(treeNode);
                        }

                        node.Nodes.Add(treeNode);
                    }
                }

            }
        }

        private void btnDeleted_Click(object sender, EventArgs e)
        {
            DialogResult showResult;

            showResult = MessageBox.Show("确定删除该项及该项下所有子项？", "温馨提示", MessageBoxButtons.YesNo);

            if (showResult == DialogResult.Yes)
            {
                if (_currentNodeName != DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE && _currentNodeName!=DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE)
                {
                    this.EditState = DefineConstantValue.EditStateEnum.OE_Delete;

                    ReturnValueInfo returnInfo = new ReturnValueInfo();

                    ArticleTypeDefine_atd_Info objInfo = new ArticleTypeDefine_atd_Info();

                    objInfo.atd_iRecordID = _currentID;

                    try
                    {
                        returnInfo = _articleTypeDefineBL.Save(objInfo, DefineConstantValue.EditStateEnum.OE_Delete);

                        if (returnInfo.boolValue)
                        {
                            ShowInformationMessage("删除成功！");

                            tvwTypeList.Nodes.Clear();

                            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE);

                            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE);

                            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_PROPERTY);

                            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_LIABILITIES);

                            LoadTree(DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_NETASSETS);

                            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ShowWarningMessage("删除失败！");
                        }
                    }
                    catch (Exception Ex)
                    {

                        ShowErrorMessage(Ex.Message);
                    }
                }
                else 
                {
                    ShowWarningMessage("不能删除此项！");
                }
            }
        }

    }
}

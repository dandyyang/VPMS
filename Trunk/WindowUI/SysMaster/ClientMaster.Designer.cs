namespace WindowUI.SysMaster
{
    partial class ClientMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox gpbCommandBox;
        private System.Windows.Forms.Panel pnlSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlMoveButton;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Panel pnlCommand;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpbCommandBox = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlMoveButton = new System.Windows.Forms.Panel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtcTaxNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtcWebSite = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtcEnglishName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtcFax = new System.Windows.Forms.TextBox();
            this.txtcPhone = new System.Windows.Forms.TextBox();
            this.txtcAddress = new System.Windows.Forms.TextBox();
            this.txtcLinkman = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcRemark = new System.Windows.Forms.TextBox();
            this.txtcChinaName = new System.Windows.Forms.TextBox();
            this.txtcCNum = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtdAddDate = new System.Windows.Forms.TextBox();
            this.txtcAdd = new System.Windows.Forms.TextBox();
            this.txtdLastDate = new System.Windows.Forms.TextBox();
            this.txtcLast = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.userToolBar1 = new WindowControls.UserToolBar();
            this.gpbCommandBox.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.pnlMoveButton.SuspendLayout();
            this.pnlCommand.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbCommandBox
            // 
            this.gpbCommandBox.Controls.Add(this.btnSearch);
            this.gpbCommandBox.Controls.Add(this.pnlSave);
            this.gpbCommandBox.Controls.Add(this.pnlMoveButton);
            this.gpbCommandBox.Controls.Add(this.pnlCommand);
            this.gpbCommandBox.Controls.Add(this.btnExit);
            this.gpbCommandBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gpbCommandBox.Location = new System.Drawing.Point(0, 52);
            this.gpbCommandBox.Name = "gpbCommandBox";
            this.gpbCommandBox.Size = new System.Drawing.Size(10, 465);
            this.gpbCommandBox.TabIndex = 13;
            this.gpbCommandBox.TabStop = false;
            this.gpbCommandBox.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearch.Location = new System.Drawing.Point(5, 325);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "&F 搜寻";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlSave
            // 
            this.pnlSave.Controls.Add(this.btnCancel);
            this.pnlSave.Controls.Add(this.btnSave);
            this.pnlSave.Location = new System.Drawing.Point(5, 107);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(68, 67);
            this.pnlSave.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(3, 34);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&C 取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Location = new System.Drawing.Point(3, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&S 保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlMoveButton
            // 
            this.pnlMoveButton.Controls.Add(this.btnFirst);
            this.pnlMoveButton.Controls.Add(this.btnPrevious);
            this.pnlMoveButton.Controls.Add(this.btnNext);
            this.pnlMoveButton.Controls.Add(this.btnLast);
            this.pnlMoveButton.Location = new System.Drawing.Point(5, 180);
            this.pnlMoveButton.Name = "pnlMoveButton";
            this.pnlMoveButton.Size = new System.Drawing.Size(67, 120);
            this.pnlMoveButton.TabIndex = 17;
            // 
            // btnFirst
            // 
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst.Location = new System.Drawing.Point(3, 11);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(60, 23);
            this.btnFirst.TabIndex = 6;
            this.btnFirst.Text = "|<<";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrevious.Location = new System.Drawing.Point(3, 36);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(60, 24);
            this.btnPrevious.TabIndex = 7;
            this.btnPrevious.Text = "<<";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(3, 62);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 24);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = ">>";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast.Location = new System.Drawing.Point(3, 88);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(60, 23);
            this.btnLast.TabIndex = 9;
            this.btnLast.Text = ">>|";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // pnlCommand
            // 
            this.pnlCommand.Controls.Add(this.btnAdd);
            this.pnlCommand.Controls.Add(this.btnModify);
            this.pnlCommand.Controls.Add(this.btnDelete);
            this.pnlCommand.Location = new System.Drawing.Point(5, 17);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(67, 88);
            this.pnlCommand.TabIndex = 16;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(3, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&A 新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnModify.Location = new System.Drawing.Point(3, 32);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(60, 24);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "&M 修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Location = new System.Drawing.Point(3, 58);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "&D 删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(-24, 420);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 26);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "&X 退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(0, 30);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(882, 487);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtcTaxNumber);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtcWebSite);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtcEnglishName);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtcFax);
            this.groupBox1.Controls.Add(this.txtcPhone);
            this.groupBox1.Controls.Add(this.txtcAddress);
            this.groupBox1.Controls.Add(this.txtcLinkman);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtcRemark);
            this.groupBox1.Controls.Add(this.txtcChinaName);
            this.groupBox1.Controls.Add(this.txtcCNum);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(3, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(876, 339);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // txtcTaxNumber
            // 
            this.txtcTaxNumber.Location = new System.Drawing.Point(66, 91);
            this.txtcTaxNumber.Name = "txtcTaxNumber";
            this.txtcTaxNumber.Size = new System.Drawing.Size(350, 21);
            this.txtcTaxNumber.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label14.Location = new System.Drawing.Point(7, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 31;
            this.label14.Text = "企业税号:";
            // 
            // txtcWebSite
            // 
            this.txtcWebSite.Location = new System.Drawing.Point(66, 219);
            this.txtcWebSite.Name = "txtcWebSite";
            this.txtcWebSite.Size = new System.Drawing.Size(350, 21);
            this.txtcWebSite.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label13.Location = new System.Drawing.Point(7, 222);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "网址:";
            // 
            // txtcEnglishName
            // 
            this.txtcEnglishName.Location = new System.Drawing.Point(66, 65);
            this.txtcEnglishName.Name = "txtcEnglishName";
            this.txtcEnglishName.Size = new System.Drawing.Size(350, 21);
            this.txtcEnglishName.TabIndex = 14;
            this.txtcEnglishName.Leave += new System.EventHandler(this.txtcEnglishName_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label11.Location = new System.Drawing.Point(7, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "英文名称:";
            // 
            // txtcFax
            // 
            this.txtcFax.Location = new System.Drawing.Point(66, 193);
            this.txtcFax.Name = "txtcFax";
            this.txtcFax.Size = new System.Drawing.Size(350, 21);
            this.txtcFax.TabIndex = 19;
            // 
            // txtcPhone
            // 
            this.txtcPhone.Location = new System.Drawing.Point(66, 167);
            this.txtcPhone.Name = "txtcPhone";
            this.txtcPhone.Size = new System.Drawing.Size(350, 21);
            this.txtcPhone.TabIndex = 18;
            // 
            // txtcAddress
            // 
            this.txtcAddress.Location = new System.Drawing.Point(66, 141);
            this.txtcAddress.Name = "txtcAddress";
            this.txtcAddress.Size = new System.Drawing.Size(350, 21);
            this.txtcAddress.TabIndex = 17;
            // 
            // txtcLinkman
            // 
            this.txtcLinkman.Location = new System.Drawing.Point(66, 116);
            this.txtcLinkman.Name = "txtcLinkman";
            this.txtcLinkman.Size = new System.Drawing.Size(350, 21);
            this.txtcLinkman.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Location = new System.Drawing.Point(7, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "传真:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(7, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "电话:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(7, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "客户地址:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(7, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "联系人:";
            // 
            // txtcRemark
            // 
            this.txtcRemark.Location = new System.Drawing.Point(66, 245);
            this.txtcRemark.Multiline = true;
            this.txtcRemark.Name = "txtcRemark";
            this.txtcRemark.Size = new System.Drawing.Size(350, 71);
            this.txtcRemark.TabIndex = 21;
            // 
            // txtcChinaName
            // 
            this.txtcChinaName.Location = new System.Drawing.Point(66, 41);
            this.txtcChinaName.Name = "txtcChinaName";
            this.txtcChinaName.Size = new System.Drawing.Size(350, 21);
            this.txtcChinaName.TabIndex = 13;
            // 
            // txtcCNum
            // 
            this.txtcCNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcCNum.Location = new System.Drawing.Point(66, 15);
            this.txtcCNum.Name = "txtcCNum";
            this.txtcCNum.Size = new System.Drawing.Size(350, 21);
            this.txtcCNum.TabIndex = 12;
            this.txtcCNum.Leave += new System.EventHandler(this.txtcCNum_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label12.Location = new System.Drawing.Point(7, 250);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "备注:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户编号:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtdAddDate);
            this.groupBox4.Controls.Add(this.txtcAdd);
            this.groupBox4.Controls.Add(this.txtdLastDate);
            this.groupBox4.Controls.Add(this.txtcLast);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(5, 398);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(874, 84);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // txtdAddDate
            // 
            this.txtdAddDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdAddDate.Enabled = false;
            this.txtdAddDate.Location = new System.Drawing.Point(72, 49);
            this.txtdAddDate.Name = "txtdAddDate";
            this.txtdAddDate.Size = new System.Drawing.Size(125, 21);
            this.txtdAddDate.TabIndex = 7;
            // 
            // txtcAdd
            // 
            this.txtcAdd.BackColor = System.Drawing.SystemColors.Info;
            this.txtcAdd.Enabled = false;
            this.txtcAdd.Location = new System.Drawing.Point(72, 24);
            this.txtcAdd.Name = "txtcAdd";
            this.txtcAdd.Size = new System.Drawing.Size(125, 21);
            this.txtcAdd.TabIndex = 6;
            // 
            // txtdLastDate
            // 
            this.txtdLastDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtdLastDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdLastDate.Enabled = false;
            this.txtdLastDate.Location = new System.Drawing.Point(742, 51);
            this.txtdLastDate.Name = "txtdLastDate";
            this.txtdLastDate.Size = new System.Drawing.Size(125, 21);
            this.txtdLastDate.TabIndex = 5;
            // 
            // txtcLast
            // 
            this.txtcLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcLast.BackColor = System.Drawing.SystemColors.Info;
            this.txtcLast.Enabled = false;
            this.txtcLast.Location = new System.Drawing.Point(742, 26);
            this.txtcLast.Name = "txtcLast";
            this.txtcLast.Size = new System.Drawing.Size(125, 21);
            this.txtcLast.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label10.Location = new System.Drawing.Point(682, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "修改时间:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label9.Location = new System.Drawing.Point(682, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "修改者:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label8.Location = new System.Drawing.Point(13, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "新增时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(13, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "新增者:";
            // 
            // userToolBar1
            // 
            this.userToolBar1.AutoSetStatus = true;
            this.userToolBar1.BtnCancelEnabled = true;
            this.userToolBar1.BtnCancelVisible = true;
            this.userToolBar1.BtnCardIssuanceEnabled = true;
            this.userToolBar1.BtnCardIssuanceVisible = false;
            this.userToolBar1.BtnCardMissingEnabled = true;
            this.userToolBar1.BtnCardMissingVisible = false;
            this.userToolBar1.BtnCardRecoveryEnabled = true;
            this.userToolBar1.BtnCardRecoveryVisible = false;
            this.userToolBar1.BtnCardReturnEnabled = true;
            this.userToolBar1.BtnCardReturnVisible = false;
            this.userToolBar1.BtnCardScrapEnabled = true;
            this.userToolBar1.BtnCardScrapVisible = false;
            this.userToolBar1.BtnDataInputEnabled = true;
            this.userToolBar1.BtnDataInputVisible = false;
            this.userToolBar1.BtnDeleteEnabled = true;
            this.userToolBar1.BtnDeleteVisible = true;
            this.userToolBar1.BtnFirstEnabled = true;
            this.userToolBar1.BtnFirstVisible = true;
            this.userToolBar1.BtnLastEnabled = true;
            this.userToolBar1.BtnLastVisible = true;
            this.userToolBar1.BtnModifyEnabled = true;
            this.userToolBar1.BtnModifyVisible = true;
            this.userToolBar1.BtnNewEnabled = true;
            this.userToolBar1.BtnNewVisible = true;
            this.userToolBar1.BtnNextEnabled = true;
            this.userToolBar1.BtnNextVisible = true;
            this.userToolBar1.BtnPreviousEnabled = true;
            this.userToolBar1.BtnPreviousVisible = true;
            this.userToolBar1.BtnSaveEnabled = true;
            this.userToolBar1.BtnSaveVisible = true;
            this.userToolBar1.BtnSearchEnabled = true;
            this.userToolBar1.BtnSearchVisible = true;
            this.userToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.userToolBar1.Location = new System.Drawing.Point(0, 0);
            this.userToolBar1.Name = "userToolBar1";
            this.userToolBar1.RecordExistPosition = WindowControls.UserToolBar.RecordIndexType.None;
            this.userToolBar1.Size = new System.Drawing.Size(882, 33);
            this.userToolBar1.TabIndex = 16;
            this.userToolBar1.toolStripSeparator11Visible = true;
            this.userToolBar1.toolStripSeparator12Visible = true;
            this.userToolBar1.toolStripSeparator21Visible = true;
            this.userToolBar1.toolStripSeparator22Visible = true;
            // 
            // ClientMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(882, 517);
            this.Controls.Add(this.userToolBar1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gpbCommandBox);
            this.Name = "ClientMaster";
            this.TabText = "客户主档";
            this.Text = "客户主档";
            this.Load += new System.EventHandler(this.ClientMaster_Load);
            this.gpbCommandBox.ResumeLayout(false);
            this.pnlSave.ResumeLayout(false);
            this.pnlMoveButton.ResumeLayout(false);
            this.pnlCommand.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtcWebSite;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtcEnglishName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtcFax;
        private System.Windows.Forms.TextBox txtcPhone;
        private System.Windows.Forms.TextBox txtcAddress;
        private System.Windows.Forms.TextBox txtcLinkman;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcRemark;
        private System.Windows.Forms.TextBox txtcChinaName;
        private System.Windows.Forms.TextBox txtcCNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtdAddDate;
        private System.Windows.Forms.TextBox txtcAdd;
        private System.Windows.Forms.TextBox txtdLastDate;
        private System.Windows.Forms.TextBox txtcLast;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtcTaxNumber;
        private System.Windows.Forms.Label label14;
        private WindowControls.UserToolBar userToolBar1;
    }
}
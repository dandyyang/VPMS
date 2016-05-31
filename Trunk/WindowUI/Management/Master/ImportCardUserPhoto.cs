using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BLL.Factory.Management;
using BLL.IBLL.Management.Master;
using Model.Management.Master;
using Common;
using Common.FileMgtService;
using System.Threading;
using System.Diagnostics;

namespace WindowUI.Management.Master
{
    public partial class ImportCardUserPhoto : BaseForm
    {
        ICardUserMasterBL _cardUserMasterBL;
        private List<string> _photoList;

        private List<string> successedPhoto;
        private List<CardUserMaster_cus_Info> _userInfoList;

        private Thread invokeOpenDialogThread;
        private DialogResult result;

        public ImportCardUserPhoto()
        {
            InitializeComponent();

            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            _photoList = new List<string>();
            _userInfoList = new List<CardUserMaster_cus_Info>();
            successedPhoto = new List<string>();
        }




        private void btnSelect_Click(object sender, EventArgs e)
        {
            //add by justinleung 2011/09/06
            invokeOpenDialogThread = new Thread(new ThreadStart(InvokeMethod));
            invokeOpenDialogThread.SetApartmentState(ApartmentState.STA);
            invokeOpenDialogThread.Start();
            invokeOpenDialogThread.Join();
            if (result == DialogResult.OK && openFileDialog1.FileName != "")
            {
                ShowSelectedPhoto(openFileDialog1.FileNames);
            }
        }
        private void InvokeMethod()
        {
            result = openFileDialog1.ShowDialog();
        }


        private void ShowSelectedPhoto(string[] fileNames)
        {
            listView1.Items.Clear();
            ImageList list = new ImageList();
            list.ImageSize = new Size(120, 150);
            list.ColorDepth = ColorDepth.Depth32Bit;
            int i = 0;
            foreach (string path in fileNames)
            {
                FileInfo fi = new FileInfo(path);
                if (fi.Length == 0)
                {
                    MessageBox.Show("此图片有问题,不能处理：" + path);
                    continue;
                }

                list.Images.Add(Image.FromFile(path));
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;

                CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
                info.cus_cNumber = Path.GetFileNameWithoutExtension(path);
                var cardUserMaster_cus_Infos = _cardUserMasterBL.SearchRecords(info);

                bool exist = false;
                foreach (CardUserMaster_cus_Info cinfo in cardUserMaster_cus_Infos)
                {
                    if (cinfo.cus_cNumber == info.cus_cNumber)
                    {
                        item.Text = cinfo.cus_cChaName + "(" + info.cus_cNumber + ")";
                        item.ToolTipText = cinfo.cus_cNumber;
                        _photoList.Add(path);
                        _userInfoList.Add(cinfo);
                        exist = true;
                    }
                }
                if (!exist)
                {
                    item.Text = "此学生编号没有登记";
                    item.ForeColor = Color.Blue;
                }
                if (fi.Length > DefineConstantValue.CardUserPicture_MaxSize * 1024)
                {
                    item.Text += "(超过大小限制)";
                    item.ForeColor = Color.Red;
                }
                listView1.Items.Add(item);
                i++;
            }
            listView1.LargeImageList = list;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                toolStripProgressBar1.Visible = true;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileMgtSoapClient soap = WebSrvFactory.GetFileMgt();
            int i = 1;
            foreach (string path in _photoList)
            {

                string fileName = Path.GetFileNameWithoutExtension(path);
                try
                {
                    byte[] bytes = File.ReadAllBytes(path);
                    CardUserMaster_cus_Info info = _userInfoList.SingleOrDefault(u => u.cus_cNumber == fileName);
                    info = _cardUserMasterBL.DisplayRecord(info) as CardUserMaster_cus_Info;
                    ReturnValueInfo rInfo;
                    if (info.cus_guidPhotoKey == Guid.Empty)
                    {
                        rInfo = soap.SaveBytes(DefineConstantValue.SchoolInternetOfThings, DefineConstantValue.CardUserPicture, path, bytes);
                        if (rInfo.boolValue)
                        {
                            info.cus_guidPhotoKey = new Guid(rInfo.ValueObject.ToString());
                            info.cus_imgPhoto = new Binary(bytes);
                            _cardUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                        }
                    }
                    else
                    {
                        rInfo = soap.UpdateBytes(info.cus_guidPhotoKey, DefineConstantValue.SchoolInternetOfThings, DefineConstantValue.CardUserPicture, path, bytes);
                    }

                    if (rInfo.boolValue)
                    {
                        successedPhoto.Add(info.cus_cNumber);
                    }

                    backgroundWorker1.ReportProgress((i * 100 / _photoList.Count), info.cus_cNumber);


                }
                catch
                {

                }

                i++;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                var tmp = successedPhoto.SingleOrDefault(i => i == item.ToolTipText);
                if (tmp != null)
                {
                    item.Remove();
                }
            }
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Text = "导入完成";
        }


    }
}

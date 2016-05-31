using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderEquipment.Entity.TH24G;
using ReaderEquipment.Entity;
using Common;
//using model = Model.General;

namespace ReaderEquipment.ReaderImplement
{
    public partial class TH24GR : AbstractReader
    {
        private AxMSCommLib.AxMSComm _mscom;
        private object _lockObject;
        short _portIndex;

        public TH24GR()
        {
            InitializeComponent();

            this._portIndex = -1;
            this._lockObject = new object();
        }

        public override ReturnValueInfo Connect()
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();

            InitComPort();

            return returnInfo;
        }

        public override ReaderEquipment.Entity.ReturnValueInfo StartRead()
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();

            //if (!_mscom.PortOpen)
            //{
            //    if (this._portIndex < 0)
            //    {
            //        model.CommInfo[] commInfos = null;

            //        commInfos = General.GetCommInfos();

            //        if (commInfos != null && commInfos.Length > 0)
            //        {
            //            for (int i = 0; i < commInfos.Length; i++)
            //            {
            //                this._mscom.CommPort = commInfos[i].CommPort;
            //                try
            //                {
            //                    _mscom.PortOpen = true;  //打开串口
            //                    this._portIndex = this._mscom.CommPort;
            //                    break;
            //                }
            //                catch (Exception Ex)
            //                {
            //                }
            //            }
            //        }

            //        if (this._portIndex < 0)
            //        {
            //            ReaderExceptionEventArgs ex = new ReaderExceptionEventArgs();
            //            ex.ExceptionObject = new Exception("沒有可用的Comm口!");
            //            OnReaderException(ex);
            //        }
            //    }
            //    else
            //    {
            //        this._mscom.PortOpen = true;  //打开串口
            //    }
            //}

            _mscom.CommPort = base.CommPort;//设置端口号为COM1

            try
            {
                this._mscom.PortOpen = true;
            }
            catch (Exception Ex)
            {
                //throw Ex;
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = Ex.Message;
                return returnInfo;
            }

            returnInfo.IsSuccess = true;

            return returnInfo;
        }

        public override void StopRead()
        {
            if (_mscom != null && _mscom.PortOpen)
            {
                _mscom.PortOpen = false;

            }
        }

        public override List<TagInformationInfo> GetTagList(string antennaNum)
        {
            return null;
        }


        #region IReaderListener Members


        /// <summary>
        /// 初始化Com 口信息


        /// </summary>
        private void InitComPort()
        {
            this._mscom = this.axMSComm1;
            if (_mscom.PortOpen)

                _mscom.PortOpen = false;//初始化



            _mscom.InputLen = 0;  //清除接收缓冲区



            //_mscom.CommPort = base.CommPort;//设置端口号为COM1

            _mscom.RThreshold = 1;   //每接收一个字符则激发OnComm()事件

            _mscom.Settings = "38400,N,8,1";  //端口设置

            _mscom.DTREnable = true;  //置DTR有效

            _mscom.RTSEnable = true;  //置RTS有效

            _mscom.Handshaking = MSCommLib.HandshakeConstants.comNone;

            //m_mscom.InputMode = MSCommLib.InputModeConstants.comInputModeText;  //文本

            _mscom.InputMode = MSCommLib.InputModeConstants.comInputModeBinary;   //二进制



            _mscom.NullDiscard = false;

            _mscom.OnComm += new EventHandler(ReadTagForCom);  //执行一个OnComm事件

            _mscom.InBufferCount = 0;


        }


        /// <summary>
        /// 從Com 口讀取對應的Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadTagForCom(object sender, EventArgs e)
        {

            if (_mscom.CommEvent == (short)(MSCommLib.OnCommConstants.comEvReceive))  //查询CommEvent属性 
            {
                lock (this._lockObject)
                {
                    System.Threading.Thread.Sleep(5);

                    object inputobjs = _mscom.Input;
                    byte[] inputBytes = (byte[])inputobjs;
                    string strInput = string.Empty;

                    for (int i = 0; i <= (inputBytes.Length - 1); i++)
                    {
                        strInput = strInput + inputBytes[i].ToString("X2");
                    }
                    HandleTab(strInput);
                }
            }

        }

        /// <summary>
        /// Tab 格式的處理
        /// </summary>
        /// <param name="tabNum"></param>
        private void HandleTab(string tabNum)
        {
            string _tabNum;

            _tabNum = tabNum.Trim();


            if (_tabNum.Length >= 24)
            {
                _tabNum = _tabNum.Substring(0, 24);
            }
            else
            {
                return;
            }

            TagInformationInfo info = GetTagInformation(_tabNum);

            TagInfoResultEventArgs infoEventArgs = new TagInfoResultEventArgs();

            infoEventArgs.TagInformation = info;
            base.OnTagResult(infoEventArgs);

        }

        /// <summary>
        /// 獲得標簽的實體信息
        /// </summary>
        /// <param name="tabString">標簽字符串</param>
        /// <returns></returns>
        private TagInformationInfo GetTagInformation(string tabString)
        {
            TagInformationInfo info = new TagInformationInfo();

            tabString = tabString.Trim().Substring(0, 24);
            info.BegFlg = tabString.Substring(0, 2);
            info.Address = tabString.Substring(2, 2);
            info.States = tabString.Substring(4, 2);
            info.DataLength = tabString.Substring(6, 2);
            info.Keyboard = tabString.Substring(8, 2);
            info.Power = tabString.Substring(10, 2);
            info.IDNum = tabString.Substring(12, 6);

            // 按键，电量和ID号组成与A型一致的标签序列串。
            //info.TagID = info.Keyboard + info.Power + info.IDNum;

            info.CRC = tabString.Substring(18, 4);
            info.EndFlg = tabString.Substring(22, 2);
            if (info.BegFlg == "C2" && info.EndFlg == "C3")
            {
                info.IsActiveTab = true;
                info.TabNum = tabString;
            }
            else
            {
                info.IsActiveTab = false;
            }

            ///選項按鈕
            switch (int.Parse(info.Keyboard))
            {
                case 8: info.OptionalValue = "A";
                    break;
                case 4: info.OptionalValue = "B";
                    break;
                case 2: info.OptionalValue = "C";
                    break;
                case 1: info.OptionalValue = "D";
                    break;
                case 9: info.OptionalValue = "A,D";
                    break;
                default: info.OptionalValue = "";
                    break;
            }


            info.ReadTimes = 1;

            return info;
        }


        #endregion

        public override List<TagInformationInfo> GetTagList(string antennaNum, bool DeleteHistory)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}

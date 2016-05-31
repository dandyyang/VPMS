using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderEquipment.Entity;
using Common;

namespace ReaderEquipment.ReaderImplement
{
    public partial class THBlueToothReader : AbstractReader
    {
        private AxMSCommLib.AxMSComm _mscom;
        private object _lockObject;
        short _portIndex;

        public THBlueToothReader()
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

            _mscom.CommPort = base.CommPort;//设置端口号为COM1

            try
            {
                this._mscom.PortOpen = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
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


            //if (_tabNum.Length >= 24)
            //{
            //    _tabNum = _tabNum.Substring(0, 24);
            //}
            //else
            //{
            //    return;
            //}

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

            info.TabNum = tabString;

            return info;
        }


        #endregion

        public override List<TagInformationInfo> GetTagList(string antennaNum, bool DeleteHistory)
        {
            throw new NotImplementedException();
        }
    }
}

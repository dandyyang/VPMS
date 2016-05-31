using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WComm_UDP;
using System.Windows.Forms;
using Common;
using ReaderEquipment.Entity;
using ReaderEquipment.Function;

namespace ReaderEquipment.ReaderImplement
{
    public class WeigengICCardController : AbstractReader
    {

        #region 內部參數

        WComm_Operate _wudp;
        int _iPort = 60000;
        string _ipAddr = ""; //IP 地址为空, 表示广播包方式




        string _hexMac = "";
        string _hexIP = "";
        string _hexsubNet = "";
        string _hexGateway = "";
        string _hexPort = "";
        string _cmdOpenDoor = "9D10";

        #endregion

        //protected System.Timers.Timer _Readtimer;
        //int _TimerInterval = 1000; //1秒读一次


        List<TagInformationInfo> _TabList;
        int _RecordIndex = 0;

        public WeigengICCardController()
        {
            if (this._wudp == null)
            {
                _wudp = new WComm_Operate();
            }
            _TabList = new List<TagInformationInfo>();
            //_Readtimer = new System.Timers.Timer();
        }

        ~WeigengICCardController()
        {
            _wudp = null;
        }

        #region 獲得控制器運行時信息

        /// <summary>
        /// 獲得控制器運行時信息
        /// </summary>
        /// <param name="controllerSN">產品序列號</param>
        /// <param name="BeginIndex">開始記錄數</param>
        /// <param name="ipAddr">IP 地址为空, 表示广播包方式</param>
        /// <returns></returns>
        private string GetRunInfoIntime(long controllerSN, long BeginIndex, string ipAddr)
        {
            try
            {
                // '读取运行状态信息




                //'生成指令帧 wudp.NumToStrHex(0,3) 表示第0个记录, 也就最新记录

                string strCmd = _wudp.CreateBstrCommand(controllerSN, "8110" + _wudp.NumToStrHex(BeginIndex, 3));
                string strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);

                //嘗試用廣播形式打開




                if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }


                if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                {
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                    }
                    //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                    ShowErrorEx(ErrorDes);

                }


                return strFrame;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region 獲得最新刷卡記錄的卡ID,時間,狀態


        /// <summary>
        /// 獲得最新刷卡記錄的卡ID,時間,狀態

        /// </summary>
        /// <param name="controllerSN">產品SN</param>
        /// <param name="ipAddr">IP 地址为空, 表示广播包方式</param>
        /// <returns></returns>
        private iCCardInfo GetLastSwipeInfo(long controllerSN, string ipAddr)
        {
            iCCardInfo cardinfo = new iCCardInfo();

            string RunInfo = GetRunInfoIntime(controllerSN, 0, ipAddr);
            if (RunInfo.Trim().Length == 0)
                return null;
            string SwipeDate = "";
            long cardID = 0;
            long status = 0;
            //最近一條刷卡記錄


            try
            {
                SwipeDate = _wudp.GetSwipeDateFromRunInfo(RunInfo, ref  cardID, ref   status);
                //無刷卡記錄

                if (SwipeDate.Trim().Length <= 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            cardinfo.controllerSN = controllerSN;
            cardinfo.cardID = cardID;
            cardinfo.swipedate = SwipeDate;
            cardinfo.status = status;
            //cardinfo.doorid = long.Parse(this._wudp.NumToStrHex(status, 1).ToString().Substring(1, 1)) + 1;
            return cardinfo;
        }

        #endregion

        #region 校准控制器时间


        /// <summary>
        /// 校准控制器时间
        /// </summary>
        /// <param name="controllerSN">產品序列號</param>
        /// <param name="ipAddr">IP 地址为空, 表示广播包方式</param>
        /// <returns></returns>
        public override bool AdjustClockByPCTime(string controllerSN, string ipAddr)
        {
            long sn = 0;

            try
            {
                sn = Convert.ToInt64(controllerSN);
            }
            catch (Exception Ex)
            {
                return false;
            }

            bool rtv = false;
            try
            {
                //'生成指令帧
                string strCmd = this._wudp.CreateBstrCommandOfAdjustClockByPCTime(sn);

                //发送指令, 并获取返回信息
                string strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);

                //嘗試用廣播形式打開
                if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                {
                    rtv = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                    }
                    //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                    ShowErrorEx(ErrorDes);
                }
                else
                {
                    rtv = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rtv;

        }

        #endregion

        #region  獲得錯誤描述

        /// <summary>
        /// 獲得錯誤描述
        /// </summary>
        /// <param name="ErrorCode"></param>
        /// <returns></returns>
        private string GetErrorDesc(long ErrorCode)
        {
            string ErrorDesc = "";
            switch (ErrorCode)
            {
                case (-13):
                    ErrorDesc = "通信不上";
                    break;
                case (-14):
                    ErrorDesc = "接受到的數據楨無效";
                    break;
                case (-53):
                    ErrorDesc = "通信或操作失敗,有異常";
                    break;
            }



            return ErrorDesc;

        }

        #endregion

        #region 獲得所有的歷史刷卡記錄

        /// <summary>
        /// 獲得所有的歷史刷卡記錄
        /// </summary>
        /// <param name="controllerSN">產品序列號</param>
        /// <param name="ipAddr">IP 地址为空, 表示广播包方式</param>
        /// <returns></returns>
        private List<TagInformationInfo> GetAllHistoryRecord(long controllerSN, string ipAddr)
        {
            List<TagInformationInfo> itemlist = new List<TagInformationInfo>();

            // '提取记录
            long recIndex = 1;     //'记录索引号

            long cardId = 0;
            long status = 0;
            try
            {
                while (true)
                {
                    // '生成指令帧

                    string strCmd = _wudp.CreateBstrCommand(controllerSN, "8D10" + _wudp.NumToStrHex(recIndex, 4));

                    //  '发送指令, 并获取返回信息

                    string strFrame = _wudp.udp_comm(strCmd, ipAddr, this._iPort);

                    //嘗試用廣播形式打開

                    if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                    {
                        strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                    }

                    if ((_wudp.ErrCode != 0) || (strFrame.Trim() == ""))
                    {
                        string ErrorDes = this._wudp.ErrMessage;
                        if (ErrorDes.Trim() == "")
                        {
                            ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                        }
                        //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);            
                        ShowErrorEx(ErrorDes);
                        return null;
                        break;
                    }
                    else
                    {
                        string swipeDate = _wudp.GetSwipeDateFromRunInfo(strFrame, ref  cardId, ref status);

                        if (swipeDate.Trim() != "")
                        {
                            TagInformationInfo item = new TagInformationInfo();
                            item.TagID = cardId.ToString();              //卡ID
                            item.IDNum = controllerSN.ToString();//SN编号
                            item.Antenna = int.Parse(this._wudp.NumToStrHex(status, 1).ToString().Substring(1, 1)) + 1; //门号
                            item.ReaderKey = ipAddr; //IP地址

                            string hexstatus = this._wudp.NumToStrHex(status, 1).ToString().Substring(0, 1);
                            string strTemp = string.Empty;
                            switch (hexstatus)
                            {
                                case "0":
                                    { strTemp = "讀卡器刷卡開門"; break; }
                                case "8":
                                    { strTemp = "讀卡器刷卡禁止通過: 原因不明"; break; }
                                case "9":
                                    { strTemp = "讀卡器刷卡禁止通過: 沒有許可權"; break; }
                                case "A":
                                    { strTemp = "讀卡器刷卡禁止通過: 密碼不對"; break; }
                                case "B":
                                    { strTemp = "讀卡器刷卡禁止通過: 系統有故障"; break; }
                                default:
                                    { strTemp = ""; break; }
                            }
                            item.States = strTemp.ToString();

                            item.ReadDatetime = DateTime.Parse(swipeDate);

                            itemlist.Add(item);

                            recIndex = recIndex + 1;
                        }
                        else
                        {
                            recIndex = recIndex - 1;
                            break;
                        }

                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {

                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
                //throw ex;
            }

            return itemlist;
        }

        #endregion

        #region 刪除歷史記錄

        /// <summary>
        /// 刪除歷史記錄
        /// </summary>
        /// <param name="controllerSN">產品序列號</param>
        /// <param name="ipAddr">IP 地址为空, 表示广播包方式</param>
        /// <returns></returns>
        private bool DeleteHistoryRecord(long controllerSN, string ipAddr)
        {
            bool Rtv = false;
            //先提取記錄


            long recIndex = 0;
            if (this._TabList == null || this._TabList.Count == 0)
            {
                _TabList = GetAllHistoryRecord(controllerSN, ipAddr);
            }


            //獲得總記錄數
            recIndex = this._TabList.Count;

            //删除已提取的记录
            if (recIndex > 0)   //只有提取了记录才进行删除
            {
                //if (MessageBox.Show(DefineConstantValue.SystemMessageText.strMessageText_Q_Delete, DefineConstantValue.SystemMessageText.strQuestion, MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                string strCmd = _wudp.CreateBstrCommand(controllerSN, "8E10" + _wudp.NumToStrHex(recIndex, 4)); //生成指令帧


                string strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);//发送指令, 并获取返回信息


                //嘗試用廣播形式打開

                if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }

                if ((_wudp.ErrCode != 0) || (strFrame.Trim() == ""))
                {
                    Rtv = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                }
                else
                {
                    Rtv = true;
                    //MessageBox.Show(DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete);
                }
                //}
            }

            return Rtv;
        }

        #endregion

        #region 查询控制器的IP设置

        /// <summary>
        /// 查询控制器的IP设置,读取网络配置信息指令
        /// </summary>
        /// <param name="controllerSN">產品序號</param>
        /// <returns></returns>
        private DeviceNetWorkInfo GetDeviceNetWorkInfo(long controllerSN)
        {

            DeviceNetWorkInfo item = null;

            try
            {
                string strCmd = _wudp.CreateBstrCommand(controllerSN, "0111"); //生成指令帧 读取网络配置信息指令
                string strFrame = _wudp.udp_comm(strCmd, _ipAddr, this._iPort);

                if ((_wudp.ErrCode != 0) || (strFrame.Trim() == ""))
                {
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                    }
                    //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                    ShowErrorEx(ErrorDes);
                }

                else
                {

                    item = new DeviceNetWorkInfo();

                    int startLoc;

                    //'MAC
                    startLoc = 10;
                    //item.MacAddr = _wudp.StrHexToNum(strFrame.Substring(startLoc, 2)).ToString();
                    //item.MacAddr = item.MacAddr + "-" + _wudp.StrHexToNum(strFrame.Substring(startLoc + 2, 2)).ToString();
                    //item.MacAddr = item.MacAddr + "-" + _wudp.StrHexToNum(strFrame.Substring(startLoc + 4, 2)).ToString();
                    //item.MacAddr = item.MacAddr + "-" + _wudp.StrHexToNum(strFrame.Substring(startLoc + 6, 2)).ToString();
                    //item.MacAddr = item.MacAddr + "-" + _wudp.StrHexToNum(strFrame.Substring(startLoc + 8, 2)).ToString();
                    //item.MacAddr = item.MacAddr + "-" + _wudp.StrHexToNum(strFrame.Substring(startLoc + 10, 2)).ToString();

                    item.MacAddr = strFrame.Substring(startLoc, 2);
                    item.MacAddr = item.MacAddr + "-" + strFrame.Substring(startLoc + 2, 2);
                    item.MacAddr = item.MacAddr + "-" + strFrame.Substring(startLoc + 4, 2);
                    item.MacAddr = item.MacAddr + "-" + strFrame.Substring(startLoc + 6, 2);
                    item.MacAddr = item.MacAddr + "-" + strFrame.Substring(startLoc + 8, 2);
                    item.MacAddr = item.MacAddr + "-" + strFrame.Substring(startLoc + 10, 2);
                    _hexMac = strFrame.Substring(startLoc, 12);

                    //'IP
                    startLoc = 22;
                    item.IPAddr = _wudp.StrHexToNum(strFrame.Substring(startLoc, 2)).ToString();
                    item.IPAddr = item.IPAddr + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 2, 2)).ToString();
                    item.IPAddr = item.IPAddr + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 4, 2)).ToString();
                    item.IPAddr = item.IPAddr + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 6, 2)).ToString();
                    _hexIP = strFrame.Substring(startLoc, 8);

                    // 'Subnet Mask
                    startLoc = 30;
                    item.subNet = _wudp.StrHexToNum(strFrame.Substring(startLoc, 2)).ToString();
                    item.subNet = item.subNet + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 2, 2)).ToString();
                    item.subNet = item.subNet + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 4, 2)).ToString();
                    item.subNet = item.subNet + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 6, 2)).ToString();
                    _hexsubNet = strFrame.Substring(startLoc, 8);

                    //'Default Gateway
                    startLoc = 38;
                    item.gateway = _wudp.StrHexToNum(strFrame.Substring(startLoc, 2)).ToString();
                    item.gateway = item.gateway + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 2, 2)).ToString();
                    item.gateway = item.gateway + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 4, 2)).ToString();
                    item.gateway = item.gateway + "." + _wudp.StrHexToNum(strFrame.Substring(startLoc + 6, 2)).ToString();
                    _hexGateway = strFrame.Substring(startLoc, 8);

                    //'UDP Port
                    startLoc = 46;
                    item.UdpPort = _wudp.StrHexToNum(strFrame.Substring(startLoc, 4)).ToString();
                    _hexPort = strFrame.Substring(startLoc, 4);

                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }

        #endregion

        #region 設置新IP

        /// <summary>
        ///  設置新IP
        /// </summary>
        /// <param name="controllerSN">產品序號</param>
        ///  <param name="ipAddr">原IP 地址为空, 表示广播包方式</param>
        /// <param name="newipAddr">新IP</param>
        /// <returns></returns>
        private bool ReSetControllerIP(long controllerSN, string ipAddr, string newipAddr)
        {

            bool rtv = false;

            DeviceNetWorkInfo iteminfo = GetDeviceNetWorkInfo(controllerSN);

            string[] temp;
            temp = newipAddr.Split('.');
            string HexNewIP = "";
            foreach (string item in temp)
            {
                HexNewIP = HexNewIP + _wudp.NumToStrHex(long.Parse(item), 1);
                //HexNewIP = HexNewIP + item;
            }


            string strCmd = _wudp.CreateBstrCommand(controllerSN, "F211" + _hexMac + HexNewIP + _hexsubNet + _hexGateway + "60EA"); //  '生成指令帧 读取网络配置信息指令

            string strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);// '发送指令, 并获取返回信息





            //嘗試用廣播形式打開




            if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
            {
                strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
            }


            if ((_wudp.ErrCode != 0) || (strFrame == ""))
            {
                string ErrorDes = this._wudp.ErrMessage;
                if (ErrorDes.Trim() == "")
                {
                    ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                }
                rtv = false;
                //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                ShowErrorEx(ErrorDes);
            }
            else
            {
                rtv = true;
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);  //引入3秒延时




                MessageBox.Show("設置成功");
            }

            return rtv;

        }


        #endregion

        #region 搜尋網絡中的控制器




        /// <summary>
        /// 搜尋網絡中的控制器



        /// </summary>
        /// <returns></returns>
        private long GetcontrollerSN()
        {

            try
            {
                long Rtv = 0;
                // '读取运行状态信息




                string strCmd = _wudp.CreateBstrCommand(-1, "0111");
                string strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);


                if (this._wudp.ErrCode != 0 || strFrame.Trim().Length <= 0)
                {
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                    }
                    //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                    ShowErrorEx(ErrorDes);

                }
                else
                {
                    Rtv = _wudp.GetSNFromRunInfo(strFrame);
                }



                return Rtv;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        #endregion

        #region 远程开门


        /// <summary>
        /// 远程开门

        /// </summary>
        /// <param name="controllerSN">产品序号</param>
        /// <param name="strFuncData">指令码</param>
        /// <returns></returns>
        public bool RemoteDoorOpening(long controllerSN, string doorNumber)
        {
            try
            {
                bool res = false;
                string strCmd = _wudp.CreateBstrCommand(controllerSN, _cmdOpenDoor + doorNumber);
                string strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);

                if ((_wudp.ErrCode != 0) || (strFrame.Trim() == ""))
                {
                    res = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);
                    }
                    //MessageBox.Show(ErrorDes, DefineConstantValue.SystemMessageText.strSystemError);
                    ShowErrorEx(ErrorDes);
                }
                else
                {
                    res = true;
                }

                #region 测试给予某卡片进入权限


                //1743301
                string cardID = _wudp.NumToStrHex(1743301, 2);
                string beginDate = _wudp.MSDateYmdToWCDateYmd(DateTime.Now.ToLongDateString());
                string endDate = _wudp.MSDateYmdToWCDateYmd(DateTime.Now.AddDays(1).ToLongDateString());
                string strRight = _wudp.CreateBstrCommand(controllerSN, "07110000" + cardID + "0303" + beginDate + endDate);
                string strGetRight = _wudp.udp_comm(strRight, _ipAddr, _iPort);

                #endregion

                return res;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        #endregion

        public override ReturnValueInfo StartRead()
        {
            ReturnValueInfo Rtvitem = new ReturnValueInfo();
            Rtvitem.IsSuccess = true;
            //try
            //{
            //    //将所需要的控制器SN传入
            //    _TabList = GetAllHistoryRecord(long.Parse( base.ControllerSN), this._ipAddr);
            //    //DeleteHistoryRecord(base.ControllerSN, base.ReaderIP);
            //    if (this._TabList == null)
            //    {
            //        _Readtimer.Start();
            //        Rtvitem.IsSuccess = false;
            //    }
            //    else
            //    {
            //        Rtvitem.IsSuccess = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Rtvitem.IsSuccess = false;
            //    base.OnReaderException(GetEXObject(ex));
            //    //throw ex ;
            //}
            return Rtvitem;

        }

        public override void StopRead()
        {
            //_Readtimer.Close();
        }

        public override bool DataAffirm(string snNumber)
        {
            if (snNumber == null || snNumber.Trim() == "")
            {
                return false;
            }

            try
            {
                return DeleteHistoryRecord(long.Parse(snNumber), this._ipAddr);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }


        void ShowErrorEx(string message)
        {
            ReaderExceptionEventArgs error = new ReaderExceptionEventArgs();
            Exception ex = new Exception();
            ex.Source = message.Trim();
            error.ExceptionObject = ex;
            base.OnReaderException(error);
        }


        public override ReturnValueInfo Connect()
        {

            ReturnValueInfo Rtvitem = new ReturnValueInfo();

            //_Readtimer.Elapsed += new System.Timers.ElapsedEventHandler(Readtimer_Elapsed);
            //_Readtimer.Interval = this._TimerInterval;
            //_RecordIndex = 0;
            //_Readtimer.Stop();

            DeviceNetWorkInfo deviceInfo = GetDeviceNetWorkInfo(long.Parse(base.ControllerSN));

            if (deviceInfo == null)
            {
                Rtvitem.IsSuccess = false;
                return Rtvitem;
            }

            this._ipAddr = deviceInfo.IPAddr;
            if (deviceInfo.UdpPort.Trim() != "")
            {
                this._iPort = int.Parse(deviceInfo.UdpPort);
            }


            Rtvitem.IsSuccess = true;
            return Rtvitem;



        }

        ReaderExceptionEventArgs GetEXObject(Exception ex)
        {
            ReaderExceptionEventArgs exitem = new ReaderExceptionEventArgs();

            exitem.ExceptionObject = ex;

            return exitem;

        }

        void Readtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                _RecordIndex++;
                TagInformationInfo item = null;

                if (_TabList == null)
                {
                    _TabList = GetAllHistoryRecord(long.Parse(base.ControllerSN), this._ipAddr);
                    DeleteHistoryRecord(long.Parse(base.ControllerSN), this._ipAddr);
                }


                if (_TabList.Count > _RecordIndex)
                {
                    item = this._TabList[_RecordIndex];
                }

                if (item != null)
                {
                    TagInfoResultEventArgs infoEventArgs = new TagInfoResultEventArgs();
                    infoEventArgs.TagInformation = item;
                    infoEventArgs.InformationObject = _TabList;
                    base.OnTagResult(infoEventArgs);
                }

                item = null;
            }
            catch (Exception ex)
            {
                base.OnReaderException(GetEXObject(ex));
                //throw;
            }


        }

        /// <summary>
        /// 获取读取考勤信息
        /// </summary>
        /// <param name="SNNum">控制器S/N序号</param>
        /// <returns></returns>
        public override List<TagInformationInfo> GetTagList(string SNNum)
        {
            try
            {
                List<TagInformationInfo> RtvItem = null;

                if (RtvItem == null || RtvItem.Count == 0)
                {
                    RtvItem = GetAllHistoryRecord(long.Parse(SNNum), this._ipAddr);

                    if (RtvItem != null)
                    {
                        this._TabList = RtvItem;
                    }
                }

                return RtvItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 获取读取考勤信息
        /// </summary>
        /// <param name="SNNum">控制器S/N序号</param>
        /// <returns></returns>
        public override List<TagInformationInfo> GetTagList(string SNNum, bool DeleteHistory)
        {
            //throw new NotImplementedException();
            try
            {
                List<TagInformationInfo> RtvItem = this._TabList;

                if (RtvItem == null)
                {
                    RtvItem = GetAllHistoryRecord(long.Parse(SNNum), this._ipAddr);
                }

                //获取数据后删除相关记录


                if (DeleteHistory)
                {
                    this.DeleteHistoryRecord(long.Parse(SNNum), this._ipAddr);
                }

                return RtvItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region 权限设置

        struct _Time
        {
            public string starTime1;
            public string endTime1;
            public string starTime2;
            public string endTime2;
            public string starTime3;
            public string endTime3;
            public string starDate;
            public string endDate;
        }

        private string CheckIP(string controllerSN)
        {
            string ipAddr = string.Empty;
            try
            {
                ipAddr = GetDeviceNetWorkInfo(Convert.ToInt32(controllerSN)).IPAddr;
            }
            catch
            {

                ipAddr = "";
                //ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                //exception.ExceptionObject = ex;
                //base.OnReaderException(exception);
            }
            return ipAddr;
        }

        private List<_Time> set(Functions._Time functionsTime, DateTime starDate, DateTime endDate)
        {
            List<_Time> timeList = new List<_Time>();
            _Time time;
            try
            {
                int count = functionsTime.time.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    time = new _Time();
                    time.starTime1 = functionsTime.time[i * 6] != null ? functionsTime.time[i * 6].Value.ToString("HH:mm:00") : null;
                    time.endTime1 = functionsTime.time[i * 6 + 1] != null ? functionsTime.time[i * 6 + 1].Value.ToString("HH:mm:00") : null;
                    time.starTime2 = functionsTime.time[i * 6 + 2] != null ? functionsTime.time[i * 6 + 2].Value.ToString("HH:mm:00") : null;
                    time.endTime2 = functionsTime.time[i * 6 + 3] != null ? functionsTime.time[i * 6 + 3].Value.ToString("HH:mm:00") : null;
                    time.starTime3 = functionsTime.time[i * 6 + 4] != null ? functionsTime.time[i * 6 + 4].Value.ToString("HH:mm:00") : null;
                    time.endTime3 = functionsTime.time[i * 6 + 5] != null ? functionsTime.time[i * 6 + 5].Value.ToString("HH:mm:00") : null;
                    time.starDate = starDate.ToString("yyyy-MM-dd");
                    time.endDate = endDate.ToString("yyyy-MM-dd");

                    timeList.Add(time);
                }
                int index = functionsTime.time.Count % 6;
                time = new _Time();
                time.starTime1 = null;
                time.endTime1 = null;
                time.starTime2 = null;
                time.endTime2 = null;
                time.starTime3 = null;
                time.endTime3 = null;
                time.starDate = starDate.ToString("yyyy-MM-dd");
                time.endDate = endDate.ToString("yyyy-MM-dd");

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        time.starTime1 = functionsTime.time[count * 6] != null ? functionsTime.time[count * 6].Value.ToString("HH:mm:00") : null;
                        timeList.Add(time);
                        break;
                    case 2:
                        time.starTime1 = functionsTime.time[count * 6] != null ? functionsTime.time[count * 6].Value.ToString("HH:mm:00") : null;
                        time.endTime1 = functionsTime.time[count * 6 + 1] != null ? functionsTime.time[count * 6 + 1].Value.ToString("HH:mm:00") : null;
                        timeList.Add(time);
                        break;
                    case 3:
                        time.starTime1 = functionsTime.time[count * 6] != null ? functionsTime.time[count * 6].Value.ToString("HH:mm:00") : null;
                        time.endTime1 = functionsTime.time[count * 6 + 1] != null ? functionsTime.time[count * 6 + 1].Value.ToString("HH:mm:00") : null;
                        time.starTime2 = functionsTime.time[count * 6 + 2] != null ? functionsTime.time[count * 6 + 2].Value.ToString("HH:mm:00") : null;
                        timeList.Add(time);
                        break;
                    case 4:
                        time.starTime1 = functionsTime.time[count * 6] != null ? functionsTime.time[count * 6].Value.ToString("HH:mm:00") : null;
                        time.endTime1 = functionsTime.time[count * 6 + 1] != null ? functionsTime.time[count * 6 + 1].Value.ToString("HH:mm:00") : null;
                        time.starTime2 = functionsTime.time[count * 6 + 2] != null ? functionsTime.time[count * 6 + 2].Value.ToString("HH:mm:00") : null;
                        time.endTime2 = functionsTime.time[count * 6 + 3] != null ? functionsTime.time[count * 6 + 3].Value.ToString("HH:mm:00") : null;
                        timeList.Add(time);
                        break;
                    case 5:
                        time.starTime1 = functionsTime.time[count * 6] != null ? functionsTime.time[count * 6].Value.ToString("HH:mm:00") : null;
                        time.endTime1 = functionsTime.time[count * 6 + 1] != null ? functionsTime.time[count * 6 + 1].Value.ToString("HH:mm:00") : null;
                        time.starTime2 = functionsTime.time[count * 6 + 2] != null ? functionsTime.time[count * 6 + 2].Value.ToString("HH:mm:00") : null;
                        time.endTime2 = functionsTime.time[count * 6 + 3] != null ? functionsTime.time[count * 6 + 3].Value.ToString("HH:mm:00") : null;
                        time.starTime3 = functionsTime.time[count * 6 + 4] != null ? functionsTime.time[count * 6 + 4].Value.ToString("HH:mm:00") : null;
                        timeList.Add(time);
                        break;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return timeList;
        }

        private void checkIndex(string controllerSN, _Time _time, out bool _type, out long timeIndex)
        {
            string setTime = string.Empty;
            setTime += _wudp.MSDateHmsToWCDateHms(_time.starTime1);
            setTime += _wudp.MSDateHmsToWCDateHms(_time.endTime1);
            setTime += _wudp.MSDateHmsToWCDateHms(_time.starTime2);
            setTime += _wudp.MSDateHmsToWCDateHms(_time.endTime2);
            setTime += _wudp.MSDateHmsToWCDateHms(_time.starTime3);
            setTime += _wudp.MSDateHmsToWCDateHms(_time.endTime3);
            setTime += _wudp.MSDateYmdToWCDateYmd(_time.starDate);
            setTime += _wudp.MSDateYmdToWCDateYmd(_time.endDate);

            string strFrame = string.Empty;
            _type = false;
            timeIndex = 0;
            string _temp = string.Empty;
            long t = 0;
            try
            {
                for (int i = 3; i < 255; i++)
                {
                    //if (_code == 0)
                    //{
                    string _privilege = "9610" + _wudp.NumToStrHex(i, 1);
                    string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), _privilege);
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                    _temp = strFrame.Substring(21, 32);
                    t = _wudp.StrHexToNum(_temp);
                    //if (t == 0)
                    //    break;
                    if (setTime == _temp)
                    {
                        timeIndex = _wudp.StrHexToNum(strFrame.Substring(10, 4));
                        _type = true;
                        break;
                    }
                    else if (t == 0)
                    {
                        timeIndex = _wudp.StrHexToNum(strFrame.Substring(10, 4));
                        _type = false;
                        break;
                    }

                    //}
                    //if (_code == 1)
                    //{
                    //    string _privilege = "9610" + _wudp.NumToStrHex(i, 1);
                    //    string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), _privilege);
                    //    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                    //    _temp = strFrame.Substring(21, 32);
                    //    t = _wudp.StrHexToNum(_temp);
                    //    if (t == 0)
                    //    {
                    //        temp = _wudp.StrHexToNum(strFrame.Substring(10, 4));
                    //        break;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            //return timeIndex;
        }

        private bool SetTime(_Time t, long timeIndex, string controllerSN)
        {
            bool temp = false;
            string setTime = string.Empty;
            string strFrame = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                setTime = _wudp.NumToStrHex(31, 1);                                 //星期控制
                setTime += _wudp.NumToStrHex(0, 1);                                 //下一链接时段(0--表示无)
                setTime += _wudp.NumToStrHex(0, 2);                                 //保留2字节(0填充)
                setTime += _wudp.MSDateHmsToWCDateHms(t.starTime1);                  //起始时分秒1
                setTime += _wudp.MSDateHmsToWCDateHms(t.endTime1);                  //终止时分秒1
                setTime += _wudp.MSDateHmsToWCDateHms(t.starTime2);                  //起始时分秒2
                setTime += _wudp.MSDateHmsToWCDateHms(t.endTime2);                  //终止时分秒2
                setTime += _wudp.MSDateHmsToWCDateHms(t.starTime3);                  //起始时分秒3
                setTime += _wudp.MSDateHmsToWCDateHms(t.endTime3);                  //终止时分秒3
                setTime += _wudp.MSDateYmdToWCDateYmd(t.starDate);                   //起始日期
                setTime += _wudp.MSDateYmdToWCDateYmd(t.endDate);                   //终止日期
                setTime += _wudp.NumToStrHex(0, 4);                                 //保留4字节(0填充)
                if (Convert.ToInt32(setTime.Length) != (24 * 2))
                {
                    //_wudp = null;
                }
                string setStr = "9710" + _wudp.NumToStrHex(timeIndex, 2) + setTime;
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), setStr);  //生成指令帧

                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    temp = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                }
                else
                {
                    temp = true;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        public override bool SetControllerType(string controllerSN, int doorIndex, Functions.ControlEnum controlIndex, double setTime)
        {
            return this.Allcontrol(controllerSN, doorIndex, controlIndex, setTime);
        }

        private bool Allcontrol(string controllerSN, int doorIndex, Functions.ControlEnum controlIndex, double setTime)
        {
            bool temp = false;
            string privilege = string.Empty;
            string strCmd = string.Empty;
            //string ipAddr = GetDeviceNetWorkInfo(controllerSN).IPAddr;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                privilege += _wudp.NumToStrHex(Convert.ToInt32(doorIndex), 1);
                privilege += _wudp.NumToStrHex(Convert.ToInt16(controlIndex), 1);



                //string time = (Convert.ToInt32(setTime)).ToString("X");

                string time = (Convert.ToInt32(Math.Ceiling(setTime))).ToString("X");


                strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), "8F10" + privilege + time);
                string strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                    temp = false;
                }
                else
                {
                    temp = true;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        //public bool Allcontrol(long controllerSN, int doorIndex, long controlIndex, int setTime)
        //{
        //    bool temp = false;
        //    string privilege = string.Empty;
        //    string strCmd = string.Empty;
        //    //string ipAddr = GetDeviceNetWorkInfo(controllerSN).IPAddr;
        //    string ipAddr = CheckIP(controllerSN);
        //    try
        //    {
        //        privilege += _wudp.NumToStrHex(doorIndex, 1);
        //        privilege += _wudp.NumToStrHex(controlIndex, 1);

        //        string time = setTime.ToString("X");
        //        strCmd = _wudp.CreateBstrCommand(controllerSN, "8F10" + privilege + time);    //在线控制的时候1E是延续3秒

        //        string strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
        //        if (_wudp.ErrCode != 0 || strFrame == "")
        //        {
        //            string ErrorDes = this._wudp.ErrMessage;
        //            if (ErrorDes.Trim() == "")
        //            {
        //                ErrorDes = GetErrorDesc(this._wudp.ErrCode);

        //            }
        //            ShowErrorEx(ErrorDes);
        //            temp = false;
        //            _wudp = null;
        //        }
        //        else
        //        {
        //            temp = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
        //        exception.ExceptionObject = ex;
        //        base.OnReaderException(exception);
        //    }
        //    return temp;
        //}

        public override bool DelAllPermissions(string controllerSN)
        {
            return this.ClearAllPermissions(controllerSN);
        }

        private bool ClearAllPermissions(string controllerSN)
        {
            bool temp = false;
            string strFrame = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), "9310");
                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    temp = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                }
                else
                {
                    temp = true;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        public override bool DelOnePermission(string controllerSN, int doorIndex, string cardno)
        {
            return this.DelOnepermissions(controllerSN, doorIndex, cardno);
        }

        private bool DelOnepermissions(string controllerSN, int doorIndex, string cardno)
        {
            bool temp = false;
            string strFrame = string.Empty;
            string delOne = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                delOne += _wudp.CardToStrHex(Convert.ToInt32(cardno));
                delOne += _wudp.NumToStrHex(Convert.ToInt32(doorIndex), 1);

                string _privilege = "0811" + _wudp.NumToStrHex(0, 2) + delOne;
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), _privilege);
                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);   //发送指令, 并获取返回信息

                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    temp = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                }
                else
                {
                    temp = true;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        public override bool AddPermission(string controllerSN, int doorIndex, string cardno, Functions._Time functionTime, DateTime starDate, DateTime endDate)
        {
            return this.AddOnePermissions(controllerSN, doorIndex, cardno, functionTime, starDate, endDate);
        }

        private bool AddOnePermissions(string controllerSN, int doorIndex, string cardno, Functions._Time functionTime, DateTime starDate, DateTime endDate)
        {
            bool _setTime = true;

            bool temp = false;
            string strFrame = string.Empty;
            string privilege = string.Empty;
            string ipAddr = CheckIP(controllerSN);

            long timeIndex = 0;
            List<_Time> _tList = new List<_Time>();
            _tList = set(functionTime, starDate, endDate);

            for (int i = 0; i < _tList.Count; i++)
            {
                if (_tList[i].endDate != null)
                {
                    bool _type = false;
                    checkIndex(controllerSN, _tList[i], out _type, out timeIndex);
                    //if (timeIndex == 0)
                    //{
                    //    timeIndex = checkIndex(controllerSN, _tList[i], 1);
                    if (_type == false)
                    {
                        _setTime = SetTime(_tList[i], timeIndex, controllerSN);
                    }
                    //if (ipAddr == "")
                    //{
                    //    _setTime = SetTime(_tList[0], timeIndex, controllerSN, _ipAddr);
                    //}
                    //else
                    //    _setTime = SetTime(_tList[0], timeIndex, controllerSN, ipAddr);
                    //}
                    //else
                    //    _setTime = true;
                    if (_setTime)
                    {
                        try
                        {
                            privilege = _wudp.CardToStrHex(Convert.ToInt32(cardno));                         //卡号
                            privilege += _wudp.NumToStrHex(Convert.ToInt32(doorIndex), 1);                   //门号
                            privilege += _wudp.MSDateYmdToWCDateYmd(_tList[i].starDate);          //有效起始日期   "2007-8-14"
                            privilege += _wudp.MSDateYmdToWCDateYmd(_tList[i].endDate);           //有效截止日期    "2020-12-31"
                            privilege += _wudp.NumToStrHex(timeIndex, 1);                   //时段索引号

                            privilege += _wudp.NumToStrHex(123456, 3);                     //用户密码
                            privilege += _wudp.NumToStrHex(0, 4);                           //备用4字节(用0填充)
                            if (Convert.ToInt32(privilege.Length) != 16 * 2)
                            {
                                //_wudp = null;
                            }
                            string _privilege = "0711" + _wudp.NumToStrHex(0, 2) + privilege;
                            string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), _privilege);  //生成指令帧

                            if (ipAddr == "")
                            {
                                strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);         //发送指令, 并获取返回信息

                            }
                            else
                            {
                                strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                            }
                            if (_wudp.ErrCode != 0 || strFrame == "")
                            {
                                string ErrorDes = this._wudp.ErrMessage;
                                if (ErrorDes.Trim() == "")
                                {
                                    ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                                }
                                ShowErrorEx(ErrorDes);
                                temp = false;
                                //_wudp = null;
                            }
                            else
                            {
                                temp = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                            exception.ExceptionObject = ex;
                            base.OnReaderException(exception);
                        }
                    }
                }
            }
            return temp;
        }

        public override bool AddPermissions(string controllerSN, List<int> doorIndexs, List<string> cardnos, Functions._Time functionTime, DateTime starDate, DateTime endDate)
        {
            return this.AddAllPermissions(controllerSN, doorIndexs, cardnos, functionTime, starDate, endDate);
        }

        private bool AddAllPermissions(string controllerSN, List<int> doorIndex, List<string> cardno, Functions._Time functionTime, DateTime starDate, DateTime endDate)
        {
            bool _setTime = true;

            bool temp = false;
            string strFrame = string.Empty;
            string privilege = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            int privilegeIndex = 0;

            long timeIndex = 0;
            List<_Time> _tList = new List<_Time>();
            _tList = set(functionTime, starDate, endDate);

            for (int i = 0; i < _tList.Count; i++)
            {
                if (_tList[i].endDate != null)
                {
                    bool _type = false;
                    checkIndex(controllerSN, _tList[i], out _type, out timeIndex);
                    //if (timeIndex == 0)
                    //{
                    //timeIndex = checkIndex(controllerSN, _tList[i], 1);
                    if (!_type)
                        _setTime = SetTime(_tList[i], timeIndex, controllerSN);
                    //if (ipAddr == "")
                    //{
                    //    _setTime = SetTime(_tList[0], timeIndex, controllerSN, _ipAddr);
                    //}
                    //else
                    //    _setTime = SetTime(_tList[0], timeIndex, controllerSN, ipAddr);
                    //}
                    //else
                    //    _setTime = true;
                    if (_setTime)
                    {
                        try
                        {
                            doorIndex.Sort();
                            cardno.Sort();

                            for (int dIndex = 0; dIndex < doorIndex.Count; dIndex++)
                            {
                                for (int cIndex = 0; cIndex < cardno.Count; cIndex++)
                                {
                                    privilege = _wudp.CardToStrHex(Convert.ToInt32(cardno[cIndex]));                         //卡号
                                    privilege += _wudp.NumToStrHex(Convert.ToInt32(doorIndex[dIndex]), 1);                   //门号
                                    privilege += _wudp.MSDateYmdToWCDateYmd(_tList[i].starDate);            //有效起始日期   "2007-8-14"
                                    privilege += _wudp.MSDateYmdToWCDateYmd(_tList[i].endDate);             //有效截止日期    "2020-12-31"
                                    privilege += _wudp.NumToStrHex(timeIndex, 1);                           //时段索引号

                                    privilege += _wudp.NumToStrHex(123456, 3);                              //用户密码
                                    privilege += _wudp.NumToStrHex(0, 4);                                   //备用4字节(用0填充)
                                    if (Convert.ToInt32(privilege.Length) != 16 * 2)
                                    {
                                        //_wudp = null;
                                        string _privilege = "9B10" + _wudp.NumToStrHex(privilegeIndex, 2) + privilege;
                                        string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), _privilege);      //生成指令帧

                                        if (ipAddr == "")
                                        {
                                            strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);                 //发送指令, 并获取返回信息

                                        }
                                        else
                                        {
                                            strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                                        }
                                        if (_wudp.ErrCode != 0 || strFrame == "")
                                        {
                                            string ErrorDes = this._wudp.ErrMessage;
                                            if (ErrorDes.Trim() == "")
                                            {
                                                ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                                            }
                                            ShowErrorEx(ErrorDes);
                                            temp = false;
                                            //_wudp = null;
                                        }
                                        else
                                        {
                                            temp = true;
                                            privilegeIndex = privilegeIndex + 1;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                            exception.ExceptionObject = ex;
                            base.OnReaderException(exception);
                        }
                    }
                }
            }
            return temp;
        }

        public override bool ClearSetTime(string controllerSN)
        {
            return this.SetTimeNull(controllerSN);
        }

        private bool SetTimeNull(string controllerSN)
        {
            _Time time = new _Time();
            time.starTime1 = null;
            time.endTime1 = null;
            time.starTime2 = null;
            time.endTime2 = null;
            time.starTime3 = null;
            time.endTime3 = null;
            time.starDate = null;
            time.endDate = null;


            bool temp = false;
            string setTime = string.Empty;
            string strFrame = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                for (int timeIndex = 3; timeIndex < 255; timeIndex++)
                {
                    setTime = _wudp.NumToStrHex(31, 1);                                 //星期控制
                    setTime += _wudp.NumToStrHex(0, 1);                                 //下一链接时段(0--表示无)
                    setTime += _wudp.NumToStrHex(0, 2);                                 //保留2字节(0填充)
                    setTime += _wudp.MSDateHmsToWCDateHms(time.starTime1);                  //起始时分秒1
                    setTime += _wudp.MSDateHmsToWCDateHms(time.endTime1);                  //终止时分秒1
                    setTime += _wudp.MSDateHmsToWCDateHms(time.starTime2);                  //起始时分秒2
                    setTime += _wudp.MSDateHmsToWCDateHms(time.endTime2);                  //终止时分秒2
                    setTime += _wudp.MSDateHmsToWCDateHms(time.starTime3);                  //起始时分秒3
                    setTime += _wudp.MSDateHmsToWCDateHms(time.endTime3);                  //终止时分秒3
                    setTime += _wudp.MSDateYmdToWCDateYmd(time.starDate);                   //起始日期
                    setTime += _wudp.MSDateYmdToWCDateYmd(time.endDate);                   //终止日期
                    setTime += _wudp.NumToStrHex(0, 4);                                 //保留4字节(0填充)
                    if (Convert.ToInt32(setTime.Length) == (24 * 2))
                    {
                        //_wudp = null;
                        string setStr = "9710" + _wudp.NumToStrHex(timeIndex, 2) + setTime;
                        string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), setStr);  //生成指令帧

                        if (ipAddr == "")
                        {
                            strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                        }
                        else
                        {
                            strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                        }
                        if (_wudp.ErrCode != 0 || strFrame == "")
                        {
                            temp = false;
                            string ErrorDes = this._wudp.ErrMessage;
                            if (ErrorDes.Trim() == "")
                            {
                                ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                            }
                            ShowErrorEx(ErrorDes);
                            //_wudp = null;
                        }
                        else
                        {
                            temp = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        string checkWeek(List<string> week)
        {
            int temp = 0;
            foreach (string item in week)
            {
                switch (item)
                {
                    case "1": temp = temp + 1; break;
                    case "2": temp = temp + 10; break;
                    case "3": temp = temp + 100; break;
                    case "4": temp = temp + 1000; break;
                    case "5": temp = temp + 10000; break;
                    case "6": temp = temp + 100000; break;
                    case "7": temp = temp + 1000000; break;
                }
            }
            string result = _wudp.NumToStrHex(Convert.ToInt32(temp.ToString(), 2), 1);
            return result;

        }

        //public bool TimingTask(string controllerSN, int doorIndex, DateTime time, string week, Functions.ControlEnum controlIndex)

        public override bool SetTimingTask(Functions._controllerTimingTask timeTask)
        {
            return this._SetTimingTask(timeTask);
        }

        private bool _SetTimingTask(Functions._controllerTimingTask timeTask)
        {
            bool temp = false;

            string strFrame = string.Empty;
            string privilege = string.Empty;
            string ipAddr = CheckIP(timeTask.controllerSN);
            //timeTask.week = checkWeek(timeTask.week);
            string week = checkWeek(timeTask.week);
            try
            {
                privilege += _wudp.NumToStrHex(0, 1);
                privilege += _wudp.NumToStrHex(1, 1);
                privilege += _wudp.NumToStrHex(timeTask.timeTaskIndex, 2);

                privilege += _wudp.NumToStrHex(6, 1);
                privilege += _wudp.MSDateHmsToWCDateHms(timeTask.time.ToString("HH:mm:00"));
                privilege += week;
                privilege += _wudp.NumToStrHex(Convert.ToInt32(timeTask.doorIndex), 1);
                privilege += _wudp.NumToStrHex(Convert.ToInt16(timeTask.controlIndex), 1);

                string _privilege = "F510" + privilege + _wudp.NumToStrHex(0, 2);
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(timeTask.controllerSN), _privilege);  //生成指令帧

                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);         //发送指令, 并获取返回信息

                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (strCmd.Substring(21, 12) == strFrame.Substring(11, 12))
                {
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }

            return temp;
        }

        public override bool TimingTask(string controllerSN)
        {
            return this._TimingTask(controllerSN);
        }

        private bool _TimingTask(string controllerSN)
        {
            bool temp = false;
            string strFrame = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), "F410360001");
                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    temp = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                }
                else
                {
                    strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), "F3100001");
                    if (ipAddr == "")
                    {
                        strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                    }
                    else
                    {
                        strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                    }
                    if (_wudp.ErrCode != 0 || strFrame == "")
                    {
                        temp = false;
                        string ErrorDes = this._wudp.ErrMessage;
                        if (ErrorDes.Trim() == "")
                        {
                            ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                        }
                        ShowErrorEx(ErrorDes);
                        //_wudp = null;
                    }
                    else
                    {
                        temp = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        public override bool StarTimingTask(string controllerSN)
        {
            return this._StarTimingTask(controllerSN);
        }

        private bool _StarTimingTask(string controllerSN)
        {
            bool temp = false;
            string strFrame = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), "0911");
                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    temp = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                }
                else
                {
                    temp = true;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        public override bool EndTimingTask(string controllerSN)
        {
            return this._EndTimingTask(controllerSN);
        }

        private bool _EndTimingTask(string controllerSN)
        {
            bool temp = false;
            string strFrame = string.Empty;
            string ipAddr = CheckIP(controllerSN);
            try
            {
                string strCmd = _wudp.CreateBstrCommand(Convert.ToInt32(controllerSN), "F410360000");
                if (ipAddr == "")
                {
                    strFrame = _wudp.udp_comm(strCmd, _ipAddr, _iPort);
                }
                else
                {
                    strFrame = _wudp.udp_comm(strCmd, ipAddr, _iPort);
                }
                if (_wudp.ErrCode != 0 || strFrame == "")
                {
                    temp = false;
                    string ErrorDes = this._wudp.ErrMessage;
                    if (ErrorDes.Trim() == "")
                    {
                        ErrorDes = GetErrorDesc(this._wudp.ErrCode);

                    }
                    ShowErrorEx(ErrorDes);
                    //_wudp = null;
                }
                else
                {
                    temp = true;
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs exception = new ReaderExceptionEventArgs();
                exception.ExceptionObject = ex;
                base.OnReaderException(exception);
            }
            return temp;
        }

        #endregion

    }

    #region 卡信息Class

    /// <summary>
    /// 獲得卡信息

    /// </summary>
    public class iCCardInfo
    {
        WComm_Operate _wudp;
        public iCCardInfo()
        {
            _wudp = new WComm_Operate();
            controllerSN = 0;
            cardID = 0;
            status = 0;
            swipedate = "";

        }

        ~iCCardInfo()
        {
            _wudp = null;
        }

        public long controllerSN { get; set; } //讀寫器SN
        public long cardID { get; set; }         //讀取到的卡號 8位


        public string statusDesc
        {
            get
            {
                string hexstatus = this._wudp.NumToStrHex(status, 1).ToString().Substring(0, 1);
                switch (hexstatus)
                {
                    case "0":
                        return "讀卡器刷卡開門";
                    case "8":
                        return "讀卡器刷卡禁止通過: 原因不明";
                    case "9":
                        return "讀卡器刷卡禁止通過: 沒有許可權";
                    case "A":
                        return "讀卡器刷卡禁止通過: 密碼不對";
                    case "B":
                        return "讀卡器刷卡禁止通過: 系統有故障";
                    default:
                        return "";
                }
            }
        }   // 狀態描述 
        public long status { get; set; }  //刷卡狀態


        public string swipedate { get; set; }   //刷卡年月日


        public long doorid
        {
            get { return long.Parse(this._wudp.NumToStrHex(status, 1).ToString().Substring(1, 1)) + 1; }
        }    //刷卡門號


    }
    #endregion


    #region 控制器網絡配置信息


    /// <summary>
    /// 控制器網絡配置信息

    /// </summary>
    public class DeviceNetWorkInfo
    {
        public DeviceNetWorkInfo()
        {
            MacAddr = "";
            IPAddr = "";
            subNet = "";
            gateway = "";
            UdpPort = "";
        }

        public string MacAddr { get; set; }
        public string IPAddr { get; set; }
        public string subNet { get; set; }
        public string gateway { get; set; }
        public string UdpPort { get; set; }

    }

    #endregion


}
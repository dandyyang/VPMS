using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;
using PaymentEquipment.Entity;
using PaymentEquipment.DLL;
using System.Threading;

namespace PaymentEquipment.DeviceImplement
{
    public class LeoJocatPayDevice : AbstractPayDevice
    {
        private int m_intPaymentDataNum = 8;

        public override bool Conn(int iCom, int iBaud)
        {
            try
            {
                this.ComPort = iCom;
                this.BaudRate = iBaud;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public override bool Conn()
        {
            try
            {
                this.GetDeviceDateTime(this.MachinesNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 设置管理卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public override bool SetAdminCard(int p_intDeviceNo, int p_intCardNo)
        {
            return JocatDevice.SetAdminCard(ComPort, BaudRate, p_intDeviceNo, p_intCardNo);
        }

        /// <summary>
        /// 设置机号
        /// </summary>
        /// <param name="p_intOldDeviceNo"></param>
        /// <param name="p_intNewDeviceNo"></param>
        /// <returns></returns>
        public override bool SetDeviceNo(int p_intOldDeviceNo, int p_intNewDeviceNo)
        {
            return JocatDevice.SetMachineNo(ComPort, BaudRate, p_intOldDeviceNo, p_intNewDeviceNo);
        }

        /// <summary>
        /// 同步机器时间
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override bool SyncDeviceDateTime(int p_intDeviceNo)
        {
            return JocatDevice.SetMachineTime(ComPort, BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 取得机器时间
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override DateTime GetDeviceDateTime(int p_intDeviceNo)
        {
            string machinesDateTime = JocatDevice.GetMachineTime(ComPort, BaudRate, p_intDeviceNo);

            DateTime machinesTime;

            try
            {
                machinesTime = Convert.ToDateTime(machinesDateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return machinesTime;
        }

        /// <summary>
        /// 添加挂失卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public override bool AddLossCard(int p_intDeviceNo, int p_intCardNo)
        {
            return JocatDevice.AddLossCard(ComPort, BaudRate, p_intDeviceNo, p_intCardNo);
        }

        /// <summary>
        /// 移除挂失卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intCardNo"></param>
        /// <returns></returns>
        public override bool RemoveLossCard(int p_intDeviceNo, int p_intCardNo)
        {
            return JocatDevice.RemoveLossCard(ComPort, BaudRate, p_intDeviceNo, p_intCardNo);
        }

        /// <summary>
        /// 清除挂失卡
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override bool CleanLossCard(int p_intDeviceNo)
        {
            return JocatDevice.CleanLossCard(ComPort, BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 设置最大消费金额
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_intMoney"></param>
        /// <returns></returns>
        public override bool SetMaxPayMoney(int p_intDeviceNo, int p_intMoney)
        {
            return JocatDevice.SetPaymentMaxAmount(ComPort, BaudRate, p_intDeviceNo, p_intMoney);
        }

        /// <summary>
        /// 设置机器消费密码
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <param name="p_strOldPassword"></param>
        /// <param name="p_strNewPassword"></param>
        /// <returns></returns>
        public override bool SetDevicePassword(int p_intDeviceNo, string p_strOldPassword, string p_strNewPassword)
        {
            return JocatDevice.SetMachinePassword(ComPort, BaudRate, p_intDeviceNo, p_strOldPassword, p_strNewPassword);
        }

        /// <summary>
        /// 取得消费记录条数
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override int GetPaymentNum(int p_intDeviceNo)
        {
            return JocatDevice.GetRecordNum(ComPort, BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 取得消费总金额
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override double GetPaymentMoney(int p_intDeviceNo)
        {
            return JocatDevice.GetMacPaymentMoney(ComPort, BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 取得消费资料
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override List<PaymentInfo> GetPaymentInfo(int p_intDeviceNo)
        {
            try
            {
                //取得消费记录数

                int l_intRecordNum = GetPaymentNum(p_intDeviceNo);

                //Thread.Sleep(3000);

                //如果未能连接到机械

                if (l_intRecordNum == 0)
                {
                    string data = JocatDevice.GetPaymentInfo(ComPort, BaudRate, p_intDeviceNo, 0);
                    return new List<PaymentInfo>();
                }
                if (l_intRecordNum == -1)
                {
                    return null;
                }

                List<PaymentInfo> l_lisReturn = new List<PaymentInfo>();

                string l_strData = "";

                for (int i = 0; i < l_intRecordNum; i = i + m_intPaymentDataNum)
                {
                    //l_strData = JocatDevice.GetPaymentInfo(ComPort, BaudRate, p_intDeviceNo, i);
                    l_strData = JocatDevice.GetPaymentInfo(int.Parse(ComPort.ToString()), int.Parse(BaudRate.ToString()), int.Parse(p_intDeviceNo.ToString()), i);

                    if (l_strData == "85" || l_strData == "86")
                    {
                        i = i - m_intPaymentDataNum;
                        continue;
                    }
                    if (l_strData.Length % 52 != 0)
                    {
                        i = i - m_intPaymentDataNum;
                        continue;
                    }

                    int l_intSeed = 0;
                    string l_strPaymentInfo = "";

                    //此处有可能遇到l_strData的值为85的情况

                    bool signSuccess = true;
                    List<PaymentInfo> l_tmpPaymentInfos = new List<PaymentInfo>();
                    while (l_intSeed < l_strData.Length)
                    {
                        l_strPaymentInfo = l_strData.Substring(l_intSeed, 52);
                        try
                        {
                            l_tmpPaymentInfos.Add(EncodePaymentInfo(l_strPaymentInfo));
                        }
                        catch (Exception Ex)
                        {
                            signSuccess = false;
                            break;
                        }

                        l_intSeed = l_intSeed + 52;
                    }

                    if (signSuccess)
                    {
                        l_lisReturn.AddRange(l_tmpPaymentInfos);
                    }
                    else
                    {
                        //捕抓机台返回的错误数据，使循环返回对上一次序列重新取数。

                        i = i - m_intPaymentDataNum;
                    }

                    if ((i + m_intPaymentDataNum) > l_intRecordNum)
                    {
                        Thread.Sleep(3000);
                        break;
                    }

                    Thread.Sleep(3000);
                }

                return l_lisReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 清除消费资料
        /// </summary>
        /// <param name="p_intDeviceNo"></param>
        /// <returns></returns>
        public override bool CleanPaymentInfo(int p_intDeviceNo)
        {
            return JocatDevice.CleanPaymentInfo(ComPort, BaudRate, p_intDeviceNo);
        }

        /// <summary>
        /// 解译消费机的消费数据
        /// </summary>
        /// <param name="p_strData">消费数据</param>
        /// <returns></returns>
        private PaymentInfo EncodePaymentInfo(string p_strData)
        {
            string l_strPaymentInfo = p_strData;
            PaymentInfo l_objPaymentData = new PaymentInfo()
            {
                CardNo = int.Parse(l_strPaymentInfo.Substring(17, 6)),
                DeviceNo = int.Parse(l_strPaymentInfo.Substring(0, 3)),
                PrivateMoney = decimal.Parse(l_strPaymentInfo.Substring(23, 8)) / 100,
                OtherMoney = decimal.Parse(l_strPaymentInfo.Substring(31, 8)) / 100,
                PayemtnMoney = decimal.Parse(l_strPaymentInfo.Substring(39, 8)) / 100,
                PaymentDate = DateTime.ParseExact(l_strPaymentInfo.Substring(3, 14), "yyyyMMddHHmmss", null),
                RecordNo = 0,
                Remark = ""
            };

            return l_objPaymentData;
        }
    }
}

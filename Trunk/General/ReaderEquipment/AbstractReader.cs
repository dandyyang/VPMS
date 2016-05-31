using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderEquipment.Entity;
using AxMSCommLib;
using ReaderEquipment.Function;

namespace ReaderEquipment
{
    public abstract class AbstractReader : System.Windows.Forms.Form
    {
        #region 字段、属性、事件

        public object Other { set; get; }
        /// <summary>
        /// 机器编号
        /// </summary>
        public virtual int MachineID { set; get; }
        /// <summary>
        /// 连接串口号
        /// </summary>
        public short CommPort { set; get; }
        /// <summary>
        /// 标签序号
        /// </summary>
        public string TagIndex { set; get; }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string ReaderIP { set; get; }
        /// <summary>
        /// 控制器SN
        /// </summary>
        public string ControllerSN { get; set; }
        /// <summary>
        /// 是否正在读数
        /// </summary>
        public bool IsReading;

        public event EventHandler<TagInfoResultEventArgs> TagInformationResult;

        /// <summary>
        /// 异常事件
        /// </summary>
        public event EventHandler<ReaderExceptionEventArgs> ReaderException;

        /// <summary>
        /// 设备状态变化事件
        /// </summary>
        public event EventHandler<EquipmentStatusEventArgs> EquipmentStatusChange;

        #endregion

        public AbstractReader()
        {
            //this.OnTagInformationResult = null;

            this.CommPort = 1;
        }

        /// <summary>
        /// 开始读数
        /// </summary>
        /// <returns></returns>
        public abstract ReturnValueInfo StartRead();

        /// <summary>
        /// 停止读数
        /// </summary>
        public abstract void StopRead();

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <returns></returns>
        public abstract ReturnValueInfo Connect();

        /// <summary>
        /// 断开设备
        /// </summary>
        /// <returns></returns>
        public virtual bool Disconnect()
        {
            return true;
        }

        /// <summary>
        /// 获取读取到的标签数据
        /// </summary>
        /// <param name="strAntennaNum">天线号</param>
        /// <returns></returns>
        public abstract List<TagInformationInfo> GetTagList(string strAntennaNum);

        /// <summary>
        /// 获取读取到的标签数据
        /// </summary>
        /// <param name="strAntennaNum">天线号</param>
        /// <param name="lIsDelHistory">是否删除利是数据</param>
        /// <returns></returns>
        public abstract List<TagInformationInfo> GetTagList(string strAntennaNum, bool lIsDelHistory);

        /// <summary>
        /// 数据确认
        /// </summary>
        /// <param name="strSnNo"></param>
        /// <returns></returns>
        public virtual bool DataAffirm(string strSnNo)
        {
            return true;
        }

        /// <summary>
        /// 抹除读写器的数据
        /// </summary>
        /// <param name="strMacID"></param>
        /// <returns></returns>
        public virtual bool DeleteData(string strMacID)
        {
            return true;
        }

        /// <summary>
        /// 校准控制器时间
        /// </summary>
        /// <param name="strSN">产品序列号</param>
        /// <param name="strIP">IP 地址为空, 表示广播包方式</param>
        /// <returns></returns>
        public virtual bool AdjustClockByPCTime(string strSN, string strIP)
        {
            return true;
        }

        /// <summary>
        /// 发布读标签结果信息
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnTagResult(TagInfoResultEventArgs tagArgs)
        {
            if (this.TagInformationResult != null)
            {
                this.TagInformationResult(this, tagArgs);
            }
        }

        /// <summary>
        /// 发布出错信息
        /// </summary>
        /// <param name="exception"></param>
        protected void OnReaderException(ReaderExceptionEventArgs readerEx)
        {
            if (this.ReaderException != null)
            {
                this.ReaderException(this, readerEx);
            }
        }

        /// <summary>
        /// 状态变化事件
        /// </summary>
        /// <param name="status"></param>
        protected void OnStatusChange(EquipmentStatusEventArgs equipStatus)
        {
            if (this.EquipmentStatusChange != null)
            {
                this.EquipmentStatusChange(this, equipStatus);
            }
        }

        /// <summary>
        /// 添加单个权限
        /// </summary>
        /// <param name="strSN">控制器SN</param>
        /// <param name="iDoorIndex">门号</param>
        /// <param name="strCardNo">卡号</param>
        /// <param name="functionTime">时间段</param>
        /// <param name="dtStart">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns></returns>
        public virtual bool AddPermission(string strSN, int iDoorIndex, string strCardNo, Functions._Time functionTime, DateTime dtStart, DateTime dtEnd)
        {
            return true;
        }

        /// <summary>
        /// 批量添加权限
        /// </summary>
        /// <param name="strSN">控制器SN</param>
        /// <param name="iDoorIndex">门号</param>
        /// <param name="listCardNo">卡号列表</param>
        /// <param name="functionTime">时间段</param>
        /// <param name="dtStart">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns></returns>
        public virtual bool AddPermissions(string strSN, List<int> iDoorIndex, List<string> listCardNo, Functions._Time functionTime, DateTime dtStart, DateTime dtEnd)
        {
            return true;
        }

        /// <summary>
        /// 清空权限
        /// </summary>
        /// <param name="strSN">控制器SN</param>
        /// <returns></returns>
        public virtual bool DelAllPermissions(string strSN)
        {
            return true;
        }

        /// <summary>
        /// 清除单个卡的权限
        /// </summary>
        /// <param name="strSN">控制器SN</param>
        /// <param name="strCardNo">卡号</param>
        /// <param name="iDoorIndex">门号</param>
        /// <returns></returns>
        public virtual bool DelOnePermission(string strSN, int iDoorIndex, string strCardNo)
        {
            return true;
        }

        /// <summary>
        /// 控制方式
        /// </summary>
        /// <param name="strSN">控制器SN</param>
        /// <param name="iDoorIndex">门号</param>
        /// <param name="controlIndex">控制方式</param>
        /// <param name="fDelayTime">延续时间</param>
        /// <returns></returns>
        public virtual bool SetControllerType(string strSN, int iDoorIndex, Functions.ControlEnum controlIndex, double fDelayTime)
        {
            return true;
        }

        /// <summary>
        /// 清空时间段
        /// </summary>
        /// <param name="strSN">控制器SN</param>
        /// <returns></returns>
        public virtual bool ClearSetTime(string strSN)
        {
            return true;
        }

        public virtual bool TimingTask(string strSN)
        {
            return true;
        }

        public virtual bool StarTimingTask(string strSN)
        {
            return true;
        }

        public virtual bool SetTimingTask(Functions._controllerTimingTask timeTask)
        {
            return true;
        }

        public virtual bool EndTimingTask(string strSN)
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderEquipment.Entity;

namespace ReaderEquipment
{
    /// <summary>
    /// 读写器抽象类（非winform派生类）
    /// </summary>
    public abstract class AbstractReaderBackground
    {
        public virtual int MachineID { get; set; }

        public string ReaderIP { get; set; }

        public int PortNum { get; set; }

        public AbstractReaderBackground()
        {

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
        /// 获取批量记录
        /// </summary>
        /// <returns></returns>
        public virtual List<TagInformationInfo> GetTagList(string strMacID)
        {
            return null;
        }

        /// <summary>
        /// 校准控制器时间
        /// </summary>
        /// <param name="strMacID">机器号</param>
        /// <returns></returns>
        public virtual bool AdjustClockByPCTime(string strMacID)
        {
            return true;
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <returns></returns>
        public abstract ReturnValueInfo Connect();

        /// <summary>
        /// 断开设备
        /// </summary>
        /// <returns></returns>
        public abstract bool Disconnect();

        /// <summary>
        /// 设备数据抛出事件
        /// </summary>
        public event EventHandler<TagInfoResultEventArgs> TagInformationResult;
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
        /// 异常事件
        /// </summary>
        public event EventHandler<ReaderExceptionEventArgs> ReaderException;
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
        /// 设备状态变化事件
        /// </summary>
        public event EventHandler<EquipmentStatusEventArgs> EquipmentStatusChange;
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
    }
}

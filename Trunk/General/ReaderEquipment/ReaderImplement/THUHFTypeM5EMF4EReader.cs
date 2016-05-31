using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderEquipment.Entity;
using ModuleTech;
using System.Windows.Forms;
using System.Threading;
using Leo.PLDMS.ProcessTagService.Entity;
using ModuleLibrary;

namespace ReaderEquipment.ReaderImplement
{
    public class THUHFTypeM5EMF4EReader : AbstractReader
    {
        /// <summary>
        /// 与读写器交互的API类
        /// </summary>
        private Reader _readerAPI = null;

        /// <summary>
        /// 是否正在连接中
        /// </summary>
        private bool _isConnected = false;

        /// <summary>
        /// 是否在读数中
        /// </summary>
        private bool _isReading = false;

        /// <summary>
        /// 读取读写器的线程
        /// </summary>
        private Thread _readerThread = null;

        /// <summary>
        /// 单次读取Reader的时间长度，单位毫秒。
        /// </summary>
        private int _readTime = 350;

        /// <summary>
        /// 每次读之间的休息时间
        /// </summary>
        private int _readSpace = 0;

        /// <summary>
        /// 過濾Tag編號類
        /// </summary>
        private ProcessTag _processTag = null;

        private int _retryCount = 0;
        private int _maxRetryTimes = 2;

        public THUHFTypeM5EMF4EReader()
        {
            _processTag = new ProcessTag();
            _processTag.EventTagHandler += new ProcessTag.tagHandler(_processTag_EventTagHandler);
        }

        void _processTag_EventTagHandler(TagEntity tag)
        {
            TagInfoResultEventArgs args = new TagInfoResultEventArgs();
            args.TagInformation = new TagInformationInfo();
            args.TagInformation.TagID = tag.TagId;
            OnTagResult(args);
        }

        /// <summary>
        /// 开始监听读写器
        /// </summary>
        /// <returns></returns>
        public override ReturnValueInfo StartRead()
        {
            #region 连接读写器

            try
            {
                //if (_readerAPI != null && _isConnected)
                //{
                //    _readerAPI.Disconnect();
                //    _readerAPI = null;
                //}

                StopRead();

                //创建Reader
                //默认使用ModuleTech.Region.NA
                string readerIP = base.ReaderIP;
                _readerAPI = Reader.Create(readerIP, ModuleTech.Region.NA, (ReaderType)(5));
                _readerAPI.ParamSet("CheckAntConnection", false);
                Console.WriteLine("连接成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Thread.Sleep(500);

                _retryCount++;
                if (_retryCount < _maxRetryTimes)
                {
                    Console.WriteLine("重新连接中。");
                    StartRead();
                    return null;
                }
                else
                {
                    Application.Exit();
                    return null;
                }
            }

            #endregion

            #region 读取数据

            try
            {
                _retryCount = 0;
                //_readerAPI = null;
                SetAntennas();
                _isConnected = _isReading = true;

                //开启过滤类
                _processTag.Start();
                //开始读写进程
                _readerThread = new Thread(readFunc);
                _readerThread.Start();
                Console.WriteLine("正在读取中。");
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常信息：{0}。", ex.Message);
                throw ex;
            }

            #endregion

            return null;
        }

        /// <summary>
        /// 读取的操作
        /// </summary>
        void readFunc()
        {
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(addTagToFilter);

            Thread threadActive = new Thread(threadStart);

            while (_isReading)
            {
                try
                {
                    TagReadData[] reads = _readerAPI.Read(_readTime);

                    List<TagEntity> entityList = new List<TagEntity>();

                    foreach (TagReadData read in reads)
                    {
                        entityList.Add(new TagEntity() { TagNo = read.EPCString, TagId = read.EPCString });
                    }

                    //出发保存tag
                    if (entityList.Count > 0)
                    {
                        threadActive = new Thread(threadStart);
                        threadActive.Start(entityList);
                    }

                    entityList = null;

                    Thread.Sleep(_readSpace);
                }
                catch (FatalInternalException ex)
                {

                    //找读写器内部出现错误， 应该重新创建读写器
                    Console.WriteLine("读写器内部出现错误，系统尝试重新连接,错误信息：{0}.", ex.Message);
                    ThreadStart threadReConnect = new ThreadStart(reConnect);
                    Thread thread = new Thread(threadReConnect);
                    thread.Start();
                    return;
                }
                catch (Exception exx)
                {
                    ReaderExceptionEventArgs exceptionArgs = new ReaderExceptionEventArgs();
                    exceptionArgs.ExceptionObject = new Exception();
                    exceptionArgs.ExceptionObject.Source = "重新连接后失败，请检查网络。错误信息：" + exx.Message;
                    OnReaderException(exceptionArgs);
                    //throw exx;
                }
            }
        }

        void addTagToFilter(object tagEntity)
        {
            List<TagEntity> tagEntityList = tagEntity as List<TagEntity>;
            _processTag.AddTagToWaittingProcessTagQueue(tagEntityList);
        }

        void reConnect()
        {
            StartRead();
        }

        /// <summary>
        /// 设置天线
        /// </summary>
        private void SetAntennas()
        {
            try
            {
                List<AntPower> antennaSet = new List<AntPower>();
                List<int> antennaRun = new List<int>();
                for (int i = 1; i <= 4; i++)
                {
                    antennaSet.Add(new AntPower(
                        (byte)i, //天线顺序编号
                        (ushort)(20 * 100),   //读功率
                        (ushort)(20 * 100)    //写功率
                        ));
                    antennaRun.Add(i);
                }

                //设置天线功率强度
                _readerAPI.ParamSet("AntPowerConf", antennaSet.ToArray());

                SimpleReadPlan searchPlan = new SimpleReadPlan(antennaRun.ToArray());

                //设置哪些天线需要运作
                _readerAPI.ParamSet("ReadPlan", searchPlan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void StopRead()
        {
            try
            {
                _processTag.Stop();
                _isReading = false;

                if (_readerThread != null)
                {
                    _readerThread.Join();
                }

                if (_readerAPI != null && _isConnected)
                {
                    _readerAPI.Disconnect();
                    _isConnected = false;
                    Console.WriteLine("停止連接.");
                }

                _readerAPI = null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override ReturnValueInfo Connect()
        {
            return null;
        }

        public override List<TagInformationInfo> GetTagList(string antennaNum)
        {
            throw new NotImplementedException();
        }

        public override List<TagInformationInfo> GetTagList(string antennaNum, bool DeleteHistory)
        {
            throw new NotImplementedException();
        }
    }

    public class ProcessTag
    {
        public event tagHandler EventTagHandler;

        public delegate void tagHandler(TagEntity tag);

        public void ShowTag(TagEntity tag)
        {

            if (EventTagHandler != null)
            {
                EventTagHandler(tag);
            }
        }

        /// <summary>
        /// 待处理队列
        /// </summary>
        private Queue<TagEntity> _queWaittingProcessTag;

        /// <summary>
        /// 已读取的所有效tagNo,以過濾reader讀的重復tag,定時根據每個tag的生命周期清空;
        /// </summary>
        private Dictionary<string, DateTime> _dicAllValidTag;

        private System.Timers.Timer _trProcessTag;
        private System.Timers.Timer _trResetAllValidTag;

        /// <summary>
        /// 每個tag在allValidTag的生命周期初始化單位Second;
        /// </summary>
        private int _iTaglife = 3;
        /// <summary>
        /// 清理allValidTag中已过期的tag的频率,单位毫秒 
        /// </summary>
        private int _iResetAllValidTagInterval = 1000 * 2;
        /// <summary>
        /// 从队列中取出tag处理的频率,单位毫秒
        /// </summary>
        private int _iProcessTagInterval = 100;

        public ProcessTag()
        {
            _queWaittingProcessTag = new Queue<TagEntity>();
            _dicAllValidTag = new Dictionary<string, DateTime>();
            _trProcessTag = new System.Timers.Timer();
            _trProcessTag.Interval = _iProcessTagInterval;
            _trProcessTag.Elapsed += new System.Timers.ElapsedEventHandler(_trProcessTag_Elapsed);

            _trResetAllValidTag = new System.Timers.Timer();
            _trResetAllValidTag.Interval = _iResetAllValidTagInterval;
            _trResetAllValidTag.Elapsed += new System.Timers.ElapsedEventHandler(_trResetAllValidTag_Elapsed);
        }

        void _trResetAllValidTag_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetAllValidTag(_iTaglife);
        }

        void _trProcessTag_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                TagEntity tag = this.GetTagFromwaittingProcessTagQueue();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Start()
        {
            ResetwaittingProcessTagQueue();
            ResetAllValidTag();
            _trProcessTag.Start();
            _trResetAllValidTag.Start();
        }

        public void Stop()
        {
            _trProcessTag.Stop();
            _trResetAllValidTag.Stop();
        }

        /// <summary>
        /// 清空待处理tag
        /// </summary>
        private void ResetwaittingProcessTagQueue()
        {
            lock (this._queWaittingProcessTag)
            {
                this._queWaittingProcessTag.Clear();
            }
        }

        /// <summary>
        /// 清空已讀取tag
        /// </summary>
        public void ResetAllValidTag()
        {
            lock (this._dicAllValidTag)
            {
                this._dicAllValidTag.Clear();
            }
        }

        /// <summary>
        ///  將生命周期已過的tag移除
        /// </summary>
        /// <param name="second"></param>
        public void ResetAllValidTag(int second)
        {
            lock (this._dicAllValidTag)
            {
                string[] tagNoArray = new string[this._dicAllValidTag.Keys.Count];
                this._dicAllValidTag.Keys.CopyTo(tagNoArray, 0);
                for (int i = 0; i < tagNoArray.Length; i++)
                {
                    if (((TimeSpan)DateTime.Now.Subtract(this._dicAllValidTag[tagNoArray[i]])).TotalMilliseconds >= 1000 * second)
                    {
                        this._dicAllValidTag.Remove(tagNoArray[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 取出一个待处理的tag
        /// </summary>
        /// <returns></returns>
        private TagEntity GetTagFromwaittingProcessTagQueue()
        {
            lock (this._queWaittingProcessTag)
            {
                if (this._queWaittingProcessTag.Count > 0)
                {
                    return this._queWaittingProcessTag.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///将新读到的tag加入待处理的队列中 
        /// </summary>
        /// <param name="tagArray"></param>
        public void AddTagToWaittingProcessTagQueue(List<TagEntity> tagArray)
        {
            lock (_dicAllValidTag)
            {
                foreach (TagEntity tag in tagArray)
                {
                    if (!_dicAllValidTag.ContainsKey(tag.TagNo + "," + tag.ReaderIP))
                    {
                        _queWaittingProcessTag.Enqueue(tag);
                        _dicAllValidTag.Add(tag.TagNo + "," + tag.ReaderIP, System.DateTime.Now);
                        ShowTag(tag);
                    }
                }
            }
        }
    }
}

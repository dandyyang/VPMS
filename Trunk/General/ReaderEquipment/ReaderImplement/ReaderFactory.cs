using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.ReaderImplement
{
    public class ReaderFactory
    {
        private static AbstractReader _readerInstance = null;
        private static object _statusObject = new object();
        private static Dictionary<string, AbstractReader> _readerDic = new Dictionary<string, AbstractReader>();


        /// <summary>
        /// 信領藍牙讀寫器
        /// </summary>
        public static string ReaderEquipment_THBlueReader = "ReaderEquipment.ReaderImplement.THBlueToothReader,ReaderEquipment";

        /// <summary>
        /// 信領2.4G讀寫器
        /// </summary>
        public static string ReaderEquipment_24GReader = "ReaderEquipment.ReaderImplement.TH24GR,ReaderEquipment";

        /// <summary>
        /// 信领2.4GA型读写器
        /// </summary>
        public static string ReaderEquipment_24GReaderAR = "ReaderEquipment.ReaderImplement.TH24GTypeAR,ReaderEquipment";

        /// <summary>
        /// 信领2.4GA型读写器(Socket)
        /// </summary>
        public static string ReaderEquipment_24GReaderAR_Socket = "ReaderEquipment.ReaderImplement.TH24GTypeAR_Socket,ReaderEquipment";

        /// <summary>
        /// 信领2.4GA型读写器(后台版本)
        /// </summary>
        public static string ReaderEquipment_24GReaderAR_Socket_BG = "ReaderEquipment.ReaderImplement.TH24GTypeAR_Socket_BG,ReaderEquipment";

        /// <summary>
        /// 微耕门禁控制器
        /// </summary>
        public static string ControllerEquipment_WeigengController = "ReaderEquipment.ReaderImplement.WeigengICCardController,ReaderEquipment";

        /// <summary>
        /// 信领UHF_M5E型读写器
        /// </summary>
        public static string ReaderEquipment_UHF_M5EMF4EReader = "ReaderEquipment.ReaderImplement.THUHFTypeM5EMF4EReader,ReaderEquipment";

        /// <summary>
        /// Verayo_HF型读写器
        /// </summary>
        public static string ReaderEquipment_VerayoHFReader = "ReaderEquipment.ReaderImplement.VerayoHFReader,ReaderEquipment";

        /// <summary>
        /// Uline_HF型读写器
        /// </summary>
        public static string ReaderEquipment_ULineHFReader = "ReaderEquipment.ReaderImplement.ULineHFReader,ReaderEquipment";

        private ReaderFactory()
        {

        }

        /// <summary>
        /// 獲得Reader實例
        /// </summary>
        /// <param name="readerName">RegawareM1、RegawareM5</param>
        /// <returns></returns>
        public static AbstractReader GetReaderInstance(string readerName)
        {
            //AbstractReader reader = GetReader(readerName);
            AbstractReader reader = GetReaderInstance<AbstractReader>(readerName);

            return reader;

        }

        /// <summary>
        /// 获得后台模式Reader实例
        /// </summary>
        /// <param name="readerName"></param>
        /// <returns></returns>
        public static AbstractReaderBackground GetReaderBGInstance(string readerName)
        {
            //AbstractReader reader = GetReader(readerName);
            AbstractReaderBackground reader = GetReaderInstance<AbstractReaderBackground>(readerName);

            return reader;

        }

        /// <summary>
        /// 獲得Reader名稱為RegawareM1的實例
        /// </summary>
        /// <returns></returns>
        public static AbstractReader GetReaderInstance()
        {
            //AbstractReader reader = GetReader("TH24G");
            AbstractReader reader = GetReaderInstance<AbstractReader>(ReaderEquipment_24GReader);
            return reader;
        }

        //private static AbstractReader GetReader(string readerName)
        //{
        //    AbstractReader readerInstance = null;

        //    lock (_readerDic)
        //    {
        //        if (!_readerDic.ContainsKey(readerName))
        //        {
        //            switch (readerName)
        //            {
        //                case "TH24G":
        //                    readerInstance = new TH24GR();
        //                    break;
        //                case "THBlueReader":
        //                    readerInstance = new THBlueToothReader();
        //                    break;
        //                case "Weigeng":
        //                    readerInstance = new WeigengICCardController();
        //                    break;
        //                default:
        //                    break;
        //            }

        //            _readerDic.Add(readerName, readerInstance);
        //        }
        //        else
        //        {
        //            readerInstance = _readerDic[readerName];
        //        }
        //    }

        //    return readerInstance;
        //}

        /// <summary>
        /// 获得对应的读写器设备
        /// </summary>
        /// <typeparam name="IReader">Reader 抽象</typeparam>
        /// <param name="ReaderFullName">Reader 全称</param>
        /// <returns></returns>
        static IReader GetReaderInstance<IReader>(string ReaderFullName)
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(ReaderFullName, false);
                return (IReader)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        ///// <summary>
        ///// 获得Controller实例
        ///// </summary>
        ///// <param name="controllerName"></param>
        ///// <returns></returns>
        //public static AbstractICCardController GetControllerInstance(string controllerName)
        //{
        //    AbstractICCardController controller = GetController(controllerName);
        //    return controller;
        //}

        //private static AbstractICCardController GetController(string controllerName)
        //{
        //    AbstractICCardController controllerInstance = null;

        //    lock (_controllerDic)
        //    {
        //        if (!_controllerDic.ContainsKey(controllerName))
        //        {
        //            switch (controllerName)
        //            {
        //                case "Weigeng":
        //                    {
        //                        controllerInstance = new WeigengICCardController();
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            _controllerDic.Add(controllerName, controllerInstance);
        //        }
        //        else
        //        {
        //            controllerInstance = _controllerDic[controllerName];
        //        }
        //    }
        //    return controllerInstance;
        //}
    }
}

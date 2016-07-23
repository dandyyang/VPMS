using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.VegetableProduction.Master;
using Model.VegetableProduction.Master;
using LinqToSQLModel;
using Common;

namespace DAL.SqlDAL.VegetableProduction.Master
{
    class VegetableSpeciesMasterDA : IVegetableSpeciesMasterDA
    {
        #region IDataBaseCommandDA<VegetableSpeciesMaster_vsm> 成员

        public VegetableSpeciesMaster_vsm_Info GetRecord_First()
        {
            VegetableSpeciesMaster_vsm_Info vegetableSpeciesMasterInfo = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    IEnumerable<VegetableSpeciesMaster_vsm> query = db.VegetableSpeciesMaster_vsm.OrderBy(t => t.vsm_iSeq);

                    if (query != null && query.Count() > 0)
                    {
                        VegetableSpeciesMaster_vsm vegetableSpeciesMaster = query.First();

                        if (vegetableSpeciesMaster != null)
                        {
                            vegetableSpeciesMasterInfo = Common.General.CopyObjectValue<VegetableSpeciesMaster_vsm, VegetableSpeciesMaster_vsm_Info>(vegetableSpeciesMaster);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return vegetableSpeciesMasterInfo;
        }

        public VegetableSpeciesMaster_vsm_Info GetRecord_Last()
        {
            VegetableSpeciesMaster_vsm_Info vegetableSpeciesMasterInfo = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    IEnumerable<VegetableSpeciesMaster_vsm> query = db.VegetableSpeciesMaster_vsm.OrderByDescending(t => t.vsm_iSeq);

                    if (query != null && query.Count() > 0)
                    {
                        VegetableSpeciesMaster_vsm vegetableSpeciesMaster = query.First();

                        if (vegetableSpeciesMaster != null)
                        {
                            vegetableSpeciesMasterInfo = Common.General.CopyObjectValue<VegetableSpeciesMaster_vsm, VegetableSpeciesMaster_vsm_Info>(vegetableSpeciesMaster);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return vegetableSpeciesMasterInfo;
        }

        public VegetableSpeciesMaster_vsm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            VegetableSpeciesMaster_vsm_Info vegetableSpeciesMasterInfo = null;

            if (commandInfo != null && commandInfo.KeyInfoList != null && commandInfo.KeyInfoList[0] != null && commandInfo.KeyInfoList[0].KeyValue != "")
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        VegetableSpeciesMaster_vsm vegetableSpecies = (from t in db.VegetableSpeciesMaster_vsm
                                                             where t.vsm_iSeq < Convert.ToInt32(commandInfo.KeyInfoList[0].KeyValue)
                                                             orderby t.vsm_iSeq descending
                                                             select t).FirstOrDefault();

                        if (vegetableSpecies != null)
                        {
                            vegetableSpeciesMasterInfo = Common.General.CopyObjectValue<VegetableSpeciesMaster_vsm, VegetableSpeciesMaster_vsm_Info>(vegetableSpecies);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }


            return vegetableSpeciesMasterInfo;
        }

        public VegetableSpeciesMaster_vsm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            VegetableSpeciesMaster_vsm_Info vegetableSpeciesMasterInfo = null;

            if (commandInfo != null && commandInfo.KeyInfoList != null && commandInfo.KeyInfoList[0] != null && commandInfo.KeyInfoList[0].KeyValue != "")
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        VegetableSpeciesMaster_vsm vegetableSpecies = (from t in db.VegetableSpeciesMaster_vsm
                                                                       where t.vsm_iSeq > Convert.ToInt32(commandInfo.KeyInfoList[0].KeyValue)
                                                                       orderby t.vsm_iSeq ascending
                                                                       select t).FirstOrDefault();

                        if (vegetableSpecies != null)
                        {
                            vegetableSpeciesMasterInfo = Common.General.CopyObjectValue<VegetableSpeciesMaster_vsm, VegetableSpeciesMaster_vsm_Info>(vegetableSpecies);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }


            return vegetableSpeciesMasterInfo;
        }

        #endregion

        #region IMainDA<VegetableSpeciesMaster_vsm> 成员

        #region Insert

        /// <summary>
        /// 新增蔬菜品种主档记录
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns></returns>
        public bool InsertRecord(VegetableSpeciesMaster_vsm_Info infoObject)
        {
            bool isSuccess = false;

            if (infoObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        
                        VegetableSpeciesMaster_vsm vegetableSpeciesMaster = Common.General.CopyObjectValue<VegetableSpeciesMaster_vsm_Info, VegetableSpeciesMaster_vsm>(infoObject);

                        db.VegetableSpeciesMaster_vsm.InsertOnSubmit(vegetableSpeciesMaster);

                        this.InsertVegetableSpeciesTransactionTime(db, infoObject.VegetableSpeciesTransactionTimes);

                        this.InsertVegetableSpeciesMasterCost(db, infoObject.VegetableSpeciesMasterCosts);

                        this.InsertVegetableSpeciesPerdictYield(db, infoObject.VegetableSpeciesPerdictYields);

                        this.InsertVegetableSpeciesSuitPlantTime(db, infoObject.VegetableSpeciesSuitPlantTimes);

                        db.SubmitChanges();

                        isSuccess = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }

            return isSuccess;
        }
        /// <summary>
        /// 新增种植事务时间记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transactionTimes"></param>
        private void InsertVegetableSpeciesTransactionTime(VPMSDBDataContext db, List<VegetableSpeciesTransactionTime_vstt_Info> transactionTimes)
        {
            if (transactionTimes != null && transactionTimes.Count > 0)
            {
                for (int i = 0; i < transactionTimes.Count; i++)
                {
                    if (transactionTimes[i] != null)
                    {
                        VegetableSpeciesTransactionTime_vstt vegetableSpeciesMaster = Common.General.CopyObjectValue<VegetableSpeciesTransactionTime_vstt_Info, VegetableSpeciesTransactionTime_vstt>(transactionTimes[i]);

                        if (vegetableSpeciesMaster != null)
                        {
                            db.VegetableSpeciesTransactionTime_vstt.InsertOnSubmit(vegetableSpeciesMaster);
                        }
                        this.InsertVegetableSpeciesCycleTransaction(db, transactionTimes[i].VegetableSpeciesCycleTransactions);
                    }
                }
            }
        }
        /// <summary>
        /// 新增种植周期事务记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cycleTransactions"></param>
        private void InsertVegetableSpeciesCycleTransaction(VPMSDBDataContext db, List<VegetableSpeciesCycleTransaction_vsct_Info> cycleTransactions)
        {
            if (cycleTransactions != null && cycleTransactions.Count > 0)
            {
                for (int i = 0; i < cycleTransactions.Count; i++)
                {
                    if (cycleTransactions[i] != null)
                    {
                        VegetableSpeciesCycleTransaction_vsct cycleTransaction = Common.General.CopyObjectValue<VegetableSpeciesCycleTransaction_vsct_Info, VegetableSpeciesCycleTransaction_vsct>(cycleTransactions[i]);

                        if (cycleTransaction != null)
                        {
                            db.VegetableSpeciesCycleTransaction_vsct.InsertOnSubmit(cycleTransaction);
                        }

                        this.InsertVSCTransactionMaterial(db, cycleTransactions[i].VSCTransactionMaterials);
                    }
                }
            }
        }
        /// <summary>
        /// 种植周期事务物料记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cycleTransactions"></param>
        private void InsertVSCTransactionMaterial(VPMSDBDataContext db, List<VSCTransactionMaterial_vctm_Info> transactionMaterials)
        {
            if (transactionMaterials != null && transactionMaterials.Count > 0)
            {
                for (int i = 0; i < transactionMaterials.Count; i++)
                {
                    if (transactionMaterials[i] != null)
                    {
                        VSCTransactionMaterial_vctm transactionMaterial = Common.General.CopyObjectValue<VSCTransactionMaterial_vctm_Info, VSCTransactionMaterial_vctm>(transactionMaterials[i]);

                        if (transactionMaterial != null)
                        {
                            db.VSCTransactionMaterial_vctm.InsertOnSubmit(transactionMaterial);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 种植成本
        /// </summary>
        /// <param name="db"></param>
        /// <param name="costs"></param>
        private void InsertVegetableSpeciesMasterCost(VPMSDBDataContext db, List<VegetableSpeciesMasterCost_vsmc_Info> costs)
        {
            if (costs != null && costs.Count > 0)
            {
                for (int i = 0; i < costs.Count; i++)
                {
                    if (costs[i] != null)
                    {
                        VegetableSpeciesMasterCost_vsmc cost = Common.General.CopyObjectValue<VegetableSpeciesMasterCost_vsmc_Info, VegetableSpeciesMasterCost_vsmc>(costs[i]);

                        if (cost != null)
                        {
                            db.VegetableSpeciesMasterCost_vsmc.InsertOnSubmit(cost);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 蔬菜品种预计产量
        /// </summary>
        /// <param name="db"></param>
        /// <param name="perdictYields"></param>
        private void InsertVegetableSpeciesPerdictYield(VPMSDBDataContext db, List<VegetableSpeciesPerdictYield_vspy_Info> perdictYields)
        {
            if (perdictYields != null && perdictYields.Count > 0)
            {
                for (int i = 0; i < perdictYields.Count; i++)
                {
                    if (perdictYields[i] != null)
                    {
                        VegetableSpeciesPerdictYield_vspy perdictYield = Common.General.CopyObjectValue<VegetableSpeciesPerdictYield_vspy_Info, VegetableSpeciesPerdictYield_vspy>(perdictYields[i]);

                        if (perdictYield != null)
                        {
                            db.VegetableSpeciesPerdictYield_vspy.InsertOnSubmit(perdictYield);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 适种月份
        /// </summary>
        /// <param name="db"></param>
        /// <param name="suitPlanTimes"></param>
        private void InsertVegetableSpeciesSuitPlantTime(VPMSDBDataContext db, List<VegetableSpeciesSuitPlantTime_vspt_Info> suitPlanTimes)
        {
            if (suitPlanTimes != null && suitPlanTimes.Count > 0)
            {
                for (int i = 0; i < suitPlanTimes.Count; i++)
                {
                    if (suitPlanTimes[i] != null)
                    {
                        VegetableSpeciesSuitPlantTime_vspt suitPlanTime = Common.General.CopyObjectValue<VegetableSpeciesSuitPlantTime_vspt_Info, VegetableSpeciesSuitPlantTime_vspt>(suitPlanTimes[i]);

                        if (suitPlanTime != null)
                        {
                            db.VegetableSpeciesSuitPlantTime_vspt.InsertOnSubmit(suitPlanTime);
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 更新蔬菜品种主档记录
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns></returns>
        public bool UpdateRecord(VegetableSpeciesMaster_vsm_Info infoObject)
        {
            bool isSuccess = false;

            if (infoObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        VegetableSpeciesMaster_vsm vegetableSpeciesMaster = db.VegetableSpeciesMaster_vsm.FirstOrDefault(t => t.vsm_RecordID == infoObject.vsm_RecordID);

                        if (vegetableSpeciesMaster != null)
                        {
                            vegetableSpeciesMaster.vsm_cName = infoObject.vsm_cName;
                            vegetableSpeciesMaster.vsm_cTypeNum = infoObject.vsm_cTypeNum;
                            vegetableSpeciesMaster.vsm_cByClassificationNum = infoObject.vsm_cByClassificationNum;
                            vegetableSpeciesMaster.vsm_iNurseryStage = infoObject.vsm_iNurseryStage;
                            vegetableSpeciesMaster.vsm_iGrowingPeriod = infoObject.vsm_iGrowingPeriod;
                            vegetableSpeciesMaster.vsm_iPickingPeriod = infoObject.vsm_iPickingPeriod;
                            vegetableSpeciesMaster.vsm_iAcreYield = infoObject.vsm_iAcreYield;
                            vegetableSpeciesMaster.vsm_iPlastochron = infoObject.vsm_iPlastochron;
                            vegetableSpeciesMaster.vsm_cRemark = infoObject.vsm_cRemark;
                            vegetableSpeciesMaster.vsm_dLastDate = DateTime.Now;
                            vegetableSpeciesMaster.vsm_cLast = infoObject.vsm_cLast;

                            this.UpdateVegetableSpeciesTransactionTime(db, infoObject.VegetableSpeciesTransactionTimes);
                            
                            db.SubmitChanges();

                            isSuccess = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// 更新种植事务时间记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transactionTimes"></param>
        private void UpdateVegetableSpeciesTransactionTime(VPMSDBDataContext db, List<VegetableSpeciesTransactionTime_vstt_Info> transactionTimes)
        {
            if (transactionTimes != null && transactionTimes.Count > 0)
            {
                for (int i = 0; i < transactionTimes.Count; i++)
                {
                    if (transactionTimes[i] != null)
                    {
                        if (transactionTimes[i].RecordEditStatus == Common.DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            VegetableSpeciesTransactionTime_vstt vegetableSpeciesMaster = Common.General.CopyObjectValue<VegetableSpeciesTransactionTime_vstt_Info, VegetableSpeciesTransactionTime_vstt>(transactionTimes[i]);

                            if (vegetableSpeciesMaster != null)
                            {
                                db.VegetableSpeciesTransactionTime_vstt.InsertOnSubmit(vegetableSpeciesMaster);
                            }

                            this.InsertVegetableSpeciesCycleTransaction(db, transactionTimes[i].VegetableSpeciesCycleTransactions);
                        }

                        if (transactionTimes[i].RecordEditStatus == Common.DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            VegetableSpeciesTransactionTime_vstt transactionTime = db.VegetableSpeciesTransactionTime_vstt.FirstOrDefault(t => t.vstt_RecordID == transactionTimes[i].vstt_RecordID);

                            if (transactionTime != null)
                            {
                                transactionTime.vstt_iCycleDatetime = transactionTimes[i].vstt_iCycleDatetime;
                                transactionTime.vstt_cRemark = transactionTimes[i].vstt_cRemark;
                            }
                        }

                        if (transactionTimes[i].RecordEditStatus == Common.DefineConstantValue.EditStateEnum.OE_Delete)
                        {
                            VegetableSpeciesTransactionTime_vstt transactionTime = db.VegetableSpeciesTransactionTime_vstt.FirstOrDefault(t => t.vstt_RecordID == transactionTimes[i].vstt_RecordID);

                            if (transactionTime != null)
                            {
                                transactionTime.vstt_iCycleDatetime = transactionTimes[i].vstt_iCycleDatetime;
                                transactionTime.vstt_cRemark = transactionTimes[i].vstt_cRemark;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 更新种植周期事务记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cycleTransactions"></param>
        private void UpdateVegetableSpeciesCycleTransaction(VPMSDBDataContext db, List<VegetableSpeciesCycleTransaction_vsct_Info> cycleTransactions)
        {
            if (cycleTransactions != null && cycleTransactions.Count > 0)
            {
                for (int i = 0; i < cycleTransactions.Count; i++)
                {
                    if (cycleTransactions[i] != null)
                    {
                        VegetableSpeciesCycleTransaction_vsct cycleTransaction = Common.General.CopyObjectValue<VegetableSpeciesCycleTransaction_vsct_Info, VegetableSpeciesCycleTransaction_vsct>(cycleTransactions[i]);

                        if (cycleTransaction != null)
                        {
                            db.VegetableSpeciesCycleTransaction_vsct.InsertOnSubmit(cycleTransaction);
                        }

                        this.InsertVSCTransactionMaterial(db, cycleTransactions[i].VSCTransactionMaterials);
                    }
                }
            }
        }
        /// <summary>
        /// 种植周期事务物料记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cycleTransactions"></param>
        private void UpdateVSCTransactionMaterial(VPMSDBDataContext db, List<VSCTransactionMaterial_vctm_Info> transactionMaterials)
        {
            if (transactionMaterials != null && transactionMaterials.Count > 0)
            {
                for (int i = 0; i < transactionMaterials.Count; i++)
                {
                    if (transactionMaterials[i] != null)
                    {
                        VSCTransactionMaterial_vctm transactionMaterial = Common.General.CopyObjectValue<VSCTransactionMaterial_vctm_Info, VSCTransactionMaterial_vctm>(transactionMaterials[i]);

                        if (transactionMaterial != null)
                        {
                            db.VSCTransactionMaterial_vctm.InsertOnSubmit(transactionMaterial);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 种植成本
        /// </summary>
        /// <param name="db"></param>
        /// <param name="costs"></param>
        private void UpdateVegetableSpeciesMasterCost(VPMSDBDataContext db, List<VegetableSpeciesMasterCost_vsmc_Info> costs)
        {
            if (costs != null && costs.Count > 0)
            {
                for (int i = 0; i < costs.Count; i++)
                {
                    if (costs[i] != null)
                    {
                        VegetableSpeciesMasterCost_vsmc cost = Common.General.CopyObjectValue<VegetableSpeciesMasterCost_vsmc_Info, VegetableSpeciesMasterCost_vsmc>(costs[i]);

                        if (cost != null)
                        {
                            db.VegetableSpeciesMasterCost_vsmc.InsertOnSubmit(cost);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 蔬菜品种预计产量
        /// </summary>
        /// <param name="db"></param>
        /// <param name="perdictYields"></param>
        private void UpdateVegetableSpeciesPerdictYield(VPMSDBDataContext db, List<VegetableSpeciesPerdictYield_vspy_Info> perdictYields)
        {
            if (perdictYields != null && perdictYields.Count > 0)
            {
                for (int i = 0; i < perdictYields.Count; i++)
                {
                    if (perdictYields[i] != null)
                    {
                        VegetableSpeciesPerdictYield_vspy perdictYield = Common.General.CopyObjectValue<VegetableSpeciesPerdictYield_vspy_Info, VegetableSpeciesPerdictYield_vspy>(perdictYields[i]);

                        if (perdictYield != null)
                        {
                            db.VegetableSpeciesPerdictYield_vspy.InsertOnSubmit(perdictYield);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 适种月份
        /// </summary>
        /// <param name="db"></param>
        /// <param name="suitPlanTimes"></param>
        private void UpdateVegetableSpeciesSuitPlantTime(VPMSDBDataContext db, List<VegetableSpeciesSuitPlantTime_vspt_Info> suitPlanTimes)
        {
            if (suitPlanTimes != null && suitPlanTimes.Count > 0)
            {
                for (int i = 0; i < suitPlanTimes.Count; i++)
                {
                    if (suitPlanTimes[i] != null)
                    {
                        VegetableSpeciesSuitPlantTime_vspt suitPlanTime = Common.General.CopyObjectValue<VegetableSpeciesSuitPlantTime_vspt_Info, VegetableSpeciesSuitPlantTime_vspt>(suitPlanTimes[i]);

                        if (suitPlanTime != null)
                        {
                            db.VegetableSpeciesSuitPlantTime_vspt.InsertOnSubmit(suitPlanTime);
                        }
                    }
                }
            }
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            bool isSuccess = false;

            VegetableSpeciesMaster_vsm_Info keyObject = KeyObject as VegetableSpeciesMaster_vsm_Info;

            if (keyObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        VegetableSpeciesMaster_vsm vegetableSpeciesMaster = db.VegetableSpeciesMaster_vsm.FirstOrDefault(t => t.vsm_RecordID == keyObject.vsm_RecordID);

                        if (vegetableSpeciesMaster != null)
                        {
                            db.VegetableSpeciesMaster_vsm.DeleteOnSubmit(vegetableSpeciesMaster);

                            db.SubmitChanges();

                            isSuccess = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return isSuccess;
        }

        public VegetableSpeciesMaster_vsm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<VegetableSpeciesMaster_vsm_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExtraDA 成员

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

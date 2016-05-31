using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.Management.Master;
using Model.Management.Master;
using DAL.IDAL.Management.Master;
using DAL.Factory.Management;
using BLL.IBLL.Management.DeleteData;
using BLL.Factory.Management;
using Model.Management.DeleteData;
using Common;
using System.ServiceModel;
//using Common.FileMgtService;
using ServiceManage.WebService;
using ServiceManage.FilesManage;
using ServiceManage.FileMgtService;

namespace BLL.DAL.Management.Master
{
    class CardUserMasterBL : ICardUserMasterBL
    {
        ICardUserMasterDA _cardUserMasterDA;
        IDeleteDataLogicBL _deleteDataLogicBL;
        AbstracFilesManage _fileManage;

        public CardUserMasterBL()
        {
            this._cardUserMasterDA = MasterDAFactory.GetDAL<ICardUserMasterDA>(MasterDAFactory.CardUserMaster);
            this._deleteDataLogicBL = MasterBLLFactory.GetBLL<IDeleteDataLogicBL>(MasterBLLFactory.DeleteDataLogic_ddl);
            try
            {
                this._fileManage = FilesManageFactory.GetFilesManage();
            }
            catch (Exception)
            { }
        }

        #region IDataBaseCommandBL<CardUserMaster_cus_Info> Members

        public CardUserMaster_cus_Info GetRecord_First()
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                info = this._cardUserMasterDA.GetRecord_First();
                if (info != null)
                {
                    GetPhoto(info);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public CardUserMaster_cus_Info GetRecord_Last()
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                info = this._cardUserMasterDA.GetRecord_Last();
                if (info != null)
                {
                    GetPhoto(info);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public CardUserMaster_cus_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                info = this._cardUserMasterDA.GetRecord_Previous(commandInfo);
                if (info != null)
                {
                    GetPhoto(info);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public CardUserMaster_cus_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                info = this._cardUserMasterDA.GetRecord_Next(commandInfo);
                if (info != null)
                {
                    GetPhoto(info);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainBL Members

        private bool InsertRecord(Model.Management.Master.CardUserMaster_cus_Info infoObject)
        {
            bool tabB = _cardUserMasterDA.IsExistRecord((infoObject as CardUserMaster_cus_Info).cus_cNumber);
            if (!tabB)
            {
                return _cardUserMasterDA.InsertRecord(infoObject);
            }
            return false;
        }

        private bool UpdateRecord(Model.Management.Master.CardUserMaster_cus_Info infoObject)
        {
            return _cardUserMasterDA.UpdateRecord(infoObject);
        }

        public Model.IModel.IModelObject DisplayRecord(Model.IModel.IModelObject itemEntity)
        {
            try
            {
                Model.IModel.IModelObject info = null;
                info = _cardUserMasterDA.DisplayRecord(itemEntity);
                if (info != null)
                {
                    GetPhoto(info as CardUserMaster_cus_Info);
                }
                return info;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            List<Model.Management.Master.CardUserMaster_cus_Info> list = _cardUserMasterDA.SearchRecords(itemEntity);
            List<Model.IModel.IModelObject> objectList = new List<Model.IModel.IModelObject>();
            foreach (CardUserMaster_cus_Info item in list)
            {
                objectList.Add(item);
            }
            return objectList;
        }

        public Model.General.ReturnValueInfo Save(Model.IModel.IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            msg.boolValue = false;
            bool tab = false;
            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
            info = itemEntity as CardUserMaster_cus_Info;
            try
            {
                switch (EditMode)
                {
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:
                        #region OE_Insert
                        if (info.cus_cChaName.Trim() != "" && info.cus_cNumber.Trim() != "")
                        {
                            if (info.cus_cSexNum.Trim() != "")
                            {
                                if (info.cus_cIdentityNum.Trim() != "")
                                {
                                    if (info.cus_cSchoolNum.Trim() != "")
                                    {
                                        if (info.cus_cIdentityNum.Trim() == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
                                        {

                                            #region 学生
                                            info.cus_cDepartmentNum = "";
                                            if (info.cus_cSpecialtyNum.Trim() != "")
                                            {
                                                if (info.cus_cClassNum.Trim() != "")
                                                {
                                                    if (info.PhotoPath.Trim() != "")
                                                    {

                                                        #region 保存图片
                                                        SavePhoto(msg, info, DefineConstantValue.EditStateEnum.OE_Insert);
                                                        #endregion

                                                    }
                                                    else
                                                    {
                                                        info.cus_guidPhotoKey = Guid.NewGuid();
                                                    }
                                                    try
                                                    {
                                                        tab = InsertRecord(info);

                                                    }
                                                    catch (Exception Ex)
                                                    {
                                                        throw Ex;
                                                    }
                                                    if (tab == true)
                                                    {
                                                        msg.boolValue = true;
                                                        //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddSuccess;
                                                        msg.messageText = "";



                                                    }
                                                    else
                                                    {
                                                        msg.boolValue = false;
                                                        msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "用户编号重复!";
                                                    }
                                                }
                                                else
                                                {
                                                    msg.boolValue = false;
                                                    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "班级不能为空!";
                                                }
                                            }
                                            else
                                            {
                                                msg.boolValue = false;
                                                msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "专业不能为空!";
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 教师
                                            info.cus_cClassNum = "";
                                            info.cus_cSpecialtyNum = "";
                                            info.cus_cGraduationPeriod = "";
                                            if (info.cus_cDepartmentNum != "")
                                            {
                                                if (info.PhotoPath.Trim() != "")
                                                {
                                                    SavePhoto(msg, info, DefineConstantValue.EditStateEnum.OE_Insert);
                                                }
                                                else
                                                {
                                                    info.cus_guidPhotoKey = Guid.NewGuid();
                                                }
                                                try
                                                {

                                                    tab = InsertRecord(info);
                                                }
                                                catch (Exception Ex)
                                                {
                                                    throw Ex;
                                                }
                                                if (tab == true)
                                                {
                                                    msg.boolValue = true;
                                                    //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddSuccess;
                                                    msg.messageText = "";
                                                }
                                                else
                                                {
                                                    msg.boolValue = false;
                                                    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "用户编号重复!";
                                                }
                                            }
                                            else
                                            {
                                                msg.boolValue = false;
                                                msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "科室不能为空!";
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        msg.boolValue = false;
                                        msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "院系不能为空!";
                                    }
                                }
                                else
                                {
                                    msg.boolValue = false;
                                    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "身份不能为空!";
                                }
                            }
                            else
                            {
                                msg.boolValue = false;
                                msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "性别不能为空!";
                            }
                        }
                        else
                        {
                            msg.boolValue = false;
                            msg.messageText = "用户编号或中文名称不能为空!";
                        }
                        #endregion
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        #region OE_Update
                        if (info.cus_cChaName.Trim() != "")
                        {
                            if (info.cus_cIdentityNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
                            {
                                #region 老师
                                info.cus_cClassNum = "";
                                info.cus_cSpecialtyNum = "";
                                info.cus_cGraduationPeriod = "";
                                if (info.cus_cDepartmentNum != "")
                                {
                                    //try
                                    //{
                                    //    var _info = _cardUserMasterDA.DisplayRecord(info);
                                    //    if (_info != null)
                                    //    {
                                    //        FileMgtSoapClient soap = new FileMgtSoapClient();

                                    //        ReturnValueInfo returnInfo = soap.UpdateBytes(_info.cus_guidPhotoKey, DefineConstantValue.Management, DefineConstantValue.CardUserPicture, info.PhotoPath, info.cus_imgPhoto.ToArray());

                                    //        if (!returnInfo.boolValue)
                                    //        {
                                    //            msg.boolValue = true;
                                    //            msg.messageText = "相片保存失败:" + returnInfo.messageText;
                                    //        }
                                    //    }

                                    //}
                                    //catch
                                    //{
                                    //    msg.boolValue = true;
                                    //    msg.messageText = "相片保存失败";
                                    //}
                                    if (info.PhotoPath != string.Empty)
                                    {
                                        SavePhoto(msg, info, DefineConstantValue.EditStateEnum.OE_Update);
                                    }
                                    tab = UpdateRecord(info);
                                    if (tab == true)
                                    {
                                        msg.boolValue = true;
                                        //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
                                        msg.messageText = "";
                                    }
                                    else
                                    {
                                        msg.boolValue = false;
                                        msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail;
                                    }
                                }
                                else
                                {
                                    msg.boolValue = false;
                                    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail + "  " + "科室不能为空!";
                                }
                                #endregion
                            }
                            else
                            {

                                #region 学生
                                info.cus_cDepartmentNum = "";
                                if (info.cus_cSpecialtyNum != "")
                                {
                                    if (info.cus_cClassNum != "")
                                    {
                                        //try
                                        //{
                                        //    if (info.PhotoPath != string.Empty)
                                        //    {
                                        //        var _info = _cardUserMasterDA.DisplayRecord(info);
                                        //        if (_info != null)
                                        //        {
                                        //            FileMgtSoapClient soap = new FileMgtSoapClient();

                                        //            ReturnValueInfo returnInfo = soap.UpdateBytes(_info.cus_guidPhotoKey, DefineConstantValue.Management, DefineConstantValue.CardUserPicture, info.PhotoPath, info.cus_imgPhoto.ToArray());

                                        //            if (!returnInfo.boolValue)
                                        //            {
                                        //                msg.boolValue = true;
                                        //                msg.messageText = "相片保存失败:" + returnInfo.messageText;
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                        //catch
                                        //{
                                        //    msg.boolValue = true;
                                        //    msg.messageText = "相片保存失败";
                                        //}
                                        if (info.PhotoPath != string.Empty)
                                        {
                                            SavePhoto(msg, info, DefineConstantValue.EditStateEnum.OE_Update);
                                        }
                                        tab = UpdateRecord(info);
                                        if (tab == true)
                                        {
                                            msg.boolValue = true;
                                            //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
                                            // msg.messageText = "";
                                        }
                                        else
                                        {
                                            msg.boolValue = false;
                                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail;
                                        }
                                    }
                                    else
                                    {
                                        msg.boolValue = false;
                                        msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail + "  " + "班级不能为空!";
                                    }
                                }
                                else
                                {
                                    msg.boolValue = false;
                                    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail + "  " + "专业不能为空!";
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            msg.boolValue = false;
                            msg.messageText = "名称不能为空!";
                        }

                        #endregion
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Delete:
                        CardUserMaster_cus_Info o_info = DisplayRecord(info) as CardUserMaster_cus_Info;
                        DeleteDataLogic_ddl_Info ddl = new DeleteDataLogic_ddl_Info();
                        ddl.ddl_TableName = Common.DefineConstantValue.MasterType.CardUser;
                        ddl.ddl_ID = (itemEntity as CardUserMaster_cus_Info).cus_iRecordID;
                        if (o_info != null)
                        {
                            ddl.ddl_Key1 = o_info.cus_guidPhotoKey.ToString();
                        }
                        msg = _deleteDataLogicBL.DeleteData(ddl);

                        //if (true)
                        //{
                        //    tab = _cardUserMasterDA.DeleteRecord(itemEntity);
                        //    if (tab == true)
                        //    {
                        //        msg.boolValue = true;
                        //        msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        //    }
                        //    else
                        //    {
                        //        msg.boolValue = false;
                        //        //msg.messageText = "刪除操作"+Common.DefineConstantValue.SystemMessageText.strSystemError;
                        //        msg.messageText = "";
                        //    } 
                        //}
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return msg;
        }

        private void SavePhoto(Model.General.ReturnValueInfo msg, CardUserMaster_cus_Info info, DefineConstantValue.EditStateEnum editState)
        {
            try
            {

                //FileMgtSoapClient soap = Common.WebSrvFactory.GetFileMgt();
                if (editState == DefineConstantValue.EditStateEnum.OE_Insert)
                {
                    ReturnValueInfo returnInfo;
                    //if (info.cus_imgPhoto.ToArray().Length > 0)
                    //{
                    returnInfo = _fileManage.SaveBytes(DefineConstantValue.SchoolInternetOfThings, DefineConstantValue.CardUserPicture, info.PhotoPath, info.cus_imgPhoto.ToArray());
                    //}
                    //else
                    //{
                    //    returnInfo = _fileManage.Save(DefineConstantValue.Management, Common.DefineConstantValue.CardUserPicture, info.PhotoPath);
                    //}

                    if (!returnInfo.boolValue)
                    {
                        msg.boolValue = true;
                        msg.messageText = "相片保存失败:" + returnInfo.messageText;
                        info.cus_guidPhotoKey = Guid.NewGuid();
                    }
                    else
                    {
                        info.cus_guidPhotoKey = (Guid)returnInfo.ValueObject;
                    }
                }
                else if (editState == DefineConstantValue.EditStateEnum.OE_Update)
                {
                    var _info = _cardUserMasterDA.DisplayRecord(info);
                    if (_info != null)
                    {
                        ReturnValueInfo returnInfo;
                        //if (info.cus_imgPhoto.ToArray().Length > 0)
                        //{
                        returnInfo = _fileManage.UpdateBytes(_info.cus_guidPhotoKey, DefineConstantValue.SchoolInternetOfThings, DefineConstantValue.CardUserPicture, info.PhotoPath, info.cus_imgPhoto.ToArray());
                        //}
                        //else
                        //{
                        //    returnInfo = _fileManage.Save(DefineConstantValue.Management, Common.DefineConstantValue.CardUserPicture, info.PhotoPath);
                        //}

                        if (!returnInfo.boolValue)
                        {
                            msg.boolValue = true;
                            msg.messageText = "相片保存失败:" + returnInfo.messageText;
                        }
                        //info.cus_guidPhotoKey = (Guid)returnInfo.ValueObject;
                    }
                }
            }
            catch
            {
                msg.boolValue = true;
                msg.messageText = "相片保存失败";
            }
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            return _cardUserMasterDA.DeleteRecord(KeyObject);
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            return _cardUserMasterDA.IsExistRecord(KeyObject);
        }

        public Model.IModel.IModelObject LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            Model.IModel.IModelObject info = null;
            try
            {
                info = _cardUserMasterDA.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region ICardUserMasterBL Members


        public CardUserMaster_cus_Info CheckCardNum(CardUserMaster_cus_Info info)
        {
            try
            {
                CardUserMaster_cus_Info Result = new CardUserMaster_cus_Info();
                Result = _cardUserMasterDA.CheckCardNum(info);
                return Result;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }



        public List<CardUserMaster_cus_Info> ClassSearch(CardUserMaster_cus_Info info)
        {
            //throw new NotImplementedException();
            try
            {
                List<CardUserMaster_cus_Info> Result = new List<CardUserMaster_cus_Info>();
                Result = _cardUserMasterDA.ClassSearch(info);
                return Result;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        /// <summary>
        /// 更新卡用户的 用户监控事项规则组别
        /// 备注：这个方法没有开启事务。如果跟新某个卡用户失败，将导致方法跳出，已更新数据不会回滚
        /// </summary>
        /// <param name="groupNum"></param>
        /// <param name="cardUserIds"></param>
        /// <returns></returns>
        public Model.General.ReturnValueInfo UpdateMonitorItemGroup(string groupNum, List<int> cardUserIds)
        {
            //throw new NotImplementedException();
            Model.General.ReturnValueInfo rinfo = new Model.General.ReturnValueInfo();
            try
            {
                foreach (int id in cardUserIds)
                {
                    rinfo.messageText += this._cardUserMasterDA.UpdateMonitorItemGroup(groupNum, id).messageText;
                }
                rinfo.boolValue = true;

            }
            catch (Exception ex)
            {
                rinfo.boolValue = false;
                rinfo.messageText = ex.Message;
            }
            return rinfo;
        }

        #endregion

        #region ICardUserMasterBL Members

        int ICardUserMasterBL.GetPersonnelsCount(string Type)
        {
            return _cardUserMasterDA.GetPersonnelsCount(Type);
        }
        #endregion



        #region ICardUserMasterBL 成员

        /// <summary>
        /// 更新亲情号码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Model.General.ReturnValueInfo ICardUserMasterBL.UpdataCardUserPhoneNum(CardUserMaster_cus_Info info)
        {
            try
            {
                return _cardUserMasterDA.UpdataCardUserPhoneNum(info);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        protected void GetPhoto(CardUserMaster_cus_Info info)
        {
            try
            {
                info.byte_cus_imgPhoto = _fileManage.GetFileBytes(info.cus_guidPhotoKey);
                info.PhotoPath = _fileManage.GetFileRelativePath(info.cus_guidPhotoKey);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #region ICardUserMasterBL Members


        public List<CardUserMaster_cus_Info> ExportClassInfo(CardUserMaster_cus_Info info)
        {
            try
            {
                return _cardUserMasterDA.ExportClassInfo(info);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region ICardUserMasterBL Members


        public bool UpdateCardUserList(List<CardUserMaster_cus_Info> list)
        {
            bool isSuccess = false;

            try
            {
                foreach (CardUserMaster_cus_Info item in list)
                {
                    if (!_cardUserMasterDA.UpdateCardUserClass(item))
                    {
                        isSuccess = false;
                        break;
                    }
                    else
                    {
                        isSuccess = true;
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return isSuccess;
        }

        #endregion

        #region ICardUserMasterBL Members


        public bool HandelCardBind(List<CardUserMaster_cus_Info> studentList)
        {
            try
            {
                return _cardUserMasterDA.HandelCardBind(studentList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion
    }
}

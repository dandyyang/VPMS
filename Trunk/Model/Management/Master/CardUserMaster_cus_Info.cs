using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Model.IModel;
using System.Data.Linq;

namespace Model.Management.Master
{
    /// <summary>
    /// 卡用户主档
    ///  </summary>
    [Serializable]
    public class CardUserMaster_cus_Info : IRecordEditStatusInfo, Model.IModel.IModelObject
    {
        public CardUserMaster_cus_Info()
        {
            this.cus_iRecordID = 0;
            this.cus_cNumber = string.Empty;
            this.cus_cChaName = string.Empty;
            this.cus_cEngName = string.Empty;
            this.cus_cSexNum = string.Empty;
            this.cus_cSchoolNum = string.Empty;
            this.cus_cDepartmentNum = string.Empty;
            this.cus_cSpecialtyNum = string.Empty;
            this.cus_cGraduationPeriod = string.Empty;
            this.cus_cClassNum = string.Empty;
            this.cus_cSMSReceivePhone = string.Empty;
            this.cus_cIdentityNum = string.Empty;
            this.cus_lIsSendSMS = false;
            this.cus_lIsSendEmail = false;
            this.cus_lValid = false;
            this.cus_cRemark = string.Empty;
            this.cus_cAdd = string.Empty;
            this.cus_cLast = string.Empty;
            this.cus_cMailAddress = string.Empty;
            this.byte_cus_imgPhoto = new byte[] { };
            this.cus_imgPhoto = new Binary(new byte[] { });
            this.cus_guidPhotoKey = Guid.Empty;
            this.RecordEditStatus = Common.DefineConstantValue.EditStateEnum.OE_ReaOnly;

            this.cus_cDormitorySiteNum = string.Empty;
            this.cus_cGroupNum = string.Empty;

            cus_CardID = string.Empty;
            cus_cSex = string.Empty;
            cus_cIdentity = string.Empty;
            cus_cSchool = string.Empty;
            cus_cDepartment = string.Empty;
            cus_cSpecialty = string.Empty;
            cus_cClass = string.Empty;
            cus_AbsenceTypeName = string.Empty;
            cus_QueryDate = string.Empty;
            cus_AbsenceRecordID = string.Empty;
            cus_cPosition = string.Empty;
            cus_cImgPatch = string.Empty;

            longID = 0;
            cus_cAppendPhone1 = string.Empty;
            cus_cAppendPhone2 = string.Empty;
            cus_cAppendPhone3 = string.Empty;

            cellphone1 = string.Empty;
            cellphone2 = string.Empty;
            cellphone3 = string.Empty;
            cellphone4 = string.Empty;

            cus_cGotoSchoolType = string.Empty;
            cus_lCashPay = false;
            cus_cStudentId = string.Empty;

            PhotoPath = string.Empty;
        }


        /// <summary>
        /// 查询下请假记录ID
        /// </summary>
        public long longID { get; set; }

        /// <summary>
        /// 查询下请假记录ID
        /// </summary>
        public string cus_AbsenceRecordID { get; set; }

        /// <summary>
        /// 查询下存放记录日期
        /// </summary>
        public string cus_QueryDate { get; set; }

        /// <summary>
        /// 查询下显视缺勤类型
        /// </summary>
        public string cus_AbsenceTypeName { get; set; }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int cus_iRecordID
        {
            set;
            get;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string cus_cNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string cus_cChaName
        {
            set;
            get;
        }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string cus_cEngName
        {
            set;
            get;
        }

        /// <summary>
        /// 性別编号
        /// </summary>
        public string cus_cSexNum
        {
            set;
            get;
        }

        /// <summary>
        /// 性別
        /// </summary>
        public string cus_cSex
        {
            set;
            get;
        }

        /// <summary>
        /// 身份编号
        /// </summary>
        public string cus_cIdentityNum
        {
            set;
            get;
        }

        /// <summary>
        /// 身份
        /// </summary>
        public string cus_cIdentity
        {
            set;
            get;
        }

        /// <summary>
        /// 院系部编号
        /// </summary>
        public string cus_cSchoolNum
        {
            set;
            get;
        }

        /// <summary>
        /// 院系部
        /// </summary>
        public string cus_cSchool
        {
            set;
            get;
        }

        /// <summary>
        /// 所属科室编号
        /// </summary>
        public string cus_cDepartmentNum
        {
            set;
            get;
        }

        /// <summary>
        /// 所属科室
        /// </summary>
        public string cus_cDepartment
        {
            set;
            get;
        }

        /// <summary>
        /// 专业编号
        /// </summary>
        public string cus_cSpecialtyNum
        {
            set;
            get;
        }

        /// <summary>
        /// 专业
        /// </summary>
        public string cus_cSpecialty
        {
            set;
            get;
        }

        /// <summary>
        /// 届别
        /// </summary>
        public string cus_cGraduationPeriod
        {
            set;
            get;
        }

        /// <summary>
        /// 班级编号
        /// </summary>
        public string cus_cClassNum
        {
            set;
            get;
        }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string cus_cClass
        {
            set;
            get;
        }

        /// <summary>
        /// 短信接收电话
        /// </summary>
        public string cus_cSMSReceivePhone
        {
            set;
            get;
        }

        /// <summary>
        /// 是否短信通知
        /// </summary>
        public bool cus_lIsSendSMS
        {
            set;
            get;
        }

        /// <summary>
        /// 是否邮件通知
        /// </summary>
        public bool cus_lIsSendEmail
        {
            set;
            get;
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool cus_lValid
        {
            set;
            get;
        }

        /// <summary>
        /// 电邮地址
        /// </summary>
        public string cus_cMailAddress
        {
            set;
            get;
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string cus_CardID
        {
            set;
            get;
        }

        /// <summary>
        /// 照片Binary
        /// </summary>
        public Binary cus_imgPhoto
        {
            set;
            get;
        }


        /// <summary>
        /// 照片文件Path,传参用
        /// </summary>
        public string PhotoPath
        {
            set;
            get;
        }

        /// <summary>
        /// 照片 文件key
        /// </summary>
        public Guid cus_guidPhotoKey
        {
            set;
            get;
        }

        /// <summary>
        /// 宿舍地点编号
        /// </summary>
        public string cus_cDormitorySiteNum
        {
            set;
            get;
        }

        /// <summary>
        /// 宿舍地点名称 格式：建筑物名称--地点名称
        /// </summary>
        public string cus_cDormitorySiteName
        {
            set;
            get;
        }

        /// <summary>
        /// 宿舍床位
        /// </summary>
        public string cus_cBedNum
        {
            set;
            get;
        }

        /// <summary>
        /// 用户活动监控事项规则设置组别编号
        /// </summary>
        public string cus_cGroupNum
        {
            set;
            get;
        }

        /// <summary>
        /// 用户活动监控事项规则设置组别名称
        /// </summary>
        public string cus_cGroupName { set; get; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string cus_cImgPatch { get; set; }

        /// <summary>
        /// 照片byte
        /// </summary>
        public byte[] byte_cus_imgPhoto
        {
            set
            {
                cus_imgPhoto = new Binary(value != null ? value : new byte[0]);
            }
            get
            {
                if (cus_imgPhoto != null && cus_imgPhoto.Length > 0)
                {
                    return cus_imgPhoto.ToArray();
                }
                else
                {
                    return new byte[0];
                }
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string cus_cRemark
        {
            set;
            get;
        }

        /// <summary>
        /// 新增者
        /// </summary>
        public string cus_cAdd
        {
            set;
            get;
        }

        /// <summary>
        /// 新增时间
        /// </summary>
        public System.DateTime? cus_dAddDate
        {
            set;
            get;
        }

        /// <summary>
        /// 修改者
        /// </summary>
        public string cus_cLast
        {
            set;
            get;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? cus_dLastDate
        {
            set;
            get;
        }


        /// <summary>
        /// 编号长度
        /// </summary>
        public int cus_cNumber_Length
        {
            set;
            get;
        }

        /// <summary>
        /// 中文名称长度
        /// </summary>
        public int cus_cChaName_Length
        {
            set;
            get;
        }

        /// <summary>
        /// 英文名称长度
        /// </summary>
        public int cus_cEngName_Length
        {
            set;
            get;
        }

        /// <summary>
        /// 届别长度
        /// </summary>
        public int cus_cGraduationPeriod_Length
        {
            set;
            get;
        }

        /// <summary>
        /// 短信接收电话长度
        /// </summary>
        public int cus_cSMSReceivePhone_Length
        {
            set;
            get;
        }

        public int cus_cMailAddress_Length
        {
            set;
            get;
        }

        public int RecordID
        {
            set;
            get;
        }

        public string cus_cPosition
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展接收电话一
        /// </summary>
        public string cus_cAppendPhone1 { get; set; }

        /// <summary>
        /// 扩展接收电话二
        /// </summary>
        public string cus_cAppendPhone2 { get; set; }

        /// <summary>
        /// 扩展接收电话三
        /// </summary>
        public string cus_cAppendPhone3 { get; set; }

        /// <summary>
        /// 亲情号码1
        /// </summary>
        public string cellphone1 { get; set; }

        /// <summary>
        /// 亲情号码2
        /// </summary>
        public string cellphone2 { get; set; }

        /// <summary>
        /// 亲情号码3
        /// </summary>
        public string cellphone3 { get; set; }

        /// <summary>
        /// 亲情号码4
        /// </summary>
        public string cellphone4 { get; set; }


        public string cus_cGotoSchoolType
        {
            get;
            set;
        }
        /// <summary>
        /// 允许消费
        /// </summary>
        public bool cus_lCashPay
        {
            get;
            set;
        }

        public string cus_cStudentId
        {
            get;
            set;
        }

        #region IRecordEditStatusInfo Members

        public Common.DefineConstantValue.EditStateEnum RecordEditStatus
        {
            set;
            get;
        }

        #endregion


        /// <summary>
        /// 亲情号码(子类)
        /// </summary>
        public CardUserPhoneNumMaster_cup_Info CardUserPhoneNum
        {
            get;
            set;
        }

        #region IModelObject Members

        int IModelObject.RecordID
        {
            get;
            set;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;

namespace PaymentEquipment.DeviceImplement
{
    public class EastRiverWriter : AbstractICWriter
    {

        public override bool Conn(int iCom, int iBaud, byte bSection, string strPwd)
        {
            throw new NotImplementedException();
        }

        public override string GetMacID()
        {
            throw new NotImplementedException();
        }

        public override int GetCardNo()
        {
            throw new NotImplementedException();
        }

        public override bool WriteCard(PaymentEquipment.Entity.PaymentCard p_objCard)
        {
            throw new NotImplementedException();
        }

        public override PaymentEquipment.Entity.PaymentCard ReadCard()
        {
            throw new NotImplementedException();
        }

        public override bool SetCardPassword(int p_intSector, string p_strPassword)
        {
            throw new NotImplementedException();
        }

        public override bool SetWriterPassword(string p_strPassword)
        {
            throw new NotImplementedException();
        }

        public override bool SetWriterPassword(int port, string p_strPassword)
        {
            throw new NotImplementedException();
        }

        public override bool RechargeMoney(decimal dMoney)
        {
            throw new NotImplementedException();
        }

        public override bool RechargeSubsidize(decimal dMoney)
        {
            throw new NotImplementedException();
        }

        public override decimal GetPosVal()
        {
            throw new NotImplementedException();
        }

        public override decimal GetSubzVal()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetPhoneNumList()
        {
            throw new NotImplementedException();
        }

        public override PaymentEquipment.Entity.PaymentCard GetUserBasicInfos()
        {
            throw new NotImplementedException();
        }

        public override bool WriteInitPosVal(decimal dMoney)
        {
            throw new NotImplementedException();
        }

        public override bool WriteInitSubzVal(decimal dMoney)
        {
            throw new NotImplementedException();
        }

        public override bool WriteInitContactPhone(string strContactInfo, List<string> listPhoneNums)
        {
            throw new NotImplementedException();
        }

        public override bool WriteInitUserBasicInfo(PaymentEquipment.Entity.PaymentCard payCard)
        {
            throw new NotImplementedException();
        }

        public override bool SetWatermeterAdminCard(int port, string paymentPassword, int eachTimeMaxinumNumber, int dailyMaxinumNumber, decimal unitPrice)
        {
            throw new NotImplementedException();
        }

        public override bool InitMacSectionPwd(string strPwd)
        {
            throw new NotImplementedException();
        }

        public override bool InitConsumptionSectionPwd(string strPwd)
        {
            throw new NotImplementedException();
        }

        public override bool InitWaterSectionPwd(string strPwd)
        {
            throw new NotImplementedException();
        }

        public override bool InitPhoneSectionPwd(string strPwd)
        {
            throw new NotImplementedException();
        }
    }
}

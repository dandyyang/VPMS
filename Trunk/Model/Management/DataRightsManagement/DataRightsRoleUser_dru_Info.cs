using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.DataRightsManagement
{
    public class DataRightsRoleUser_dru_Info : IModelObject
    {
      public DataRightsRoleUser_dru_Info() 
      {
          this.dru_iRecordID = 0;
          this.dru_cRoleNumber = string.Empty;
          this.dur_cUserLoginID = string.Empty;
      }

      public int dru_iRecordID { get; set; }

      public string dru_cRoleNumber { get; set; }

      public string dur_cUserLoginID { get; set; }

      #region IModelObject Members

      public int RecordID
      {
          get
          {
              throw new NotImplementedException();
          }
          set
          {
              throw new NotImplementedException();
          }
      }

      #endregion
    }
}

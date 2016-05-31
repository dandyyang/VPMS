using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Model.Management.DeleteData
{
   public class DeleteDataLogic_ddl_Info
    {
       public DeleteDataLogic_ddl_Info() 
       {
           this.ddl_ID = 0;
           this.ddl_Key1 = string.Empty;
           this.ddl_Key2 = string.Empty;
       }

       /// <summary>
       /// 操作对象表名
       /// </summary>
       public DefineConstantValue.MasterType ddl_TableName { get; set; }

       /// <summary>
       /// 表ID
       /// </summary>
       public int ddl_ID { get; set; }

       /// <summary>
       /// 表Key1
       /// </summary>
      public string ddl_Key1 { get; set; }

       /// <summary>
       /// 表Key2
       /// </summary>
      public string ddl_Key2 { get; set; }



    }
}

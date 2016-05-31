using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Model.Base;

namespace BLL.IBLL
{
    public interface IDataBaseCommandBL<T>
    {
        T GetRecord_First();
        T GetRecord_Last();
        T GetRecord_Previous(DataBaseCommandInfo commandInfo);
        T GetRecord_Next(DataBaseCommandInfo commandInfo);
    }
}

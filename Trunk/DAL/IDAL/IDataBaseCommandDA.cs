using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Model.Base;

namespace DAL.IDAL
{
    public interface IDataBaseCommandDA<T>
    {
        T GetRecord_First();
        T GetRecord_Last();
        T GetRecord_Previous(DataBaseCommandInfo commandInfo);
        T GetRecord_Next(DataBaseCommandInfo commandInfo);
    }
}

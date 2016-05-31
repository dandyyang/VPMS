using System;
using System.Collections.Generic;
using System.Text;

namespace UKey
{
    public class UKeyFactory
    {
        static string _ukeyFullName = "UKey.UKeyImplement.NT119API,UKey";

        public UKeyFactory()
        {

        }

        public static AbstractUKey GetUKey()
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(_ukeyFullName, false);
                return (AbstractUKey)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

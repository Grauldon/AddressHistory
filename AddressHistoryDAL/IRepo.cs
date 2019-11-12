using System;
using System.Collections.Generic;

namespace AddressHistoryDAL
{
    interface IRepo<T>
    {
        T GetAddress(DateTime date);
        List<T> GetAddresses();
        List<T> GetAddresses(DateTime date);
        int Add(T addr);
        int Update(T addr);
        int Delete(T addr);
    }
}

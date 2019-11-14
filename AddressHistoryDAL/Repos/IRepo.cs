using System;
using System.Collections.Generic;

namespace AddressHistoryDAL.Repos
{
    public interface IRepo<T>
    {
        T GetAddress(DateTime date);
        List<T> GetAddresses();
        List<T> GetAddresses(DateTime date);
        T GetLastAddress();
        int Add(T addr);
        int Update(T addr);
        int Delete(T addr);
    }
}

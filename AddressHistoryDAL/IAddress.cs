using System.Collections.Generic;

using AddressHistoryDAL.Models;

namespace AddressHistory.Interfaces
{
    interface IAddress
    {
        List<Address> GetAddresses();
    }
}

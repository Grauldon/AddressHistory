using AddressHistoryDAL.Enums;
using AddressHistoryDAL.Models;

namespace AddressHistoryDAL.Repos
{
    public interface IDataRepo : IRepo<Address>
    {
        int InsertAddress(Address addr);
        int DeleteAddress();
        int UpdateAddress(Address addr, FieldType field, string value);
    }
}

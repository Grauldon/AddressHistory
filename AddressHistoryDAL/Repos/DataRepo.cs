using System;
using System.Collections.Generic;
using System.Linq;

using AddressHistoryDAL.Enums;
using AddressHistoryDAL.Models;

namespace AddressHistoryDAL.Repos
{
    public class DataRepo : BaseRepo<Address>, IDataRepo
    {
        public int DeleteAddress()
        {
            int response = 0;
            Address addr = new Address();
            List<Address> addresses = new List<Address>();

            addr = GetLastAddress();
            response =  Delete(addr);

            addresses = GetAddresses();
            addr = addresses.OrderByDescending(a => a.StartDate).First();
            addr.EndDate = DateTime.Parse("9999-12-31");
            response = Update(addr);

            return response;
        }

        public int InsertAddress()
        {
            int response = 0;

            response = TerminateAddress();

            if (response == 0)
            {
                Address addr = new Address()
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Parse("9999-12-31"),
                    Address1 = "123 Somewhere Ave",
                    City = "Nowhere",
                    State = "AS",
                    Zip5 = "12345"
                };

                response = Add(addr);
            }

            return response;
        }

        public int UpdateAddress(Address addr, FieldType field, string value)
        {
            int response = 0;

            switch (field)
            {
                case FieldType.STARTDATE:
                    response = -100;
                    break;
                case FieldType.ENDDATE:
                    // Add validation
                    addr.EndDate = DateTime.Parse(value);
                    break;
                case FieldType.ADDRESS1:
                    addr.Address1 = value;
                    break;
                case FieldType.ADDRESS2:
                    addr.Address2 = value;
                    break;
                case FieldType.CITY:
                    addr.City = value;
                    break;
                case FieldType.STATE:
                    addr.State = value;
                    break;
                case FieldType.ZIP5:
                    addr.Zip5 = value;
                    break;
                case FieldType.ZIP4:
                    addr.Zip4 = value;
                    break;
                default:
                    throw new Exception("Invalid field type.");
            }

            response = Update(addr);

            return response; 
        }

        private int TerminateAddress()
        {
            int response = 0;

            Address addr = new Address();
            addr = GetLastAddress();
            addr.EndDate = DateTime.Now.AddDays(-1);

            response = Update(addr);

            return response;
        }
    }
}

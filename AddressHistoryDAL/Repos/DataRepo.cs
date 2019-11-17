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

        public int InsertAddress(Address addr)
        {
            int response = 0;
            DateTime today = DateTime.Today;

            Address terminateAddress = new Address();
            terminateAddress = GetLastAddress();

            if (terminateAddress.StartDate == today)
            {
                // add error catching
                return -100;
            }
            else
            {
                terminateAddress.EndDate = DateTime.Now.AddDays(-1);

                response = Update(terminateAddress);

                if (response == 1)
                {
                    addr.StartDate = today;
                    addr.EndDate = DateTime.Parse("9999-12-31");
                    response = Add(addr);
                }
                // Add an else clause to catch when no records are updated or when more than one records are updated. The application should only update one record at a time.
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
                    if (value.Length == 2)
                    {
                        addr.State = value;
                    }
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

            if (response == 0)
            {
                response = Update(addr);
            }

            return response; 
        }
    }
}

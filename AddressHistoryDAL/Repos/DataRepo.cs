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

            try
            {
                addr = GetLastAddress();
                response = Delete(addr);

                if (response == 1)
                {
                    addresses = GetAddresses();
                    addr = addresses.OrderByDescending(a => a.StartDate).First();
                    addr.EndDate = DateTime.Parse("9999-12-31");

                    response = Update(addr);

                    if (response != 1)
                    {
                        throw new Exception("Update operation affected other than one row.");
                    }
                }
                else
                {
                    throw new Exception("Delete operation affected other than one row.");
                }
            }
            catch (Exception ex)
            {
                InputOutput logger = InputOutput.GetInstance();

                ex.Data.Add("Class:", "DataRepo");
                ex.Data.Add("Method:", "DeleteAddress()");
                ex.Data.Add("StartDate:", addr.StartDate);
                ex.Data.Add("EndDate:", addr.EndDate);
                ex.Data.Add("Address:", addr.Address1);

                logger.WriteLogFile(ex);
            }

            return response;
        }

        public int InsertAddress(Address addr)
        {
            int response = 0;
            DateTime today = DateTime.Today;

            Address terminateAddress = new Address();
            terminateAddress = GetLastAddress();

            try
            {
                if (terminateAddress.StartDate == today)
                {
                    throw new Exception("The new address will have the same start date as the terminating address, which is not allowed.");
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

                        if (response != 1)
                        {
                            throw new Exception("Insert operation affected other than one row.");
                        }
                    }
                    else
                    {
                        addr = terminateAddress;
                        throw new Exception("Update operation affected other than one row.");
                    }
                }
            }
            catch (Exception ex)
            {
                InputOutput logger = InputOutput.GetInstance();

                ex.Data.Add("Class:", "DataRepo");
                ex.Data.Add("Method:", "InsertAddress(Address addr)");
                ex.Data.Add("StartDate:", addr.StartDate);
                ex.Data.Add("EndDate:", addr.EndDate);
                ex.Data.Add("Address:", addr.Address1);

                logger.WriteLogFile(ex);
            }

            return response;
        }

        public int UpdateAddress(Address addr, FieldType field, string value)
        {
            int response = 0;

            try
            {
                switch (field)
                {
                    case FieldType.STARTDATE:
                        throw new Exception("Start date cannot be updated.");
                    case FieldType.ENDDATE:
                        throw new Exception("End date cannot be updated.");
                    case FieldType.ADDRESS1:
                        if (value.Length > 50)
                        {
                            throw new Exception("Invalid string length for Address1.");
                        }
                        else
                        {
                            addr.Address1 = value;
                        }
                        break;
                    case FieldType.ADDRESS2:
                        if (value?.Length > 20)
                        {
                            throw new Exception("Invalid string length for Address2.");
                        }
                        else
                        {
                            addr.Address2 = value;
                        }
                        break;
                    case FieldType.CITY:
                        if (value.Length > 20)
                        {
                            throw new Exception("Invalid string length for City.");
                        }
                        else
                        {
                            addr.City = value;
                        }
                        break;
                    case FieldType.STATE:
                        if (value.Length == 2)
                        {
                            addr.State = value;
                        }
                        else
                        {
                            throw new Exception("Invalid string length for State.");
                        }
                        break;
                    case FieldType.ZIP5:
                        if (value.Length != 5)
                        {
                            throw new Exception("Invalid string length for Zip5.");
                        }
                        else
                        {
                            addr.Zip5= value;
                        }
                        break;
                    case FieldType.ZIP4:
                        if (value != null && value.Length != 4)
                        {
                            throw new Exception("Invalid string length for Zip4.");
                        }
                        else
                        {
                            addr.Zip4 = value;
                        }
                        break;
                    default:
                        throw new Exception("Invalid field type.");
                }

                response = Update(addr);

                if (response != 1)
                {
                    throw new Exception("Update operation affected other than one row.");
                }

            }
            catch (Exception ex)
            {
                InputOutput logger = InputOutput.GetInstance();

                ex.Data.Add("Class:", "DataRepo");
                ex.Data.Add("Method:", "UpdateAddress(Address addr, FieldType field, string value)");
                ex.Data.Add("StartDate:", addr.StartDate);
                ex.Data.Add("EndDate:", addr.EndDate);
                ex.Data.Add("Address:", addr.Address1);
                ex.Data.Add("value:", value);

                logger.WriteLogFile(ex);
            }

            return response; 
        }
    }
}

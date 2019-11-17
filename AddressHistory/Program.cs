using System;
using System.Collections.Generic;

using AddressHistoryDAL.Enums;
using AddressHistoryDAL.Models;
using AddressHistoryDAL.Repos;

namespace AddressHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            //RetrieveList();
            //RetrieveListByDate();
            //RetrieveOneByDate();
            InsertAddress();
            //DeleteAddress();
            //UpdateAddress();
        }

        static void RetrieveList()
        {
            BaseRepo<Address> data = new BaseRepo<Address>();
            List<Address> addresses = new List<Address>();

            addresses = data.GetAddresses();

            foreach (var item in addresses)
            {
                Console.WriteLine($"Between {item.StartDate.ToString("MM/dd/yyyy")} and {item.EndDate.ToString("MM/dd/yyyy")}, you lived at");
                DisplayAddress(item);
                Console.WriteLine();
            }
        }

        static void RetrieveListByDate()
        {
            BaseRepo<Address> data = new BaseRepo<Address>();
            List<Address> addresses = new List<Address>();
            DateTime date = new DateTime(2007, 07, 01);

            addresses = data.GetAddresses(date);

            foreach (var item in addresses)
            {
                Console.WriteLine($"Between {item.StartDate.ToString("MM/dd/yyyy")} and {item.EndDate.ToString("MM/dd/yyyy")}, you lived at");
                DisplayAddress(item);
                Console.WriteLine();
            }
        }

        static void RetrieveOneByDate()
        {
            BaseRepo<Address> data = new BaseRepo<Address>();
            Address addr = new Address();
            DateTime date = new DateTime(2018, 12, 01);

            addr = data.GetAddress(date);
            Console.WriteLine($"On {date.ToString("MM/dd/yyyy")}, you lived at");
            DisplayAddress(addr);
        }

        static void DisplayAddress(Address address)
        {
            Console.WriteLine($"{address.Address1} {address.Address2}");
            Console.WriteLine($"{address.City}, {address.State} {address.Zip5}-{address.Zip4}");
        }

        static void InsertAddress()
        {
            DataRepo data = new DataRepo();
            Address addr = new Address()
            {
                Address1 = "123 Somewhere Ave",
                City = "Nowhere",
                State = "AS",
                Zip5 = "12345"
            };

            int response = data.InsertAddress(addr);
            Console.WriteLine($"Return code: {response}");
        }

        static void DeleteAddress()
        {
            DataRepo data = new DataRepo();
            int response = data.DeleteAddress();
            Console.WriteLine($"Response: {response}");
        }

        static void UpdateAddress()
        {
            DataRepo data = new DataRepo();
            Address addr = new Address();
            DateTime date = new DateTime(2005, 12, 01);

            addr = data.GetAddress(date);
            int response = data.UpdateAddress(addr, FieldType.ADDRESS2, null);
            Console.WriteLine($"Response: {response}");
        }
    }
}

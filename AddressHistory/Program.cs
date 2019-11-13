using System;
using System.Collections.Generic;

using AddressHistoryDAL;
using AddressHistoryDAL.Models;

namespace AddressHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepo<Address> data = new DataRepo<Address>();
            List<Address> addresses = new List<Address>();
            Address addr = new Address();
            DateTime date = new DateTime(2007, 07, 01);

            addresses = data.GetAddresses(date);

            foreach (var item in addresses)
            {
                Console.WriteLine($"Between {item.StartDate.ToString("MM/dd/yyyy")} and {item.EndDate.ToString("MM/dd/yyyy")}, you lived at");
                DisplayAddress(item);
                Console.WriteLine();
            }

            //Console.WriteLine();

            //addr = data.GetAddress(date);
            //Console.WriteLine($"On {date.ToString("MM/dd/yyyy")}, you lived at");
            //DisplayAddress(addr);
        }

        static void DisplayAddress(Address address)
        {
            Console.WriteLine($"{address.Address1} {address.Address2}");
            Console.WriteLine($"{address.City}, {address.State} {address.Zip5}-{address.Zip4}");
        }
    }
}

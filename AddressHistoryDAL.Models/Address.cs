using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressHistoryDAL.Models
{
    public class Address
    {
        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        [DataType(DataType.Date)]
        public string EndDate { get; set; }

        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(20)]
        public string Address2 { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(5)]
        public string Zip5 { get; set; }

        [StringLength(4)]
        public string Zip4 { get; set; }

        public List<Address> GetAddresses()
        {
            throw new System.NotImplementedException();
        }
    }
}

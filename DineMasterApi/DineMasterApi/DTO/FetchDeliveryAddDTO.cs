using DineMasterApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.DTO
{
    public class FetchDeliveryAddDTO
    {
        public int AddressId { get; set; }

        public int UserId { get; set; }
        public string FullAddress { get; set; }

       
        public string Pincode { get; set; }

        
        public string City { get; set; }


        public string State { get; set; }

       
        public string Landmark { get; set; }
    }
}

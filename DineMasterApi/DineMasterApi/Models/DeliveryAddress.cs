using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class DeliveryAddress
    {
        [Key]
        public int AddressId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string FullAddress { get; set; }

        [MaxLength(10)]
        public string Pincode { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Landmark { get; set; }
    }
}

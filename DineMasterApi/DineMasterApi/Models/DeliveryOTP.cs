using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class DeliveryOTP
    {
        [Key]
        public int OtpId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [MaxLength(6)]
        public string OTPCode { get; set; }

        public DateTime GeneratedAt { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}

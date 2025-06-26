using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? TableId { get; set; }
        public DiningTable Table { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [MaxLength(20)]
        public string OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        [MaxLength(50)]
        public string OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public Bill Bill { get; set; }

        public ICollection<DeliveryTracking> DeliveryTrackings { get; set; }

        public DeliveryOTP DeliveryOTP { get; set; }
    }
}

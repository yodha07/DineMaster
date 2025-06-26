using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class DeliveryTracking
    {
        [Key]
        public int TrackingId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

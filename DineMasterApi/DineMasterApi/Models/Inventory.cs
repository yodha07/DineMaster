using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.Models
{
    public class Inventory
    {
        [Key]
        public int ItemId { get; set; }

        [MaxLength(100)]
        public string ItemName { get; set; }

        public decimal Quantity { get; set; }

        [MaxLength(20)]
        public string Unit { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class MenuItem
    {
        [Key]
        public int ItemId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public MenuCategory Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

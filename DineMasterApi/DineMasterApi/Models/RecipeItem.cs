using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.Models
{
    public class RecipeItem
    {
        [Key]
        public int RecipeItemId { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryItemId { get; set; }
        public Inventory Inventory { get; set; }

        public decimal QuantityNeeded { get; set; }

        [MaxLength(20)]
        public string Unit { get; set; }
    }
}

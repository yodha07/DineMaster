namespace DineMasterApi.DTO
{
    public class RecipeItemDto
    {
        public int RecipeItemId { get; set; }
        public int MenuItemId { get; set; }
        public int InventoryItemId { get; set; }
        public string InventoryItemName { get; set; }
        public decimal QuantityNeeded { get; set; }
        public string Unit { get; set; }
        public bool IsLowStock { get; set; } // Calculated flag
    }
}

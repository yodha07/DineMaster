namespace DineMasterApi.DTO
{
    public class InventoryDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public decimal Quantity { get; set; }


        public string Unit { get; set; }
        public decimal ReorderLevel { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool isActive { get; set; } = true;
    }
}

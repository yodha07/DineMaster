namespace DineMasterApi.DTO
{
    public class inventoryUpdateDto
    {
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal ReorderReorderLevel { get; set; }
    }
}

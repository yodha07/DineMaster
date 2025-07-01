namespace DineMasterApi.DTO
{
    public class MenuItemDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}

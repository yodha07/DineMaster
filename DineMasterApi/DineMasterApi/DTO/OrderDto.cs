namespace DineMasterApi.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public BillDto Bill { get; set; }
    }
}

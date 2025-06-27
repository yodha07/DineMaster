namespace DineMasterApi.DTO
{
    public class BillDto
    {
        public int BillId { get; set; }
        public int OrderId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}

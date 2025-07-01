namespace DineMasterApi.DTO
{
    public class ReservationDTO3
    {
        public int ReservationId { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public int GuestsCount { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TableId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

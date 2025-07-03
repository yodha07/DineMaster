namespace DineMasterApi.DTO
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}

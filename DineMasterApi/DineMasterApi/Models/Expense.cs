using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; }
    }
}

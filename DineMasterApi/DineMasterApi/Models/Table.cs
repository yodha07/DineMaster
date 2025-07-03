using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Order> Orders { get; set; }
    }
}

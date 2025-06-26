using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.Models
{
    public class DiningTable
    {
        [Key]
        public int TableId { get; set; }

        [MaxLength(50)]
        public string TableName { get; set; }

        public int Capacity { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public DiningTable Table { get; set; }

        public DateTime ReservationTime { get; set; }

        public int GuestsCount { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

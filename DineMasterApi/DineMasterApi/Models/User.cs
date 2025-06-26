using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DineMasterApi.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public bool IsGuest { get; set; } = false;

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; }


    }
}

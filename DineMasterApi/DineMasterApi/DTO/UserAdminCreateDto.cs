using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.DTO
{
    public class UserAdminCreateDto
    {
        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}

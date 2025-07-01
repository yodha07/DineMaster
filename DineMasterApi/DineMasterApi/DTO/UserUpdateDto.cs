using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.DTO
{
    public class UserUpdateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        public int RoleId { get; set; }
    }
}

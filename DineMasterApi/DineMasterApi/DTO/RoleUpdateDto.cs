using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.DTO
{
    public class RoleUpdateDto
    {
        [Required]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }
    }
}

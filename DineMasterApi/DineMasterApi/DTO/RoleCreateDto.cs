using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.DTO
{
    public class RoleCreateDto
    {
        [Required, MaxLength(50)]
        public string RoleName { get; set; }
    }
}

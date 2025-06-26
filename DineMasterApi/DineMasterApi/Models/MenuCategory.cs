using System.ComponentModel.DataAnnotations;

namespace DineMasterApi.Models
{
    public class MenuCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class Product : Entity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

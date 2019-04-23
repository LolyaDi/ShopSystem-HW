using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class User : Entity
    {
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}

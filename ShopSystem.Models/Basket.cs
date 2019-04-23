namespace ShopSystem.Models
{
    public class Basket : Entity
    {
        public User User { get; set; }
        public Product Product { get; set; }
    }
}

namespace ShopSystem.DataAccess
{
    using ShopSystem.Models;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer(new DataInitializer());
        }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        }
    }
}
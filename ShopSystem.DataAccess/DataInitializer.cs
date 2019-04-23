using ShopSystem.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace ShopSystem.DataAccess
{
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        private List<User> _users;
        private List<Basket> _baskets;
        private List<Product> _products;

        public DataInitializer()
        {
            _users = new List<User>
            {
                new User
                {
                    Login = "admin",
                    Password = "123"
                },
                new User
                {
                    Login = "user",
                    Password = "123"
                }
            };

            _products = new List<Product>
            {
                new Product
                {
                    Name = "product1",
                    Price = 1500
                },
                new Product
                {
                    Name = "product2",
                    Price = 2000
                },
                new Product
                {
                    Name = "product3",
                    Price = 1000
                }
            };

            _baskets = new List<Basket>
            {
                new Basket
                {
                    User = _users[0],
                    Product = _products[0]
                },
                new Basket
                {
                    User = _users[1]
                }
            };
        }

        protected override void Seed(DataContext context)
        {
            context.Baskets.AddRange(_baskets);
            context.SaveChanges();
        }
    }
}

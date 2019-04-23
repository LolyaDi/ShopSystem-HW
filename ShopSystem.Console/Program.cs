using ShopSystem.DataAccess;
using ShopSystem.Models;
using System.Collections.Generic;

namespace ShopSystem.Console
{
    public class Program
    {
        private static Repository _repository;
        public static void Main(string[] args)
        {
            var userList = new List<User>();
            var basketList = new List<Basket>();
            var productList = new List<Product>();

            _repository = new Repository();

            var users = _repository.Select<User>();
            var baskets = _repository.Select<Basket>();
            var products = _repository.Select<Product>();

            userList.AddRange(users);
            basketList.AddRange(baskets);
            productList.AddRange(products);

            _repository.Dispose();

            string userPassword, userLogin;

            System.Console.WriteLine("Введите логин:");
            userLogin = System.Console.ReadLine();

            System.Console.WriteLine("Введите пароль:");
            userPassword = System.Console.ReadLine();

            var currentUser = userList.Find(u => u.Login == userLogin && u.Password == userPassword);

            while (currentUser == null)
            {
                System.Console.WriteLine("Пользователь с таким логином и паролем не найден!");
                System.Console.WriteLine("Попробуйте еще раз:");

                System.Console.WriteLine("Введите логин:");
                userLogin = System.Console.ReadLine();

                System.Console.WriteLine("Введите пароль:");
                userPassword = System.Console.ReadLine();

                currentUser = userList.Find(u => u.Login == userLogin && u.Password == userPassword);
            }

            var currentBasketData = basketList.FindAll(b => b.User.Id == currentUser.Id && b.Product != null);

            System.Console.WriteLine("Вывод продуктов в корзине:");

            int i;

            if (currentBasketData.Count == 0)
            {
                System.Console.WriteLine("Корзина пуста!\n");

                string userChoice;

                System.Console.WriteLine("Хотите добавить продуктов в корзину? y/n");
                userChoice = System.Console.ReadLine();

                switch (userChoice)
                {
                    case "y":
                        {
                            System.Console.WriteLine("\nВывод продуктов:");

                            i = 0;

                            System.Console.WriteLine(string.Format("{0} {1,-20} {2,-20}", "№)", "Название", "Цена"));
                            foreach (var item in productList)
                            {
                                System.Console.WriteLine(string.Format("{0}) {1,-20} {2,-20}", ++i, item.Name, item.Price));
                            }

                            Insert(currentUser, productList);
                        }
                        break;
                    case "n":
                        System.Console.WriteLine("Ну лана...");
                        break;
                    default:
                        System.Console.WriteLine("Или y, или n... Неужели так трудно сделать выбор????");
                        break;
                }

                System.Console.ReadLine();
                return;
            }

            i = 0;

            System.Console.WriteLine(string.Format("{0} {1,-20} {2,-20}", "№)", "Название", "Цена"));
            foreach (var item in currentBasketData)
            {
                System.Console.WriteLine(string.Format("{0}) {1,-20} {2,-20}", ++i, item.Product.Name, item.Product.Price));
            }

            System.Console.ReadLine();
        }

        public static void Insert(User user, List<Product> products)
        {
            string userChoice;
            int choice;
            bool isParsed;

            System.Console.WriteLine("Выберите номер продукта:");
            userChoice = System.Console.ReadLine();

            isParsed = int.TryParse(userChoice, out choice);

            while (!isParsed || choice <= 0 || products.Count < choice)
            {
                System.Console.WriteLine("Введите номер продукта из ВЫШЕПЕРЕЧИСЛЕННОГО СПИСКА ПРОДУКТОВ!");
                userChoice = System.Console.ReadLine();

                isParsed = int.TryParse(userChoice, out choice);
            }

            var product = products[choice - 1];

            _repository = new Repository();

            _repository._context.Users.Attach(user);
            _repository._context.Products.Attach(product);

            _repository.Insert(new Basket
            {
                User = user,
                Product = product
            });

            _repository.Dispose();

            System.Console.WriteLine("Продукт успешно добавлен в корзину!");
        }
    }
}

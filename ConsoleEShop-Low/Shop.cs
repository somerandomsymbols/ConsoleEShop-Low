using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEShop_Low
{
    public enum OrderStatus
    {
        New,
        PaymentRecieved,
        Sent,
        UserCancelled,
        AdminCancelled,
        Recieved,
        Done,
    }

    public class Product
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, string category, string description, decimal price)
        {
            if (name == null || name.Equals(""))
                throw new Exception("Name is null or empty");
            if (category == null || category.Equals(""))
                throw new Exception("Category is null or empty");
            if (description == null || description.Equals(""))
                throw new Exception("Description is null or empty");
            if (price < 0)
                throw new Exception("Price cant be < 0");

            Name = name;
            Category = category;
            Description = description;
            Price = price;
        }

        public void ChangeProduct(string category, string description, decimal price)
        {
            if (category == null || category.Equals(""))
                throw new Exception("Category is null or empty");
            if (description == null || description.Equals(""))
                throw new Exception("Description is null or empty");
            if (price < 0)
                throw new Exception("Price cant be < 0");

            Category = category;
            Description = description;
            Price = price;
        }
    }

    public class Order
    {
        public readonly int OrderID;
        public User Reciever { get; private set; }
        public string ProductName { get; private set; }
        public int Amount { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(User user, string productName, int amount, int orderID)
        {
            if (user == null)
                throw new Exception("User is null");
            if (amount <= 0)
                throw new Exception("Amount must be positive");

            Reciever = user;
            ProductName = productName;
            Amount = amount;
            OrderID = orderID;
            Status = OrderStatus.New;
        }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
        }
    }

    public static class Shop
    {
        private static List<Product> products = new List<Product> { new Product("Baseball cap", "Clothing", "Just a cap", 19.99m), new Product("Jeans", "Clothing", "Pants", 49.99m), new Product("Kolobok", "Books", "Book for children", 4.99m) };
        private static List<Order> orders = new List<Order>() { };
        private static List<User> users = new List<User> { new Admin("admin", "1234", "-"), new User("Bob", "111", "Sun st. 21") };

        public static OrderStatus ToStatus(string status)
        {
            if (status == "New")
                return OrderStatus.New;
            if (status == "AdminCancelled")
                return OrderStatus.AdminCancelled;
            if (status == "UserCancelled")
                return OrderStatus.UserCancelled;
            if (status == "PaymentRecieved")
                return OrderStatus.PaymentRecieved;
            if (status == "Sent")
                return OrderStatus.Sent;
            if (status == "Recieved")
                return OrderStatus.Recieved;
            if (status == "Done")
                return OrderStatus.Done;
            throw new Exception($"{status} is not an OrderStatus");
        }

        public static Product FindProduct(string name)
        {
            return products.Find(x => x.Name == name);
        }

        public static Order FindOrder(int ID)
        {
            return orders.Find(x => x.OrderID == ID);
        }

        public static User FindUser(string name)
        {
            return users.Find(x => x.Name == name);
        }

        public static string CreateProduct(string name, string category, string description, decimal price)
        {
            if (FindProduct(name) != null)
                return $"Product with name {name} already exists\n";

            products.Add(new Product(name, category, description, price));
            return $"Created new product {name} in category {category} with description {description} and price {price}\n";
        }

        public static string ChangeProduct(string name, string category, string description, decimal price)
        {
            Product product = FindProduct(name);

            if (product != null)
            {
                product.ChangeProduct(category, description, price);
                return $"Changed product's {name} category to {category}, description to {description} and price to {price}\n";
            }
            else
                return $"No product with name {name}\n";
        }

        public static string ChangeOrderStatus(int orderID, OrderStatus status)
        {
            Order order = FindOrder(orderID);

            if (order != null)
            {
                if (order.Status == OrderStatus.AdminCancelled || order.Status == OrderStatus.UserCancelled || order.Status > status)
                    return $"Change from {order.Status} to {status} is impossible\n";

                order.ChangeStatus(status);
                return $"Changed order {order.OrderID} status to {order.Status}\n";
            }
            else
                return $"No order with ID {orderID}\n";
        }

        public static string CreateOrder(User user, string productName, int amount)
        {
            if (!products.Exists(x => x.Name == productName))
                return $"No product with name {productName}\n";

            if (orders.Count == 0)
                orders.Add(new Order(user, productName, amount, 1));
            else
                orders.Add(new Order(user, productName, amount, orders.Last().OrderID + 1));

            return $"Created order {amount}x {productName} with ID {orders.Last().OrderID}\n";
        }

        public static User RegisterUser(string name, string password, string adress)
        {
            if (FindUser(name) != null)
                return null;

            users.Add(new User(name, password, adress));
            return users.Last();
        }

        public static string ShowProducts()
        {
            if (products.Count == 0)
                return "No products\n";
            else
            {
                string res = "Products:\n";

                foreach (Product product in products)
                {
                    res += $"Name: {product.Name}, Category: {product.Category}, Description: {product.Description}, Price: {product.Price}\n";
                }

                return res;
            }
        }

        public static string ShowOrders()
        {
            if (orders.Count == 0)
                return "No orders\n";
            else
            {
                string res = "Orders:\n";

                foreach (Order order in orders)
                {
                    res += $"ID: {order.OrderID}, User: {order.Reciever.Name}, Product name: {order.ProductName}, Amount: {order.Amount}, Order: {order.Status}\n";
                }

                return res;
            }
        }

        public static string ShowOrdersForUser(string name)
        {
            if (!users.Exists(x => x.Name == name))
                return $"No user with name {name}\n";
            List<Order> userOrders = new List<Order>();
            foreach (Order order in orders)
                if (order.Reciever.Name == name)
                    userOrders.Add(order);

            if (userOrders.Count == 0)
                return $"No orders for user {name}\n";
            else
            {
                string res = $"Orders for user {name}:\n";

                foreach (Order order in userOrders)
                {
                    res += $"ID: {order.OrderID}, User: {order.Reciever.Name}, Product name: {order.ProductName}, Amount: {order.Amount}, Order: {order.Status}\n";
                }

                return res;
            }
        }

        public static string ShowUsers()
        {
            if (users.Count == 0)
                return "No users\n";
            else
            {
                string res = "Users:\n";

                foreach (User user in users)
                {
                    res += $"Name: {user.Name}, Adress: {user.Adress}\n";
                }

                return res;
            }
        }

        public static string ChangeAdress(string name, string adress)
        {
            User user = FindUser(name);

            if (user == null)
                return $"No user with name {name}\n";
            else
            {
                user.EditAdress(adress);
                return $"Changed {name} adress to {adress}\n";
            }
        }
    }
}

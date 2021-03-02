using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEShop_Low
{
    public class User
    {
        public string Name { get; private set; }
        private string Password { get; set; }
        public string Adress { get; private set; }

        public User(string name, string password, string adress)
        {
            Name = name;
            Password = password;
            Adress = adress;
        }

        public virtual string Commands()
        {
            return $"Commands for user {Name}\n" +
                "ShowProducts\n" +
                "FindProduct\n" +
                "CreateOrder\n" +
                "ShowOrders\n" +
                "RecieveOrder\n" +
                "CancelOrder\n" +
                "EditAdress\n" +
                "ChangePassword\n" +
                "LogOut\n";
        }

        public void EditAdress(string adress)
        {
            Adress = adress;
        }

        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        public string ChangePassword(string oldPassword, string newPassword)
        {
            if (!CheckPassword(oldPassword))
                return "Password incorrect\n";
            
            Password = newPassword;
            return "Password successfully changed\n";
        }

        public static string ShowProducts()
        {
            return Shop.ShowProducts();
        }

        public virtual string ShowUsers()
        {
            return "Users can't see user list\n";
        }

        public virtual string ShowOrders()
        {
            return Shop.ShowOrdersForUser(this.Name);
        }

        public static string FindProduct(string name)
        {
            Product product = Shop.FindProduct(name);

            if (product != null)
                return $"Name: {product.Name}, Category: {product.Category}, Description: {product.Description}, Price: {product.Price}\n";
            else
                return $"No product with name {name}\n";
        }

        public string CreateOrder(string name, int amount)
        {
            return Shop.CreateOrder(this, name, amount);
        }

        public virtual string CancelOrder(int orderID)
        {
            if (Shop.FindOrder(orderID).Reciever == this)
                return Shop.ChangeOrderStatus(orderID, OrderStatus.UserCancelled);
            else
                return "Can't cancel others' orders\n";
        }

        public string RecieveOrder(int orderID)
        {
            if (Shop.FindOrder(orderID).Reciever == this)
                return Shop.ChangeOrderStatus(orderID, OrderStatus.Recieved);
            else
                return "Can't recieve others' orders\n";
        }

        public virtual string ChangeOrder(int orderID, OrderStatus status)
        {
            return "Users can't mark orders\n";
        }

        public virtual string ChangeUser(string name, string adress)
        {
            return "Users can't change others personal information\n";
        }

        public string ChangeSelf(string adress)
        {
            return Shop.ChangeAdress(this.Name, adress);
        }

        public virtual string CreateProduct(string name, string category, string description, decimal price)
        {
            return "Users can't create new products\n";
        }

        public virtual string ChangeProduct(string name, string category, string description, decimal price)
        {
            return "Users can't change products\n";
        }
    }
}

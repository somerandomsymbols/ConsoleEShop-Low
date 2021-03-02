using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEShop_Low
{
    public class Admin : User
    {
        public Admin(string name, string password, string adress) : base(name, password, adress)
        {

        }

        public override string Commands()
        {
            return $"Commands for admin {Name}\n" +
                "ShowProducts\n" +
                "FindProduct\n" +
                "CreateOrder\n" +
                "ShowOrders\n" +
                "RecieveOrder\n" +
                "CancelOrder\n" +
                "MarkOrder" +
                "EditAdress\n" +
                "EditUser" +
                "ChangePassword\n" +
                "LogOut\n";
        }

        public override string ShowUsers()
        {
            return Shop.ShowUsers();
        }

        public override string ChangeUser(string name, string adress)
        {
            return Shop.ChangeAdress(name, adress);
        }

        public override string CancelOrder(int orderID)
        {
            return Shop.ChangeOrderStatus(orderID, OrderStatus.AdminCancelled);
        }

        public override string ChangeOrder(int orderID, OrderStatus status)
        {
            return Shop.ChangeOrderStatus(orderID, status);
        }

        public override string ShowOrders()
        {
            return Shop.ShowOrders();
        }

        public override string CreateProduct(string name, string category, string description, decimal price)
        {
            return Shop.CreateProduct(name, category, description, price);
        }

        public override string ChangeProduct(string name, string category, string description, decimal price)
        {
            return Shop.ChangeProduct(name, category, description, price);
        }
    }
}

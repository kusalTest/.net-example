using System.Collections.Generic;
using AngularBasic.Data.Entities;

namespace AngularBasic.Data
{
    public interface IAngularBasicRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);

        Order GetOrderById(string username, int id);
        void AddEntity(object model);
        void AddOrder(Order newOrder);
    }
}
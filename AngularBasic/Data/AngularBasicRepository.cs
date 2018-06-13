using AngularBasic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularBasic.Data
{
    public class AngularBasicRepository : IAngularBasicRepository
    {
        private readonly AngularBasicContext _ctx;
        //private readonly ILogger<AngularBasicRepository> _logger; 

        public AngularBasicRepository(AngularBasicContext ctx)
        {
            _ctx = ctx;
            //_logger = logger; 
        }

        public AngularBasicRepository()
        {
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders.Include(o => o.Items)
                              .ThenInclude(i => i.Product)
                              .ToList();
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders.Include(o => o.Items)
                              .ThenInclude(i => i.Product)
                              .Where(o => o.Id == id && o.User.UserName == username)
                              .FirstOrDefault();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            //_logger.LogInformation("GetAllProducts was called");    
         
            return _ctx.Products
                       .OrderBy(p => p.Title)
                       .ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            
            return _ctx.Products
                       .Where(p => p.Category == category)
                       .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
        }

        public void AddOrder(Order newOrder)
        {
            foreach (var item in newOrder.Items)
            {
                item.Product = _ctx.Products.Find(item.Product.Id);
            }
            AddEntity(newOrder);
        }
    }
}

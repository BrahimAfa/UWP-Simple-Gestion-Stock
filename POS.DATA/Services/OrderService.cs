using POS.Data.Helpers;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace POS.Data.Services
{
  public class OrderService
  {
    POSContext POSContext;

    public OrderService()
    {
      POSContext = Singleton<POSContext>.Instance;
      POSContext.Database.EnsureCreated();

    }

    public async Task<bool> SaveOrderAsync(Order Order)
    {
      Order.Product = null;
      Order.Provider = null;
      Order.OrderId = 0;
      var p = await POSContext.Orders.AddAsync(Order);
      int result = await POSContext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
      var data = await POSContext.Orders.FindAsync(id);
      if (data == null) return false;
      POSContext.Orders.Remove(data);
      var result = await POSContext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<bool> ValidateOrderAsync(int id)
    {
      var data = await POSContext.Orders.FindAsync(id);
      if (data == null) return false;
      if (data.Status.ToLower().Equals("completed")) return false;
      data.Status = "Completed";
      var product = await POSContext.Products.FindAsync(data.Product.ProductId);
      product.Quantity += data.Quantity;
      var result = await POSContext.SaveChangesAsync();
      return result > 0;
    }


    public async Task<bool> UpdateOrderAsync(Order Order)
    {
      var data = await POSContext.Orders.FindAsync(Order.OrderId);
      if (data == null) return false;
      data = Order;
      var result = await POSContext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync() => await POSContext.Orders.ToListAsync();
  }
}

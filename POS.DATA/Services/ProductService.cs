using POS.Data.Helpers;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace POS.Data.Services
{
  public class ProductService
  {
    POSContext POScontext;
    public ProductService()
    {
      POScontext = Singleton<POSContext>.Instance;
      POScontext.Database.EnsureCreated();

    }
    public async Task<IEnumerable<Product>> GetFakeProductsAsync()
    {
       await Task.CompletedTask;
      List<Product> ps = new List<Product>
      {
        new Product()
        {
          Name="Product #1",
          BuyingPrice=100,
          SellingPrice=150.50d,
          Description="Simple Fake Description",
          Code = "P-111111",
          Quantity=90,
          WarningQuantity=10,
          ProductId=1,
          Category = new Category(){CategoryId=1, Name="Category #1"},
          Provider = new Provider(){ProviderId=1, Name="Provider #1"}
        },
         new Product()
        {
         ProviderId=2,
          Name="Product #2",
          BuyingPrice=100,
          SellingPrice=150.50d,
          Description="Simple Fake Description",
          Code = "P-222222",
          Quantity=90,
          WarningQuantity=10,
          ProductId=2,
          Category = new Category(){CategoryId=2, Name="Category #2"},
          Provider = new Provider(){ProviderId=2, Name="Provider #2"}
        }
      };
      return ps;
   }
    public async Task<bool> SaveProductAsync(Product product)
    {
      product.Category = null;
      product.Provider = null;
      product.ProductId = 0;
      var p = await POScontext.Products.AddAsync(product);
      int result = await POScontext.SaveChangesAsync();
      return result > 0;
     }

    public async Task<bool> DeleteProductAsync(int id)
    {
      var data = await POScontext.Products.FindAsync(id);
      if (data == null) return false;
      POScontext.Products.Remove(data);
      var result = await  POScontext.SaveChangesAsync();
      return result>0;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
      var data = await POScontext.Products.FindAsync(product.ProductId);
      if (data == null) return false;
      data = product;
      var result = await POScontext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
      // await Task.CompletedTask; // placeHolder to simplify async functions
      return await POScontext.Products.ToListAsync();
    }
  }
}

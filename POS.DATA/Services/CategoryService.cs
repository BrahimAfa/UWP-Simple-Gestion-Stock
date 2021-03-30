using POS.Data.Helpers;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace POS.Data.Services
{
  public class CategoryService
  {
    POSContext POSContext;
    public CategoryService()
    {
      POSContext = Singleton<POSContext>.Instance;
      POSContext.Database.EnsureCreated();
      // POSContext.Database.Migrate();

    }
    public async Task<IEnumerable<Category>> GetFakeCategoriesAsync()
    {
       await Task.CompletedTask;
      List<Category> ps = new List<Category>
      {
          new Category(){CategoryId=1, Name="Category #1"},
          new Category(){CategoryId=2,Name="Category #2"},
          new Category(){CategoryId=3,Name="Category #3"},
          new Category(){CategoryId=4,Name="Category #4"},

      };
      return ps;
    }
    public async Task<bool> SaveCategoryAsync(Category product)
    {
      await POSContext.Categories.AddAsync(product);
      int result = await POSContext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      // await Task.CompletedTask; // placeHolder to simplify async functions
      return await POSContext.Categories.ToListAsync();
    }
  }
}

using POS.Data.Helpers;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace POS.Data.Services
{
  public class ProviderService
  {
    POSContext POSContext;
    public ProviderService()
    {
      POSContext = Singleton<POSContext>.Instance;
      POSContext.Database.EnsureCreated();

    }
    public async Task<IEnumerable<Provider>> GetFakeProvidersAsync()
    {
       await Task.CompletedTask;
      List<Provider> ps = new List<Provider>
      {
        new Provider(){Name="Provider #1" }
      };
      return ps;
    }

    public async Task<bool> SaveProviderAsync(Provider product)
    {
      await POSContext.Providers.AddAsync(product);
      int result = await POSContext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<IEnumerable<Provider>> GetProvidersAsync()
    {
      return await POSContext.Providers.ToListAsync();
    }
  }
}

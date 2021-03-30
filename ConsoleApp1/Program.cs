using POS.Data.Models;
using POS.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  class Page { }
  class Program
  {
    static void Main(string[] args)
    {
      var l = new ProviderService().SaveProviderAsync(new Provider() { Name = "provider#2", Description = "hello#3" }).Result;
      if (l)
      {
        Console.WriteLine( "inserted");
        Console.WriteLine("db count : "+ new ProviderService().GetProvidersAsync().Result.Count());
      }
      Console.WriteLine(typeof(Page).FullName);
      Console.ReadLine();
    }
  }
}

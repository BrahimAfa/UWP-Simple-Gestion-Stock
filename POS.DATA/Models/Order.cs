using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace POS.Data.Models
{
  public class Order : INotifyPropertyChanged
  {
    private int orderId;

    public int OrderId
    {
      get { return orderId; }
      set { Set(ref orderId, value); }
    }
    private int quantity;

    public int Quantity
    {
      get { return quantity; }
      set { Set(ref quantity, value); }
    }

    private string status;

    public string Status
    {
      get { return status; }
      set { Set(ref status, value); }
    }

    private double pricePerUnit;

    public double PricePerUnit
    {
      get { return pricePerUnit; }
      set { Set(ref pricePerUnit, value); }
    }

    private double total;

    public double Total
    {
      get { return total; }
      set { Set(ref total, value); }
    }


    private int providerId;
    public int ProviderId
    {
      get { return providerId; }
      set { Set(ref providerId, value); }
    }

    private Provider provider;

    public Provider Provider
    {
      get { return provider; }
      set { Set(ref provider, value); }
    }


    private int productId;
    public int ProductId
    {
      get { return productId; }
      set { Set(ref productId, value); }
    }

    private Product product;

    public Product Product
    {
      get { return product; }
      set { Set(ref product, value); }
    }

    protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (Equals(storage, value))
      {
        return false;
      }
      // var j = nameof(value); can replace [CallerMemberName]
      storage = value;
      OnPropertyChanged(propertyName);
      return true;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override bool Equals(object obj) => this.OrderId.Equals((obj as Order)?.OrderId);

    public override int GetHashCode() => this.OrderId.GetHashCode();






  }
}

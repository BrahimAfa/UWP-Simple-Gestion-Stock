using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace POS.Data.Models
{
  public class Product : INotifyPropertyChanged
  {
    // add annotaion validation.
    private int productId;
    public int ProductId
    {
      get { return productId; }
      set { SetProperty(ref productId, value); }
    }

    private string name;
    public string Name
    {
      get { return name; }
      set { SetProperty(ref name, value); }
    }

    private string code;
    public string Code
    {
      get { return code; }
      set { SetProperty(ref code, value); }
    }

    private double buyingPrice;
    public double BuyingPrice
    {
      get { return buyingPrice; }
      set { SetProperty(ref buyingPrice, value); }
    }

    private double sellingPrice;
    public double SellingPrice
    {
      get { return sellingPrice; }
      set { SetProperty(ref sellingPrice, value); }
    }

    private string description;
    public string Description
    {
      get { return description; }
      set { SetProperty(ref description, value); }
    }

    private int quantity;
    public int Quantity
    {
      get { return quantity; }
      set { SetProperty(ref quantity, value); }
    }

    private int warningQuantity;
    public int WarningQuantity
    {
      get { return warningQuantity; }
      set { SetProperty(ref warningQuantity, value); }
    }

    private int categoryId;
    public int CategoryId
    {
      get { return categoryId; }
      set { SetProperty(ref categoryId, value); }
    } // this add a non nullable to the one to many relation

    private Category category;
    public Category Category
    {
      get { return category; }
      set { SetProperty(ref category, value); }
    }// this alon will create a Nullable one to many relationship

    private int providerId;
    public int ProviderId
    {
      get { return providerId; }
      set { SetProperty(ref providerId, value); }
    }

    private Provider provider;
    public Provider Provider
    {
      get { return provider; }
      set{ SetProperty(ref provider, value); }
    }
    public ICollection<Order> Orders { get; set; }



    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
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
      Debug.WriteLine($"Proprety {propertyName} has changed...");
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override bool Equals(object obj) => this.productId.Equals((obj as Product)?.productId);

    public override int GetHashCode() => this.productId.GetHashCode();

    
  }
}

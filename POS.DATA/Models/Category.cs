using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace POS.Data.Models
{
  [Table("Category")]
  public class Category : INotifyPropertyChanged
  {
    private int categoryId;
    public int CategoryId
    {
      get { return categoryId; }
      set { SetProperty(ref categoryId, value); }
    }

    private string name;
    public string Name
    {
      get { return name; }
      set { SetProperty(ref name, value); }
    }

    private string description;
    public string Description
    {
      get { return description; }
      set { SetProperty(ref description, value); }
    }

    public ICollection<Product> Products { get; set; }


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
      // Debug.WriteLine($"Proprety {propertyName} has changed..");
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override bool Equals(object obj) =>  this.CategoryId.Equals((obj as Category)?.CategoryId);
    
    public override int GetHashCode() => this.CategoryId.GetHashCode();
    
  }
}

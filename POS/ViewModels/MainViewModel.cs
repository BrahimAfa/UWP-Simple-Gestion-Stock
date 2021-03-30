using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using POS.Data.Models;
using POS.Data.Services;
using POS.Services;
using System.Linq;
using System.Collections;
using Windows.UI.Xaml.Controls;

namespace POS.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private ProductService ProductService = new ProductService();
    private ObservableCollection<Product> productsList;
    public ObservableCollection<Product> ProductsList
    {
      get
      {
        if (productsList == null)
        {
          productsList = new ObservableCollection<Product>(ProductService.GetProductsAsync().Result);
          return productsList;
        }
        return productsList;
      }
      set
      {
        productsList = value;
        RaisePropertyChanged();
      }
    }
    private ObservableCollection<Category> categoriesList;

    public ObservableCollection<Category> CategoriesList
    {
      get {
          if(categoriesList == null) {
          categoriesList = new ObservableCollection<Category>(GetCategories());
        }
        return categoriesList;
      }
      set
      {
        categoriesList = value;
        RaisePropertyChanged();
      }
    }
    private ObservableCollection<Provider> providersList;
    public ObservableCollection<Provider> ProvidersList
    {
      get
      {
        if (providersList == null)
        {
          providersList = new ObservableCollection<Provider>(GetProviders());
        }
        return providersList;
      }
      set
      {
        providersList = value;
        RaisePropertyChanged();
      }
    }
    private IDialogService DialogService { get; set; }

    public MainViewModel(){}
    public MainViewModel(IDialogService dialogService ) {
      DialogService = dialogService;
    }



    private ICommand selectionCommand;
    public ICommand SelectionCommand => selectionCommand ?? (selectionCommand = new RelayCommand<SelectionChangedEventArgs>(onSelected));

    private ICommand deleteCommand;
    public ICommand DeleteCommand => deleteCommand ?? (deleteCommand = new RelayCommand(DeleteProduct));

    private ICommand updateCommand;
    public ICommand UpdateCommand => updateCommand ?? (updateCommand = new RelayCommand(UpdateProduct));


    private ICommand saveCommand;
    public ICommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(SaveProduct));

    private Product product;

    public Product SelectedProduct
    {
      get { return product; }
      set {
        product = value;
        Debug.WriteLine("SelectedProduct is set!");
        RaisePropertyChanged();
        RaisePropertyChanged("ProductData");

      }
    }
    public Product ProductData
    {
      get { return product; }
      set
      {
        product = value;
        Debug.WriteLine("ProductData is set!");
        RaisePropertyChanged();

      }
    }
    async void onSelected(SelectionChangedEventArgs e)
    {
      try
      {
        if(e.AddedItems[0] is Category)
        {
          var item = e.AddedItems[0] as Category;
          if (item.CategoryId == -1)
          {
            var result = await DialogService.ShowAddDialog();
            if (result == App3.Views.CateoryDialog.CreateResult.SignInOK)
            {
              Debug.WriteLine("Done");
              await RefresCategoriesAsync();
            }
          }
        }
        else
        {
          var item = e.AddedItems[0] as Provider;
          if (item.ProviderId == -1)
          {
            var result = await DialogService.ShowAddDialog(true);
            if (result == App3.Views.CateoryDialog.CreateResult.SignInOK)
            {
              Debug.WriteLine("Done");
              await RefresProvidersAsync();
            }
          }
        }  
      }
      catch(Exception ex)
      {
        Debug.WriteLine(ex.Message);
      }

    }
    async void SaveProduct()
    {
      try
      {
        if (ProductData == null) return;

        bool isSaved = await ProductService.SaveProductAsync(ProductData);
        if (isSaved)
        {
          await DialogService.ShowAsync("Saved!", "Product Saved Successfully.");
          await RefresProductsAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Product Not Saved :(");
      }
      catch (Exception ex)
      {
        throw ex;
        //await DialogService.ShowAsync("Error", ex.Message);

      }
    }


    private  IEnumerable<Category> GetCategories()
    {
      var result = new CategoryService().GetCategoriesAsync().Result;
      var list = result.ToList();
      list.Insert(0, new Category() { CategoryId = -1, Name = "Add New Category" });
      return list;
    }
    private IEnumerable<Provider> GetProviders()
    {
      var result =  new ProviderService().GetProvidersAsync().Result;
      var list = result.ToList();
      Debug.WriteLine("providersl"+list.Count());
      list.Insert(0, new Provider() { ProviderId = -1, Name = "Add New Provider" });
      return list;
    }
    public async Task RefresProductsAsync()
    {
      var data = await ProductService.GetProductsAsync();
      ProductsList.Clear();
      foreach (var item in data)
      {
        ProductsList.Add(item);
      }
    }

    async void DeleteProduct()
    {
      try
      {
        if (ProductData == null) return;
        if (ProductData == null) return;
        bool isDeleted = await ProductService.DeleteProductAsync(ProductData.ProductId);
        if (isDeleted)
        {
          await DialogService.ShowAsync("Deleted!", "Product Deleted Successfully.");
          await RefresProductsAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Product Not Deleted :(");
      }
      catch (Exception ex)
      {
        throw ex;
        //await DialogService.ShowAsync("Error", ex.Message);

      }
    }

    async void UpdateProduct()
    {
      try
      {
        if (ProductData == null) return;
        bool isUpdated = await ProductService.UpdateProductAsync(ProductData);
        if (isUpdated)
        {
          await DialogService.ShowAsync("Updated!", "Product Updated Successfully.");
          await RefresProductsAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Product Not Updated :(");
      }
      catch (Exception ex)
      {
        throw ex;
        //await DialogService.ShowAsync("Error", ex.Message);

      }
    }

    public async Task RefresCategoriesAsync()
    {

      var result = await new CategoryService().GetCategoriesAsync();
      var data = result.ToList();
      data.Insert(0, new Category() { CategoryId = -1, Name = "Add New Category" });
      CategoriesList.Clear();
      foreach (var item in data)
      {
        CategoriesList.Add(item);
      }
    }
    public async Task RefresProvidersAsync()
    {

      var result = await new ProviderService().GetProvidersAsync();
      var data = result.ToList();
      data.Insert(0, new Provider() { ProviderId = -1, Name = "Add New Provider" });
      ProvidersList.Clear();
      foreach (var item in data)
      {
        ProvidersList.Add(item);
      }
    }
  }
}

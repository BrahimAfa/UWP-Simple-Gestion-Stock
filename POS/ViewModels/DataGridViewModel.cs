using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using POS.Data.Models;
using POS.Data.Services;
using POS.Services;
using Windows.UI.Xaml.Controls;

namespace POS.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
    private ProductService ProductService = new ProductService();
    private OrderService OrderService = new OrderService();

    private ObservableCollection<Order> orderList;
    public ObservableCollection<Order> OrderList
    {
      get
      {
        if (orderList == null)
        {
          orderList = new ObservableCollection<Order>(OrderService.GetOrdersAsync().Result);
        }
        return orderList;
      }
      set
      {
        orderList = value;
        RaisePropertyChanged();
      }
    }

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

    public DataGridViewModel() { }
    public DataGridViewModel(IDialogService dialogService)
    {
      DialogService = dialogService;
    }

    private ICommand deleteCommand;
    public ICommand DeleteCommand => deleteCommand ?? (deleteCommand = new RelayCommand(DeleteOrder));

    private ICommand updateCommand;
    public ICommand UpdateCommand => updateCommand ?? (updateCommand = new RelayCommand(UpdateOrder));


    private ICommand saveCommand;
    public ICommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(SaveOrder));

    private ICommand validateCommand;
    public ICommand ValidateCommand => validateCommand ?? (validateCommand = new RelayCommand(validateOrder));

    private Order order;

    public Order SelectedOrder
    {
      get { return order; }
      set
      {
        order = value;
        RaisePropertyChanged();
        RaisePropertyChanged("OrderData");

      }
    }
    public Order OrderData
    {
      get { return order; }
      set
      {
        order = value;
        RaisePropertyChanged();

      }
    }
    async void SaveOrder()
    {
      try
      {
        if (OrderData == null)
        {
          await DialogService.ShowAsync("Faild!", "Order Not Updated");
          return;
        }
        bool isSaved = await OrderService.SaveOrderAsync(OrderData);
        if (isSaved)
        {
          await DialogService.ShowAsync("Saved!", "Order Saved Successfully.");
          await RefresOrdersAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Order Not Saved :(");
      }
      catch (Exception ex)
      {
        throw ex;
        //await DialogService.ShowAsync("Error", ex.Message);

      }
    }

    private IEnumerable<Provider> GetProviders()
    {
      var result = new ProviderService().GetProvidersAsync().Result;
      var list = result.ToList();
      Debug.WriteLine("providersl" + list.Count());
      return list;
    }
    public async Task RefresOrdersAsync()
    {
      var data = await OrderService.GetOrdersAsync();
      OrderList.Clear();
      foreach (var item in data)
      {
        OrderList.Add(item);
      }
    }

    async void DeleteOrder()
    {
      try
      {
        if (OrderData == null)
        {
          await DialogService.ShowAsync("Faild!", "Order Not Deleted");
          return;
        }
        bool isDeleted = await OrderService.DeleteOrderAsync(OrderData.ProductId);
        if (isDeleted)
        {
          await DialogService.ShowAsync("Deleted!", "Order Deleted Successfully.");
          await RefresOrdersAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Product Not Deleted :(");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    async void UpdateOrder()
    {
      try
      {
        if (OrderData == null)
        {
          await DialogService.ShowAsync("Faild!", "Order Not Updated");
          return;
        }
        bool isUpdated = await OrderService.UpdateOrderAsync(OrderData);
        if (isUpdated)
        {
          await DialogService.ShowAsync("Updated!", "Order Updated Successfully.");
          await RefresOrdersAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Order Not Updated :(");
      }
      catch (Exception ex)
      {
        throw ex;
        //await DialogService.ShowAsync("Error", ex.Message);

      }
    }

    async void validateOrder()
    {
      try
      {
        if (OrderData == null)
        {
          await DialogService.ShowAsync("Faild!", "Order Not Validated");
          return;
        }
        bool isValidated = await OrderService.ValidateOrderAsync(OrderData.OrderId);
        if (isValidated)
        {
          await DialogService.ShowAsync("Validated!", "Order Validated Successfully.");
          await RefresOrdersAsync();
          return;
        }
        await DialogService.ShowAsync("Faild!", "Order Not Updated probably it a Completed Order!");
      }
      catch (Exception ex)
      {
        throw ex;
        //await DialogService.ShowAsync("Error", ex.Message);

      }
    }
    public async Task RefresProvidersAsync()
    {

      var result = await new ProviderService().GetProvidersAsync();
      var data = result.ToList();
      ProvidersList.Clear();
      foreach (var item in data)
      {
        ProvidersList.Add(item);
      }
    }
  }
}

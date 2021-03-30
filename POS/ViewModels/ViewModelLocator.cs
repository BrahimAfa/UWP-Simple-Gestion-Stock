using System;

using POS.Services;
using POS.Views;

using GalaSoft.MvvmLight.Ioc;

namespace POS.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;

        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>(new DialogService());
            Register<DataGridViewModel, DataGridPage>(new DialogService());
        }

        public DataGridViewModel DataGridViewModel => SimpleIoc.Default.GetInstance<DataGridViewModel>();

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();

        //public void Register<VM, V>(int InvalidateThiFunction,int AGin)
        //    where VM : class
        //{
        //    SimpleIoc.Default.Register<VM>();

        //    NavigationService.Configure(typeof(VM).FullName, typeof(V));
        //}
    public void Register<VM, V>(object service)
    where VM : class,new()
    {
      // this function is used to rigester viewModels and add pass data to its constructor if its nedded 
      // and in this cas i nedded it for passing dialog content service
      var ConstructorParams = service!=null ? new object[] { service } : null;
      VM instance = (VM)Activator.CreateInstance(typeof(VM), ConstructorParams);
      SimpleIoc.Default.Register(()=> instance);
      NavigationService.Configure(typeof(VM).FullName, typeof(V));
    }
  }
}

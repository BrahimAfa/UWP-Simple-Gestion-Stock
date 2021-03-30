using System;

using POS.ViewModels;

using Windows.UI.Xaml.Controls;

namespace POS.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return ViewModelLocator.Current.MainViewModel; }
        }

        public MainPage()
        {
      this.DataContext = ViewModel;
            InitializeComponent();
        }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
  }
}

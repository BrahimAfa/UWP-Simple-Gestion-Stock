using System;

using POS.ViewModels;

using Windows.UI.Xaml.Controls;

namespace POS.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel
        {
            get { return ViewModelLocator.Current.ShellViewModel; }
        }

        public ShellPage()
        {

              InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }
  }
}

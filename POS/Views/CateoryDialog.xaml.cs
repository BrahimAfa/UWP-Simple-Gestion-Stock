using POS.Data.Models;
using POS.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App3.Views
{
  public sealed partial class CateoryDialog : ContentDialog
  {
    public enum CreateResult
    {
      SignInOK,
      SignInFail,
      SignInCancel,
      Nothing
    }
    public CreateResult Result { get; private set; }
    public Category Category { get; private set; }
    private bool IsForProvider { get; set; }
    public CateoryDialog(bool isForProvider=false)
    {
      this.InitializeComponent();
      IsForProvider = isForProvider;
      if(isForProvider) this.Title = "Create Provider";
      this.Opened += SignInContentDialog_Opened;
      this.Closing += SignInContentDialog_Closing;
    }

    private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
      // Ensure the user name and password fields aren't empty. If a required field
      // is empty, set args.Cancel = true to keep the dialog open.
      if (string.IsNullOrEmpty(NameTextx.Text))
      {
        args.Cancel = true;
        errorTextBlock.Text = "Name is required.";
      }
      else if (string.IsNullOrEmpty(DescriptioText.Text))
      {
        args.Cancel = true;
        errorTextBlock.Text = "Description is required.";
      }
      ContentDialogButtonClickDeferral deferral = args.GetDeferral();
      bool isSaved = false;
      if (IsForProvider)
      {
        Provider provider = new Provider() { Name = NameTextx.Text, Description = DescriptioText.Text };
        isSaved = await new ProviderService().SaveProviderAsync(provider);
      }
      else
      {
        Category = new Category() { Name = NameTextx.Text, Description = DescriptioText.Text };
        isSaved = await new CategoryService().SaveCategoryAsync(Category);

      }
      if (isSaved){
        this.Result = CreateResult.SignInOK;
      }
      else
      {
        this.Result = CreateResult.SignInFail;
      }

      deferral.Complete();
    }

    private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
      // User clicked Cancel, ESC, or the system back button.
      this.Result = CreateResult.SignInCancel;
    }

    void SignInContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
    {
      this.Result = CreateResult.Nothing;
    }

    void SignInContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
    {
     
    }

  }
}

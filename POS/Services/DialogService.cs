using App3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using static App3.Views.CateoryDialog;

namespace POS.Services
{
  public interface IDialogService
  {
    Task ShowAsync();
    Task ShowAsync(string title,string message);
    Task<CreateResult> ShowAddDialog(bool isForProvider = false);

  }
  class DialogService : IDialogService
  {
    public string Title { get; set; }
    public string Message { get; set; }
    public ContentDialog Dialog { get; set; }
    public DialogService(string title, string message)
    {
      Title = title;
      Message = message;
    }
    public DialogService(){}

    public async Task ShowAsync()
    {
      Dialog = new ContentDialog()
      {
        Title = this.Title,
        Content = this.Message,
        CloseButtonText = "Ok",
        Opacity = 0.8
      };
      await Dialog.ShowAsync();
    }

    public async Task ShowAsync(string title, string message)
    {
      Title = title;
      Message = message;
      await ShowAsync();

    }

    public async Task<CreateResult> ShowAddDialog(bool isForProvider)
    {
      var c = new CateoryDialog(isForProvider);
      await c.ShowAsync();
      return c.Result;
    }
  }
}

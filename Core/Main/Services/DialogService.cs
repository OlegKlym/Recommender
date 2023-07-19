using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
using Recommender.Core.Services;

namespace Recommender.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title = " ", string buttonLabel = " ")
        {
            return Device.InvokeOnMainThreadAsync(() => UserDialogs.Instance.Alert(title, message, buttonLabel));
        }

        public Task<bool> ShowConfirmAsync(string message, string title, string confirmButtonLabel, string cancelButtonLabel)
        {
            return UserDialogs.Instance.ConfirmAsync(title, message, confirmButtonLabel, cancelButtonLabel);
        }

        public Task ShowLoadingAsync(string message)
        {
            return Device.InvokeOnMainThreadAsync(() => UserDialogs.Instance.ShowLoading(message));
        }

        public void HideLoading()
        {
            Device.BeginInvokeOnMainThread(() => UserDialogs.Instance.HideLoading());
        }
    }
}

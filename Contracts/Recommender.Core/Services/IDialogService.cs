using System.Threading.Tasks;

namespace Recommender.Core.Services
{
    public interface IDialogService
    {
        Task<bool> ShowConfirmAsync(string message, string title, string confirmButtonLabel, string cancelButtonLabel);
        Task ShowAlertAsync(string message, string title = " ", string buttonLabel = " ");
        Task ShowLoadingAsync(string message);
        void HideLoading();
    }
}

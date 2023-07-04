using System;
using System.Threading.Tasks;
using Recommender.Core.Services;
using Recommender.Resources.Localizations;

namespace Recommender.Helpers
{
    public interface ILoaderDecorator
    {
        Task LoadAsync(Task task, string loadingMessage = null);
    }

    public class LoaderDecorator : ILoaderDecorator
    {
        private readonly IDialogService _dialogService;

        public LoaderDecorator(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task LoadAsync(Task task, string loadingMessage = null)
        {
            loadingMessage = loadingMessage ?? AppResources.LoadingMessage;

            await _dialogService.ShowLoadingAsync(loadingMessage);

            try
            {
                await task;
            }
            finally
            {
                _dialogService.HideLoading();
            }
        }
    }
}

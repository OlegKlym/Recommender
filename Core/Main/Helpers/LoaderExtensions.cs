using System.Threading.Tasks;
using FreshMvvm;

namespace Recommender.Helpers
{
    public static class LoaderExtensions
    {
        public static Task WithLoaderAsync(this Task task, string loadingMessage = null)
        {
            return FreshIOC.Container
                .Resolve<ILoaderDecorator>()
                .LoadAsync(task, loadingMessage);
        }
    }
}

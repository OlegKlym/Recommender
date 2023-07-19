using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recommender.Core.Models;
using Recommender.Core.Services;
using Xamarin.Essentials;

namespace Recommender.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        public async Task SaveNotSeenMoviesToLocalStorageAsync(IEnumerable<MovieModel> movies)
        {
            var userData = JsonConvert.SerializeObject(movies);
            await SecureStorage.SetAsync("notSeenMovies", userData);
        }

        public async Task<IResponseData<IEnumerable<MovieModel>>> GetNotSeenMoviesFromLocalStorageAsync()
        {
            var serializedMovies = await SecureStorage.GetAsync("notSeenMovies");
            if (serializedMovies == null)
            {
                return new ResponseData<IEnumerable<MovieModel>>
                {
                    StatusCode = 401,
                    ErrorMessage = "Не вдалося отримати список рекомендацій"
                };
            }

            var deserializedMovies = JsonConvert.DeserializeObject<IEnumerable<MovieModel>>(serializedMovies);

            return new ResponseData<IEnumerable<MovieModel>>
            {
                StatusCode = 200,
                Data = deserializedMovies
            };
        }
    }
}

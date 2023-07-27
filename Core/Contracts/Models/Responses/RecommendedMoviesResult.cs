using System.Collections.Generic;
using Recommender.Core.Models;

namespace Recommender.Contracts.Models.Responses
{
    public class RecommendedMoviesResult
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<MovieModel> RecommendedMovies { get; set; }
    }
}

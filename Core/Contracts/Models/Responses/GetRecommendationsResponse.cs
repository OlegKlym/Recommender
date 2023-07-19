using System.Collections.Generic;

namespace Recommender.Core.Models
{
    public class GetRecommendationsResponse
    {
       public IEnumerable<MovieModel> RecommendedMovies { get; set; }
    }
}

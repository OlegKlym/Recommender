using System.Collections.Generic;

namespace Recommender.Core.Models.Responses
{
    public class SearchMoviesResponse
    {
       public IEnumerable<MovieModel> Movies { get; set; }
    }
}

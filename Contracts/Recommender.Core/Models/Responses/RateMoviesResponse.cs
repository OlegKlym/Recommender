using System.Collections.Generic;
using Recommender.Core.Enums;

namespace Recommender.Core.Models.Responses
{
    public class RateMoviesResponse
    {
        public int UserId { get; set; }
        public IEnumerable<int> Movies { get; set; }
        public Rate Rate { get; set; }
    }
}

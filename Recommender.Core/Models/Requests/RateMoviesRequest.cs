using System.Collections.Generic;
using Recommender.Core.Enums;

namespace Recommender.Core.Models.Requests
{
    public class RateMoviesRequest
    {
        public int UserId { get; set; }
        public IEnumerable<int> Movies { get; set; }
        public Rate Rate { get; set; }
    }
}

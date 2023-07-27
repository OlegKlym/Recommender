using System;
namespace Recommender.API.Models
{
    public class TimeoutInfo
    {
        public Uri Uri { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}

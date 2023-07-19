using Recommender.Core.Enums;

namespace Recommender.Core.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public Genre MainGenre { get; set; }
        public Genre AdditionalGenre { get; set; }
        public int Year { get; set; }
        public int Popularity { get; set; }
        public double AverageRating { get; set; }
        public string Description { get; set; }
    }
}
namespace Recommender.Models
{
    public class MovieModel
    {
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public int Year { get; set; }
        
        public string ActorsList { get; set; }
        
        public bool IsWatched { get; set; }
    }
}
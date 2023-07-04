using FreshMvvm;
using Recommender.Core.Models;

namespace Recommender.ViewModels
{
    public class MovieDetailsPageModel : FreshBasePageModel
    {
        public MovieModel MovieDetails { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            MovieDetails = initData as MovieModel;
        }
    }
}
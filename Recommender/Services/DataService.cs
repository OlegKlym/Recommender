using System.Collections.Generic;
using Recommender.Core.Models;

namespace Recommender.Services
{
    public static class DataService
    {
        public static IEnumerable<MovieModel> GetMovies()
        {
            return new List<MovieModel>
            {
                new MovieModel
                {
                    Title = "Хрещений батько",
                    Image = "https://image.tmdb.org/t/p/w1280/iVZ3JAcAjmguGPnRNfWFOtLHOuY.jpg",
                    Year = 2010,
                },
                new MovieModel
                {
                    Title = "Титанік", Image = "https://image.tmdb.org/t/p/w1280/r3sYKBtoNHPyS9vkaA8dJyh8grG.jpg",
                    Year = 2011
                },
                new MovieModel
                {
                    Title = "Люцифер", Image = "https://img.hurtom.com/i/2020/08/Lucifer-ukr.jpg",
                    Year = 2012
                },
                new MovieModel
                {
                    Title = "Початок", Image = "https://image.tmdb.org/t/p/w1280/7SivRwOLuA6DR09zNJ9JIo14GyX.jpg",
                    Year = 2013
                },
                new MovieModel
                {
                    Title = "Титанік", Image = "https://image.tmdb.org/t/p/w1280/r3sYKBtoNHPyS9vkaA8dJyh8grG.jpg",
                    Year = 2014
                },
                new MovieModel
                {
                    Title = "Люцифер", Image = "https://image.tmdb.org/t/p/w1280/1sBx2Ew4WFsa1YY32vlHt079O03.jpg",
                    Year = 2015
                },
                new MovieModel
                {
                    Title = "Початок", Image = "https://image.tmdb.org/t/p/w1280/7SivRwOLuA6DR09zNJ9JIo14GyX.jpg",
                    Year = 2016
                },
            };
        }
    }
}
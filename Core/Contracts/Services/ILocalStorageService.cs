﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Recommender.Core.Models;

namespace Recommender.Core.Services
{
    public interface ILocalStorageService
    {
        Task SaveMoviesToLocalStorageAsync(IEnumerable<MovieModel> movies);
        Task<IResponseData<IEnumerable<MovieModel>>> GetMoviesFromLocalStorageAsync();
    }
}
using LibraryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Repositories.Interfaces
{
    public interface IMovieRepositroy
    {

        public Task Create(Movie movie);
        public Task<bool> Delete(string id);
        public Task<bool> Update(Movie movie);
        public Task<Movie> GetMovieById(string id);
        public Task<IEnumerable<Movie>> GetMovies();
        public Task<IEnumerable<Movie>> GetMoviesByName(string name);
        public Task<IEnumerable<Movie>> GetMoviesByCategory(string categoryName);
    }
}

using LibraryAPI.Data;
using LibraryAPI.Data.Interfaces;
using LibraryAPI.Entities;
using LibraryAPI.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Repositories
{
    public class MovieRepositoryL : IMovieRepositroy
    {

        public readonly ILibraryDBContext _context;

        public MovieRepositoryL(ILibraryDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(Movie movie)
        {
            await _context.Movies.InsertOneAsync(movie);
        }

        public async Task<bool> Delete(int id)
        {
            FilterDefinition<Movie> filter = Builders<Movie>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Movies.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        public async Task<bool> Update(Movie movie)
        {
            var updateRes = await _context.Movies.ReplaceOneAsync(filter: m => m.Id == movie.Id, replacement: movie);
            return updateRes.IsAcknowledged && updateRes.ModifiedCount > 0;
        }
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _context.Movies.Find(m => true).ToListAsync();
        }


        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByName(string name)
        {
            FilterDefinition<Movie> filter = Builders<Movie>.Filter.ElemMatch(m => m.Name, name);
            return await _context.Movies.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByCategory(string categoryName)
        {
            FilterDefinition<Movie> filter = Builders<Movie>.Filter.ElemMatch(m => m.Category, categoryName);
            return await _context.Movies.Find(filter).ToListAsync();
        }


    }
}

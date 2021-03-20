using LibraryAPI.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Data
{
    public class LibraryContextSeed
    {
        public static void SeedData(IMongoCollection<Movie> movieCollection)
        {
            if (movieCollection.Find(m => true).Any())
            {
            movieCollection.InsertManyAsync(GetPreConfiguredData());
            }
        }

        public static IEnumerable<Movie> GetPreConfiguredData()
        {


            return new List<Movie>()
            {
                new Movie
                {
                    Name = "Harry Potter",
                    Description = "Harry Potter story",
                    Category = "Fantasy",
                }
            };
        }

    }
}

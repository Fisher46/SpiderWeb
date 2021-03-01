using LibraryAPI.Data.Interfaces;
using LibraryAPI.Entities;
using LibraryAPI.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Data
{
    public class LibraryContext : ILibraryDBContext
    {
        public IMongoCollection<Movie> Movies { get; set; }
        public LibraryContext(ICatalogDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Movies = database.GetCollection<Movie>(settings.CollectionName);

            LibraryContextSeed.SeedData(Movies);
        }
      
    }
}

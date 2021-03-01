using LibraryAPI.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Data.Interfaces
{
    public interface ILibraryDBContext
    {
        public IMongoCollection<Movie> Movies { get; set; }
    }
}

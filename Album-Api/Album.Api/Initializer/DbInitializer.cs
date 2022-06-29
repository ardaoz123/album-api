using AlbumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.Api.Initializer
{
    public static class DbInitializer
    {
        public static void Initialize(AlbumContext context)
        {
            context.Database.EnsureCreated();

            if (context.Album.Any())
                return;

            var albums = new List<AlbumModel>
            {
                new() { Id = 1, Name = "Trilogy" , Artist = "The Weeknd", ImageUrl = null },
                new() { Id = 2, Name = "Nothing was the same", Artist = "Drake", ImageUrl = null }
            };

            context.AddRange(albums);
            context.SaveChanges();
        }
    }
}

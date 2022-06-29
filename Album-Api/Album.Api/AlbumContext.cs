using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Album.Api
{
    public class AlbumContext : DbContext
    {
        public DbSet<AlbumApi.Models.AlbumModel> Album { get; set; }

        public AlbumContext(DbContextOptions<AlbumContext> options) : base(options) { }

    }
}

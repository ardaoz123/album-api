using AlbumApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.Api.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly AlbumContext _context;

        public AlbumService(AlbumContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AlbumModel>> GetAll()
        {
            return await _context.Album.ToListAsync();
        }

        public async Task<AlbumModel?> TryGetAlbumModel(int id)
        {
            var albumModel = await _context.Album.FindAsync(id);
            return albumModel;
        }

        public async Task<AlbumModel> Create(AlbumModel model)
        {
            _context.Album.Add(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return model;
        }

        public async Task Delete(AlbumModel model)
        {
            _context.Album.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Put(int id, AlbumModel model)
        {
            var albumModel = await _context.Album.FindAsync(id);
            if(albumModel == null)
            {
                return false;
            }

            albumModel.Artist = model.Artist;
            albumModel.Name = model.Name;
            albumModel.ImageUrl = model.ImageUrl;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumModelExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        private bool AlbumModelExists(int id)
        {
            return _context.Album.Any(e => e.Id == id);
        }
    }
}

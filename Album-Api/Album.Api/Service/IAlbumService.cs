using AlbumApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Service
{
    public interface IAlbumService
    {
        Task<AlbumModel> Create(AlbumModel model);

        Task Delete(AlbumModel model);

        Task<IEnumerable<AlbumModel>> GetAll();

        Task<AlbumModel?> TryGetAlbumModel(int id);

        Task<bool> Put(int id, AlbumModel model);
    }
}
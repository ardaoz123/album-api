using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Album.Api;
using AlbumApi.Models;
using Album.Api.Service;

namespace Album.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;

        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        // GET: api/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetAlbum()
        {
            return Ok(await _service.GetAll());
        }

        // GET: api/Album/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumModel>> GetAlbumModel(int id)
        {
            var albumModel = await _service.TryGetAlbumModel(id);

            if (albumModel == null)
            {
                return NotFound();
            }

            return Ok(albumModel);
        }

        // PUT: api/Album/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAlbumModel(int id, AlbumModel albumModel)
        {
            if (id != albumModel.Id)
            {
                return BadRequest();
            }

            bool succes = await _service.Put(id, albumModel);

            if (!succes)
            {
                return NotFound();
            }
            

            return NoContent();
        }

        // POST: api/Album
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlbumModel>> PostAlbumModel(AlbumModel albumModel)
        {
            var created = await _service.Create(albumModel);
            if(created == null)
            {
                return BadRequest("Album with id already exists");
            }
            return CreatedAtAction("GetAlbumModel", new { id = created.Id}, created);
        }

        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAlbumModel(int id)
        {
            var model = await _service.TryGetAlbumModel(id);
            
            if (model == null)
            {
                return NotFound();
            }

            await _service.Delete(model);

            return NoContent();
        }
    }
}

﻿using Album.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using AlbumApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Album.Api.Service;

namespace Album.Api.Tests
{
    public class UnitTestController
    {
        private readonly AlbumController _controller;

        public UnitTestController()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DatabaseConfig.DefaultConnection = configuration.GetConnectionString("DefaultConnection");
            IServiceProvider provider = new ServiceCollection().AddDbContext<AlbumContext>().AddScoped<IAlbumService, AlbumService>().BuildServiceProvider();
            IAlbumService service = provider.GetRequiredService<IAlbumService>();
            this._controller = new AlbumController(service);
        }
        
        [Fact]
        public async Task GetAlbum()
        {
            ActionResult<IEnumerable<AlbumModel>> response = await _controller.GetAlbum();
            Assert.IsType<OkObjectResult>(response.Result);
            var okObject = (OkObjectResult)response.Result;
            var value = (IEnumerable<AlbumModel>)okObject.Value;

            //Test DB Initializer working
            Assert.Contains(value, x => x.Id == 1);
            Assert.Contains(value, x => x.Id == 2);
        }

        [Fact]
        public async Task GetAlbumModelExisting()
        {
            ActionResult<AlbumModel> response = await _controller.GetAlbumModel(1);
            Assert.IsType<OkObjectResult>(response.Result);

            var okObject = (OkObjectResult)response.Result;
            var value = (AlbumModel)okObject.Value;

            Assert.Equal("Trilogy", value.Name);
            Assert.Equal("The Weeknd", value.Artist);
        }

        [Fact]
        public async Task GetAlbumModelNonExisting()
        {
            ActionResult<AlbumModel> response = await _controller.GetAlbumModel(1000);
            Assert.IsType<NotFoundResult>(response.Result);
        }


        [Fact]
        public async Task PostAlbumModel()
        {
            int Id = new Random().Next(10, 10000000);

            var album = new AlbumModel(Id, "The Woo", "Pop Smoke", null);

            ActionResult<AlbumModel> response = await _controller.PostAlbumModel(album);
            Assert.IsType<CreatedAtActionResult>(response.Result);

            var okObject = (CreatedAtActionResult)response.Result;
            var value = (AlbumModel)okObject.Value;

            Assert.Equal(album.Name, value.Name);
            Assert.Equal(album.Artist, value.Artist);
            Assert.Equal(album.Id, value.Id);

            await _controller.DeleteAlbumModel(album.Id);
        }


        [Fact]
        public async Task PostAlbumModelDuplicateKey()
        {
            int Id = new Random().Next(10, 10000000);

            var album = new AlbumModel(Id, "The Woo", "Pop Smoke", null);
            ActionResult<AlbumModel> response = await _controller.PostAlbumModel(album);
            Assert.IsType<CreatedAtActionResult>(response.Result);

            response = await _controller.PostAlbumModel(album);
            Assert.IsType<BadRequestObjectResult>(response.Result);

            await _controller.DeleteAlbumModel(album.Id);
        }

        [Fact]
        public async Task DeleteAlbumModel()
        {
            int Id = new Random().Next(10, 10000000);
            var album = new AlbumModel(Id, "The Woo", "Pop Smoke", null);

            ActionResult<AlbumModel> response = await _controller.PostAlbumModel(album);
            Assert.IsType<CreatedAtActionResult>(response.Result);


            ActionResult response2 = await _controller.DeleteAlbumModel(album.Id);
            Assert.IsType<NoContentResult>(response2);
        }

        [Fact]
        public async Task DeleteAlbumNonExistingModel()
        {
            int Id = new Random().Next(10, 10000000);
            var album = new AlbumModel(Id, "The Woo", "Pop Smoke", null);

            ActionResult response2 = await _controller.DeleteAlbumModel(album.Id);
            Assert.IsType<NotFoundResult>(response2);
        }

        [Fact]
        public async Task PutAlbumModel()
        {
            int Id = new Random().Next(10, 10000000);
            var album = new AlbumModel(Id, "The Woo", "Pop Smoke", null);

            ActionResult<AlbumModel> response = await _controller.PostAlbumModel(album);
            Assert.IsType<CreatedAtActionResult>(response.Result);

            var album2 = new AlbumModel(Id, "Slime Season", "Young Thug", null);

            ActionResult<AlbumModel> response2 = await _controller.PutAlbumModel(Id, album2);
            Assert.IsType<NoContentResult>(response2.Result);


            ActionResult<AlbumModel> response3 = await _controller.GetAlbumModel(Id);
            Assert.IsType<OkObjectResult>(response3.Result);

            var okObject = (OkObjectResult)response3.Result;
            var value = (AlbumModel)okObject.Value;

            Assert.Equal(album2.Name, value.Name);
            Assert.Equal(album2.Artist, value.Artist);
            Assert.Equal(album2.Id, value.Id);

            await _controller.DeleteAlbumModel(album.Id);
        }

        [Fact]
        public async Task PutAlbumModelDifferentID()
        {
            int Id = new Random().Next(10, 10000000);
            
            var album2 = new AlbumModel(Id, "Slime Season", "Young Thug", null);

            ActionResult<AlbumModel> response2 = await _controller.PutAlbumModel(Id+1, album2);
            Assert.IsType<BadRequestResult>(response2.Result);

        }

        [Fact]
        public async Task PutAlbumModelNotFound()
        {
            int Id = new Random().Next(10, 10000000);

            var album2 = new AlbumModel(Id, "Slime Season", "Young Thug", null);

            ActionResult<AlbumModel> response2 = await _controller.PutAlbumModel(Id, album2);
            Assert.IsType<NotFoundResult>(response2.Result);

        }

    }
}

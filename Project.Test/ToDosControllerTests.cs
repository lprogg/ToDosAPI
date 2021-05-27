using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controllers;
using Project.Models;
using Xunit;

namespace Project.Test
{
    public class ToDosControllerTests
    {
        private static async Task<ToDosContext> GetToDosContext()
        {
            var options = new DbContextOptionsBuilder<ToDosContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var toDosContext = new ToDosContext(options);
            await toDosContext.Database.EnsureCreatedAsync();

            return toDosContext;
        }

        [Fact]
        public async Task PostToDosWillReturnA201Created()
        {
            var toDosContext = await GetToDosContext();

            var toDosController = new ToDosController(toDosContext);

            var postToDo = toDosController.PostToDo(new() {Description = "some description"});

            Assert.IsType<CreatedAtActionResult>(postToDo.Result);
        }

        [Fact]
        public async Task PutToDoItemWillReturnA200Created()
        {
            var toDosContext = await GetToDosContext();
            var toDosController = new ToDosController(toDosContext);
            var putToDo = toDosController.PutToDoItem(1, new ToDos(){Id = 1, Description = "some description"});

            Assert.IsType<ObjectResult>(putToDo);
        }
    }
}
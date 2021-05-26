using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Controllers;
using Project.Models;
using Xunit;

namespace Project.Test
{
    public class ToDosControllerTests
    {
        [Fact]
        public async Task PostToDoWillReturnA201Created()
        {
            var databaseContext = await GetToDosContext();

            var todosController = new ToDosController(databaseContext);

            // TODO: this is how the one post test might look
            // var postToDo = todosController.PostToDo(new ToDos() {Description = "some description"});
            //
            // Assert.IsType<CreatedAtActionResult>(postToDo.Result);
        }

        private static async Task<ToDosContext> GetToDosContext()
        {
            var options = new DbContextOptionsBuilder<ToDosContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ToDosContext(options);
            await databaseContext.Database.EnsureCreatedAsync();

            return databaseContext;
        }
    }
}
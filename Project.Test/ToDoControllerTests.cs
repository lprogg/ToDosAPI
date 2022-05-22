using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controllers;
using Project.Models;
using Xunit;

namespace Project.Test
{
    public class ToDoControllerTests
    {
        private static async Task<ToDoContext> GetToDoContext()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var toDoContext = new ToDoContext(options);
            await toDoContext.Database.EnsureCreatedAsync();

            return toDoContext;
        }

        [Fact]
        public async Task PostToDosWillReturnA201Created()
        {
            var toDoContext = await GetToDoContext();

            var toDoController = new ToDoController(toDoContext);

            var postToDo = toDoController.PostItem(new() {Description = "some description"});

            Assert.IsType<CreatedAtActionResult>(postToDo.Result);
        }

        [Fact]
        public async Task PutToDoItemWillReturnA200Created()
        {
            var toDoContext = await GetToDoContext();
            var toDoController = new ToDoController(toDoContext);
            var putToDo = toDoController.PutItem(1, new ToDo(){Id = 1, Description = "some description"});

            Assert.IsType<ObjectResult>(putToDo);
        }
    }
}
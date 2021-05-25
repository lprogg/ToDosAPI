using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Route("todos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDosContext _context;

        public ToDosController(ToDosContext context) => _context = context;

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ToDos> PostToDo(ToDos todo)
        {
            try
            {
                if (todo.Description == null)
                {
                    return BadRequest();
                }

                _context.ToDosItems.Add(todo);
                _context.SaveChanges();

                return CreatedAtAction("GetToDoItem", new ToDos {Id = todo.Id}, todo);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = e.InnerException != null
                            ? $"{e.Message} {e.InnerException.Message}"
                            : e.Message
                    });
            }
        }

        [HttpGet("{Id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public ActionResult<ToDos> GetToDoItem(int Id)
        {
            try
            {
                var todoItem = _context.ToDosItems.Find(Id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                return todoItem;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = e.InnerException != null
                            ? $"{e.Message} {e.InnerException.Message}"
                            : e.Message
                    });
            }
        }
    }
}
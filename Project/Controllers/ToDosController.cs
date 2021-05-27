using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult<ToDos> PostToDo(ToDos todos)
        {
            try
            {
                if (todos.Description == null)
                {
                    return BadRequest();
                }

                _context.ToDosItems.Add(todos);
                _context.SaveChanges();

                return CreatedAtAction("GetToDoItem", new ToDos {Id = todos.Id}, todos);
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

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ToDos> GetToDoItem(int id)
        {
            try
            {
                var toDosItem = _context.ToDosItems.Find(id);

                if (toDosItem == null)
                {
                    return NotFound();
                }

                return toDosItem;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new
                    {
                        Message = e.InnerException != null
                            ? $"{e.Message} {e.InnerException.Message}"
                            : e.Message
                    });
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ToDos>> GetToDoItems()
        {
            try
            {
                return _context.ToDosItems;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new
                    {
                        Message = e.InnerException != null
                            ? $"{e.Message} {e.InnerException.Message}"
                            : e.Message
                    });
            }
        }

        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult PutToDoItem(int id, ToDos todos)
        {
            try
            {
                if (id != todos.Id)
                {
                    return BadRequest();
                }

                _context.Entry(todos).State = EntityState.Modified;
                _context.SaveChanges();

                return CreatedAtAction("GetToDoItem", new ToDos {Id = todos.Id}, todos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new
                    {
                        Message = e.InnerException != null
                            ? $"{e.Message} {e.InnerException.Message}"
                            : e.Message
                    });
            }
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ToDos> DeleteToDoItem(int id)
        {
            try
            {
                var toDosItem = _context.ToDosItems.Find(id);

                if (toDosItem == null)
                {
                    return NotFound();
                }

                _context.ToDosItems.Remove(toDosItem);
                _context.SaveChanges();

                return toDosItem;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new
                    {
                        Message = e.InnerException != null
                            ? $"{e.Message} {e.InnerException.Message}"
                            : e.Message
                    });
            }
        }

        [HttpPatch("{id}")]
        public ActionResult PatchToDoCheck(int id, [FromBody]JsonPatchDocument<ToDos> value)
        {
            try
            {
                var toDosItem = _context.ToDosItems.Find(id);

                if (toDosItem == null)
                {
                    return BadRequest();
                }

                value.ApplyTo(toDosItem);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
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
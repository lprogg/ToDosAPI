using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    [Route("todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context) => _context = context;

        private StatusMethods _code = new();

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ToDo> PostItem(ToDo todo)
        {
            try
            {
                if (String.IsNullOrEmpty(todo.Description))
                {
                    return BadRequest("Description field is null or empty.");
                }

                _context.ToDoItems.Add(todo);
                _context.SaveChanges();

                return CreatedAtAction("GetItem", new ToDo {Id = todo.Id}, todo);
            }
            catch (Exception e)
            {
                return _code.PostStatus();
            }
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ToDo> GetItem(int id)
        {
            try
            {
                var toDoItem = _context.ToDoItems.Find(id);

                if (toDoItem == null)
                {
                    return NotFound("The record with the specified id does not exist.");
                }

                return toDoItem;
            }
            catch (Exception e)
            {
                return _code.Status();
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ToDo>> GetItems()
        {
            try
            {
                return _context.ToDoItems;
            }
            catch (Exception e)
            {
                return _code.Status();
            }
        }

        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult PutItem(int id, ToDo todo)
        {
            try
            {
                if (id != todo.Id)
                {
                    return BadRequest("The specified id was not found in any of the stored records.");
                }

                _context.Entry(todo).State = EntityState.Modified;
                _context.SaveChanges();

                return CreatedAtAction("GetItem", new ToDo {Id = todo.Id}, todo);
            }
            catch (Exception e)
            {
                return _code.Status();
            }
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ToDo> DeleteToDoItem(int id)
        {
            try
            {
                var toDoItem = _context.ToDoItems.Find(id);

                if (toDoItem == null)
                {
                    return NotFound("Record with the specified id does not exist.");
                }

                _context.ToDoItems.Remove(toDoItem);
                _context.SaveChanges();

                return toDoItem;
            }
            catch (Exception e)
            {
                return _code.Status();
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody]JsonPatchDocument<ToDo> value)
        {
            try
            {
                var toDoItem = _context.ToDoItems.Find(id);

                if (toDoItem == null || value == null)
                {
                    return BadRequest("Record with the specified id does not exist or Json Patch " +
                                      "format incorrect.");
                }

                value.ApplyTo(toDoItem, ModelState);
                _context.SaveChanges();
                return new ObjectResult(toDoItem);
            }
            catch (Exception e)
            {
                return _code.Status();
            }
        }
    }
}
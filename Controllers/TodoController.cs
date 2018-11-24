using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TodoContext _context;

        public TaskController(TodoContext context)
        {
            _context = context;

            if (_context.Tasks.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Tasks.Add(new TaskItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll()
        {
            return _context.Tasks.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TaskItem> GetById(long id)
        {
            var item = _context.Tasks.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(TaskItem item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TaskItem item)
        {

            var todo = _context.Tasks.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.Status == Status.Done)
            {
                 return BadRequest("La tarea no puede ser modicada");
            }



            todo.Name = item.Name;
            todo.Status = item.Status;

            _context.Tasks.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param> 
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Tasks.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
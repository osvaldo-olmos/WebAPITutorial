using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;

            if (_context.TaskItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TaskItems.Add(new TaskItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTask")]
        public ActionResult<TaskItem> GetById(long id)
        {
            var item = _context.TaskItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(TaskItem item)
        {
            _context.TaskItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTask", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TaskItem item)
        {
            var todo = _context.TaskItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TaskItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific TaskItem.
        /// </summary>
        /// <param name="id"></param> 
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TaskItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
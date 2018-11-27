using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskApi.Models;

namespace TaskApi.Controllers
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
            if(item.Status !=TaskStatus.Todo)
            {
                return BadRequest($"No se puede crear una tarea con estado {item.Status}");
            }
            _context.TaskItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTask", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TaskItem item)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            //Si el item esta finalizado, no se puede updetear
            if(task.Status == TaskStatus.Canceled &&
            item.Status == TaskStatus.Done)
            {
                return BadRequest("No se puede pasar una tarea cancelada a finalizada");
            }

            task.Status = item.Status;
            task.Name = item.Name;

            _context.TaskItems.Update(task);
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
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskApi.DTO;
using TaskApi.Extensions;
using TaskApi.Models;
using TodoApi.DTO;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly IMapper _mapper;

        public TaskController(TaskContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            if (_context.TaskItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TaskItems.Add(new TaskItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TaskDTO>> GetAll()
        {
            return _mapper.Map<List<TaskDTO>>(_context.TaskItems.ToList());
        }

        [HttpGet("{id}", Name = "GetTask")]
        public ActionResult<TaskDTO> GetById(long id)
        {
            var item = _context.TaskItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return _mapper.Map<TaskDTO>(item);
        }

        [HttpPost]
        public IActionResult Create(CreateTaskDTO taskDTO)
        {
            TaskItem task = _mapper.Map<TaskItem>(taskDTO);
            _context.TaskItems.Add(task);
            _context.SaveChanges();

            return CreatedAtRoute("GetTask", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateTaskDTO item)
        {
            TaskItem updatedTask = _mapper.Map<TaskItem>(item);

            var task = _context.TaskItems.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            if (task.Status.isFinalStatus())
            {
                return BadRequest("La tarea se encuentra en un estado final, no puede ser modificada");
            }

            task.Status = updatedTask.Status;
            task.Name = updatedTask.Name;

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
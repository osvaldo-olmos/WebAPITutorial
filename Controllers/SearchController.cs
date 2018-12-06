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
    public class SearchController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly IMapper _mapper;

        public SearchController(TaskContext context, IMapper mapper)
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
        public ActionResult<List<TaskDTO>> Get(string searchString)
        {
            return _mapper.Map<List<TaskDTO>>(_context.TaskItems.Where(x => x.Name.ToUpper().Contains(searchString.ToUpper())));
        }

    }
}
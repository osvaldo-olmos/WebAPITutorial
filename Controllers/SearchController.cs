using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            List<TaskItem> result = null;
            if(searchString ==null)
            {
                result =_context.TaskItems.ToList();
            }else
            {
                result =_context.TaskItems.Where(x => x.Name.ToLowerInvariant().Contains(searchString.ToLowerInvariant())).ToList();
            }
            
            return _mapper.Map<List<TaskDTO>>(result);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {

        private readonly TodoListContext _context;

        public TaskController(TodoListContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasksbyId(int id)
        {
            var Tasks = new List<Task>();
            Tasks = await _context.Tasks.Where(c => c.User.Equals(id)).ToListAsync();
            if (Tasks == null)
            {
                return NotFound();
            }
            return Tasks;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task>>> GetMyTasks()
        {
            int id_user = 1;
            var Tasks = new List<Task>();
            Tasks = await _context.Tasks.Where(c => c.User.Equals(id_user)).ToListAsync();
            if (Tasks == null)
            {
                return NotFound();
            }
            return Tasks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Task>> CreateTask(Task task)
        {
            if (task == null)
            {
                return NoContent();
            }
            else
            {
                int id_user = 1;
                task.User = id_user;
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTasksbyId), new { id = task.Id }, task);
            }

        }
    }
}

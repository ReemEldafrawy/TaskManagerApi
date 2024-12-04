using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public TaskManagerController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> CreatrTask([FromBody] TaskManager task)
        {
            _dbContext.Set<TaskManager>().Add(task);
            await _dbContext.SaveChangesAsync();
            return Ok(task.Id);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] TaskManager task)
        {
            var ExistingTask = await _dbContext.Set<TaskManager>().FindAsync(id);
            if (ExistingTask == null)
            {
                return NotFound();
            }
            else
            {
                ExistingTask.Name = task.Name;
                ExistingTask.Description = task.Description;
                ExistingTask.DueDate = task.DueDate;
                ExistingTask.IsCompleted = task.IsCompleted;
                ExistingTask.Priority = task.Priority;
                await _dbContext.SaveChangesAsync();
                return Ok(ExistingTask);

            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var ExistingTask = await _dbContext.Set<TaskManager>().FindAsync(id);
            if (ExistingTask == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Set<TaskManager>().Remove(ExistingTask);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }


        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _dbContext.Set<TaskManager>().ToListAsync();
            return Ok(tasks);
        }





    }

}

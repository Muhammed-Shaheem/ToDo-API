using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoLibrary.DataAccess;
using ToDoLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ITodoData todoData;
        private readonly ILogger<ToDosController> logger;

        public ToDosController(ITodoData todoData, ILogger<ToDosController> logger.)
        {
            this.todoData = todoData;
            this.logger = logger;
        }
        private string? GetUserId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoModel>>> Get()
        {
            logger.LogInformation("Get: api/Todos");
            try
            {
                string? userId = GetUserId();
                var output = await todoData.GetAllAssigned(int.Parse(userId));

                return Ok(output);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "the Get call api/todos failed."); 
                return BadRequest();
            }
        }

     

        [HttpGet("{todoId}")]
        public async Task<ActionResult<TodoModel>> Get(int todoId)
        {
            logger.LogInformation("Get: api/Todos/{TodoId}",todoId);

            try
            {
                var userId = GetUserId();
                var output = await todoData.GetOneAssigned(int.Parse(userId), todoId);

                return Ok(output);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "the Get call to {ApiPath} failed. The Id was {id}",$"api/Todos/Id",todoId);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
        {
            logger.LogInformation("Get: api/Todos (Task: {Task})",task);

            try
            {
                var userId = GetUserId();
                var output = await todoData.Create(task, int.Parse(userId));

                return Ok(output);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "The POST call to api/Todos failed. task value was {task}", task);
                return BadRequest();
            }

        }

        [HttpPut("{todoId}")]
        public async Task<IActionResult> Put(int todoId, [FromBody] string task)
        {
            await todoData.UpdateTask(task,int.Parse(GetUserId()), todoId);
            return Ok();
        }

        [HttpPut("{todoId}/Complete")]
        public async Task<IActionResult> Complete(int todoId)
        {
          await todoData.CompleteTodo(int.Parse(GetUserId()), todoId);
            return Ok();
        }

        [HttpDelete("{todoId}")]
        public async Task<IActionResult> Delete(int todoId)
        {
            await todoData.Delete(int.Parse(GetUserId()), todoId);
            return Ok();
        }
    }
}

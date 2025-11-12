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

        public ToDosController(ITodoData todoData)
        {
            this.todoData = todoData;
        }


        [HttpGet]
        public async Task<ActionResult<List<TodoModel>>> Get()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var output = await todoData.GetAllAssigned(int.Parse(userId));

            return Ok(output);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> Get(int todoId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var output = await todoData.GetOneAssigned(int.Parse(userId),todoId);  
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        
        [HttpPut("{id}/Complete")]
        public void Complete(int id)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

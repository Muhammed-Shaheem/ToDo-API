using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess;

public class TodoData
{
    private readonly ISqlDataAccess sqlDataAccess;

    public TodoData(ISqlDataAccess sqlDataAccess)
    {
        this.sqlDataAccess = sqlDataAccess;
    }

    public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
    {
        return sqlDataAccess.LoadData<TodoModel, dynamic>("spTodos_GetAllAssigned", new { Assignedto = assignedTo }, "Default");
    }

    public async Task<TodoModel?> GetOneAssigned(int assignedTo, int id)
    {
        var todo = await sqlDataAccess.LoadData<TodoModel, dynamic>("spTodos_GetAllAssigned", new { Assignedto = assignedTo, Id = id }, "Default");
        return todo.FirstOrDefault();
    }
    
    public async Task<TodoModel?> Create(string task, int assignedTo)
    {
        var todo = await sqlDataAccess.LoadData<TodoModel, dynamic>("spTodos_Create", new { Assignedto = assignedTo, Task = task}, "Default");
        return todo.FirstOrDefault();
    }
}

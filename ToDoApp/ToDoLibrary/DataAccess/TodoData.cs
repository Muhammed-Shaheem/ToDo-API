using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess;

public class TodoData : ITodoData
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
        var todo = await sqlDataAccess.LoadData<TodoModel, dynamic>("spTodos_Create", new { Assignedto = assignedTo, Task = task }, "Default");
        return todo.FirstOrDefault();
    }

    public Task UpdateTask(string task, int assignedTo, int todoId)
    {
        return sqlDataAccess.SaveData<dynamic>("spTodos_UpdateTask", new { Task = task, Assignedto = assignedTo, TodoId = todoId }, "Default");
    }

    public Task CompleteTodo(int assignedTo, int todoId)
    {
        return sqlDataAccess.SaveData<dynamic>("spTodos_CompleteTodo", new { Assignedto = assignedTo, TodoId = todoId }, "Default");
    }

    public Task Delete(int assignedTo, int todoId)
    {
        return sqlDataAccess.SaveData<dynamic>("spTodos_Delete", new { Assignedto = assignedTo, TodoId = todoId }, "Default");
    }


}

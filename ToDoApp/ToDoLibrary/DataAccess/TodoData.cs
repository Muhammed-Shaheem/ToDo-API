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
      return  sqlDataAccess.LoadData<TodoModel, dynamic>("spTodos_GetAllAssigned", new {Assignedto = assignedTo},"Default");
    }
}

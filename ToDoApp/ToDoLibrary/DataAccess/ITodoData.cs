using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess
{
    public interface ITodoData
    {
        Task CompleteTodo(int assignedTo, int todoId);
        Task<TodoModel?> Create(string task, int assignedTo);
        Task Delete(int assignedTo, int todoId);
        Task<List<TodoModel>> GetAllAssigned(int assignedTo);
        Task<TodoModel?> GetOneAssigned(int assignedTo, int id);
        Task UpdateTask(string task, int assignedTo, int todoId);
    }
}
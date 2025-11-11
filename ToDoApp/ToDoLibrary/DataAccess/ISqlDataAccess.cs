
namespace ToDoLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, P>(string sql, P parameters, string connectionStringName);
        Task SaveData<P>(string sql, P parameters, string connectionStringName);
    }
}
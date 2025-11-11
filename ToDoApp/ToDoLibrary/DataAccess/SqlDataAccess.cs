using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ToDoLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration configuration;

    public SqlDataAccess(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<List<T>> LoadData<T, P>(string sql, P parameters, string connectionStringName)
    {
        string? connectionString = configuration.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);

        return rows.ToList();


    }

    public Task SaveData<P>(string sql, P parameters, string connectionStringName)
    {
        string? connectionString = configuration.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);

        return connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);




    }

}

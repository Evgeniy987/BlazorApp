using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    public string ConnectionId { get; set; } = "Default";

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<T> LoadItem<T, U>(
        string storedProcedure,
        U parameters)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionId));
        return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters,
        commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> LoadItems<T, U>(
        string storedProcedure,
        U parameters)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionId));
        return await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task Execute<T>(
        string storedProcedure,
        T parameters
        )
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionId));

        _ = await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> InsertData<T>(
       string sql,
       T parameters)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionId));
        return await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task SaveData<T>(
        string sql,
        T parameters)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionId));
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<T> Select<T, U>(
        string sql,
        U parameters)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(ConnectionId));

        return await connection.QueryFirstAsync<T>(sql, parameters,
            commandType: CommandType.Text);
    }
}

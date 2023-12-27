public interface ISqlDataAccess
{
    string ConnectionId { get; set; }

    Task Execute<T>(string storedProcedure, T parameters);
    Task<int> InsertData<T>(string sql, T parameters);
    Task<T> LoadItem<T, U>(string storedProcedure, U parameters);
    Task<IEnumerable<T>> LoadItems<T, U>(string storedProcedure, U parameters);
    Task SaveData<T>(string sql, T parameters);
    Task<T> Select<T, U>(string sql, U parameters);
}
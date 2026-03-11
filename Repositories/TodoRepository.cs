using Microsoft.Data.SqlClient;
using System.Threading.RateLimiting;
using Todo_Service.Enums;
using Todo_Service.Factory;
using Todo_Service.Models;

namespace Todo_Service.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DbConnectionFactory _factory;
        private ILogger<TodoRepository> _logger;
        public TodoRepository(DbConnectionFactory factory , ILogger<TodoRepository> logger)
        {
            this._logger = logger;
            this._factory = factory;
        }
        public async Task<Todo> CreateTodo(Todo todo)
        {
            try
            {
                using var conn = this._factory.GetDatabaseConnection("main");
                conn.Open();
                using SqlCommand cmd = new SqlCommand("INSERT INTO Todo (Title,Description,DateLimite,Status,UserId) VALUES(@Title,@Description,@DateLimite,@Status,@UserId)", conn);
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@DateLimite", todo.DateLimite);
                cmd.Parameters.AddWithValue("@Status", todo.Status.ToString());
                cmd.Parameters.AddWithValue("@UserId", todo.UserId); //Default Value for now :)
                if (await cmd.ExecuteNonQueryAsync() > 0)
                    return todo;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, " Failed to insert Todo with {TodoId}", todo.Id);
                throw;
                
            }
            return null;
            
        }

        public bool DeleteTodo(string uuid)
        {
            try
            {
                using var conn = this._factory.GetDatabaseConnection("main");
                conn.Open();
                using var cmd = new SqlCommand("DELETE FROM Todo WHERE Id=@Id" , conn);
                cmd.Parameters.AddWithValue("Id", uuid);
                if(cmd.ExecuteNonQuery() > 0) return true;
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, " Failed to delete todo with {TodoId}", uuid);
                throw; //notify the controller layer so we ddint mute the exception to ensure retourning 500 internal server
            }
            return false;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            var myTodos = new List<Todo>();
            try
            {
                using var conn = _factory.GetDatabaseConnection("main");
                await conn.OpenAsync();

                using var cmd = new SqlCommand("SELECT * FROM Todo WHERE UserId=@UserId", conn);
                cmd.Parameters.AddWithValue("@UserId", Guid.Parse("EEBB08F6-1620-4322-B3EB-5B19FB0557CA")); //Static for now :)

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    myTodos.Add(new Todo
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"] is DBNull ? null : reader["Description"].ToString(),
                        DateLimite = reader["DateLimite"] is DBNull ? null : (DateTime?)reader["DateLimite"],
                        Status = Enum.TryParse<TodoStats>(reader["Status"].ToString(), out var status) ? status : TodoStats.TODO,
                        UserId = Guid.Parse(reader["UserId"].ToString())
                    });
                }

                return myTodos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch todos for user {UserId}", Guid.Parse("00000000-0000-0000-0000-000000000001"));
                throw;
            }
        }

        public Todo GetById(string uuid)
        {
            try
            {
                using var conn = _factory.GetDatabaseConnection("main");
                conn.Open();

                using var cmd = new SqlCommand("SELECT * FROM Todo WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", Guid.Parse(uuid));

                using var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                return new Todo
                {
                    Id = Guid.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"] is DBNull ? null : reader["Description"].ToString(),
                    DateLimite = reader["DateLimite"] is DBNull ? null : (DateTime?)reader["DateLimite"],
                    Status = Enum.TryParse<TodoStats>(reader["Status"].ToString(), out var Status) ? Status : TodoStats.TODO,
                    UserId = Guid.Parse(reader["UserId"].ToString())
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Todo {TodoId}", uuid);
                throw; 
            }
        }

        public async Task<Todo?> UpdateTodo(Todo todo)
        {
            try
            {
                using var conn = _factory.GetDatabaseConnection("main");
                await conn.OpenAsync();

                var cmd = new SqlCommand(
                    "UPDATE Todo SET Title=@Title, Description=@Description, DateLimite=@DateLimite, Status=@Status " +
                    "WHERE Id=@Id", conn);

                cmd.Parameters.AddWithValue("@Id", todo.Id);
                cmd.Parameters.AddWithValue("@UserId", todo.UserId);
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", (object)todo.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateLimite", (object)todo.DateLimite ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", todo.Status.ToString());

                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows == 0)
                {
                    return null;
                }
                return todo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update Todo {TodoId}", todo.Id);
                throw;
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using Todo_Service.Factory;
using Todo_Service.Models;

namespace Todo_Service.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DbConnectionFactory _factory;
        public TodoRepository(DbConnectionFactory factory)
        {
            this._factory = factory;
        }
        public Todo CreateTodo(Todo todo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTodo(string uuid)
        {
            throw new NotImplementedException();
        }

        public List<Todo> GetAllTodos()
        {
            throw new NotImplementedException();
        }

        public Todo GetById(string uuid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTodo(Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}

using Todo_Service.Models;

namespace Todo_Service.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllTodos();
        Todo GetById(string uuid);
        Boolean DeleteTodo(string uuid);
        Task<Todo> UpdateTodo(Todo todo);
        Task<Todo> CreateTodo(Todo todo);
    }
}

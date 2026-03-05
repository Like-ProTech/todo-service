using Todo_Service.Models;

namespace Todo_Service.Repositories
{
    public interface ITodoRepository
    {
        List<Todo> GetAllTodos();
        Todo GetById(string uuid);
        Boolean DeleteTodo(string uuid);
        Boolean UpdateTodo(Todo todo);
        Todo CreateTodo(Todo todo);
    }
}
